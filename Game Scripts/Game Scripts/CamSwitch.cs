//camera switch script 
//third person shooter game
//Mohammad Mohsen Moradi / Amir Mohammad Parvizi / Morteza Pourasgar

using UnityEngine;
using Cinemachine;

public class CamSwitch : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public CinemachineFreeLook freeLookCamera;
    public Transform Swat;
    public float sensitivity = 10f;

    private Transform cameraTransform;

    void Start()
    {
        freeLookCamera.Priority = 1;
        virtualCamera.Priority = 0;
        cameraTransform = virtualCamera.transform;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            virtualCamera.Priority = 1;
            freeLookCamera.Priority = 0;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            virtualCamera.Priority = 0;
            freeLookCamera.Priority = 1;
        }

        if (virtualCamera.Priority == 1)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

            Swat.Rotate(Vector3.up * mouseX);
            cameraTransform.Rotate(Vector3.right * -mouseY);
        }
    }
}
