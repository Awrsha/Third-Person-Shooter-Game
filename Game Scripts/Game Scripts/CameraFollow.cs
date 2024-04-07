//camera follow script 
//third person shooter game
//Mohammad Mohsen Moradi / Amir Mohammad Parvizi / Morteza Pourasgar

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    GameObject character;
    [SerializeField]
    Vector3 offset;
    float rotationSpeed=40f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        offset=Quaternion.AngleAxis(Input.GetAxis("Mouse X")*rotationSpeed*Time.deltaTime,Vector3.up)*offset;
        this.transform.position=Vector3.Lerp(this.transform.position,character.transform.position+offset,0.5f);
        this.transform.LookAt(character.transform.position+new Vector3(1,5,0));
    }
}
