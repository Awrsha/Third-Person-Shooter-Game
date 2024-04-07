//main character movements script 
//third person shooter game
//Mohammad Mohsen Moradi / Amir Mohammad Parvizi / Morteza Pourasgar

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    CharacterController characterController;
    Animator animator;
    FPShooterGame inputActions;
    InputAction move;
    InputAction aim;
    InputAction run;
    InputAction switchGunAction;
    bool isRunning=false;
    bool isAiming=false;
    //movement speed
    float MoveSpeed = 0f;
    float ASpeed = 0f;
    float AimVal=0;
    public float AimSpeed=3f;
    public Camera MainCamera;
    float deaccelerationASpeed = 0.5f;
    float accelerationASpeed = 0.4f;
    //float rotationSpeed=40f;
    float currentVelocity;
    public float MaxRunningAccelerationSpeed;
    public float RunningAccelerationSpeed;
    float smoothTime = 0.2f;
    public GameObject Pistol;
    Guns SelectedGun;
    // Start is called before the first frame update
    void Start()
    {
        characterController=GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        inputActions = new FPShooterGame();
        move = inputActions.Player.Move;
        run = inputActions.Player.Run;
        aim = inputActions.Player.Aim;
        switchGunAction = inputActions.Player.SwitchGun;
        move.Enable();
        run.Enable();
        aim.Enable();
        switchGunAction.Enable();
        run.performed += Run_performed;
        run.canceled += Run_canceled;
        aim.performed += Aim_performed;
        aim.canceled += Aim_canceled;
        switchGunAction.started += switchGunAction_Started;
    }

    private void switchGunAction_Started(InputAction.CallbackContext obj)
    {
        SwitchGuns();
    }

    private void Aim_performed(InputAction.CallbackContext obj)
    {
        isAiming = true;
    }

    private void Aim_canceled(InputAction.CallbackContext obj)
    {
        isAiming = false;
    }

    private void Run_canceled(InputAction.CallbackContext obj)
    {
        isRunning = false;
    }

    private void Run_performed(InputAction.CallbackContext obj)
    {
        isRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveVal=move.ReadValue<Vector2>();
        //if move is performed the animations are activate
        if(moveVal.magnitude > 0.1f)
        {
            //activate run animation
            if(isRunning)
            {
                if(ASpeed <= 1)
                    ASpeed += accelerationASpeed * Time.deltaTime;
                if(MoveSpeed <= MaxRunningAccelerationSpeed)
                    MoveSpeed += (RunningAccelerationSpeed) * Time.deltaTime; 
            }
            //activate walk animation
            else
            {
                ASpeed=0.1f;
                MoveSpeed=4.0f;
            }
        }
        //run idle animation
        else
        {
            if(MoveSpeed > 0)
                MoveSpeed = 0;
            if(ASpeed > 0)
                ASpeed -= deaccelerationASpeed * Time.deltaTime;
        }
        Aiming();
        Vector3 direction = new Vector3(moveVal.x,-100, moveVal.y);
        if(direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + MainCamera.transform.eulerAngles.y;
            float SmoothAngle = Mathf.SmoothDampAngle(this.transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
            Quaternion rotation = Quaternion.Euler(new Vector3(0f, SmoothAngle, 0f));
            this.transform.rotation = rotation;
            Vector3 moveDir = rotation * Vector3.forward;

            moveDir -= new Vector3(0,1,0);
            characterController.Move(moveDir * MoveSpeed * Time.deltaTime);
        }
        animator.SetFloat("Speed", ASpeed);
        /*this.transform.Rotate(new Vector3(0,Input.GetAxis("Mouse X")*rotationSpeed*Time.deltaTime,0));
        if(MoveSpeed > 0)
            characterController.Move(this.transform.forward*moveVal.y*MoveSpeed*Time.deltaTime);*/
    }
    
    void Aiming()
    {
        if(isAiming==true && SelectedGun != Guns.NoGun)
        {
            if(AimVal <= 1)
                AimVal += AimSpeed * Time.deltaTime;
        }
        else
        {
            if(AimVal > 0)
                AimVal -= AimSpeed * Time.deltaTime;
        }
        animator.SetLayerWeight(1, AimVal);
    }

    void SwitchGuns()
    {
        int selectedIndex = (int)SelectedGun;
        if(selectedIndex==1)
            selectedIndex=0;
        else
            selectedIndex++;
        SelectedGun = (Guns)selectedIndex;
        animator.SetBool("SwitchPistol",true);
        StartCoroutine(SwitchGunAnimationWaiting(1f));
    }

    IEnumerator SwitchGunAnimationWaiting(float sec)
    {
        yield return new WaitForSeconds(sec);
        switch(SelectedGun)
        {
            case Guns.NoGun:
                Pistol.SetActive(false);
                break;
            case Guns.Pistol:
                Pistol.SetActive(true);
                break;
            default:
                break;
        }
        animator.SetBool("SwitchPistol",false);
    }

    public enum Guns
    {
        NoGun=0,
        Pistol=1,
    }
}
