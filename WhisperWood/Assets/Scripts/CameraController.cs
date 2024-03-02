using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraController : MonoBehaviour
{
    //public float sensitivity = 2f;
    public float speed = 40f;

    public float minAngle = -45f;
    public float maxAngle = 45f;

    public GameObject player;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //float mouseX = Input.GetAxis("Mouse X");
        //float mouseY = Input.GetAxis("Mouse Y");
        //transform.eulerAngles += new Vector3(-mouseY * sensitivity, mouseX * sensitivity, 0);

        

    }


  
    
    // Might remove this part because wonky with the movement settings - Damian

    /*
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.RotateAround(player.transform.position, Vector3.up, -speed * Time.deltaTime);
    //        RotateCameraHorizontal(-speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
       //     RotateCameraHorizontal(speed * Time.deltaTime);
            transform.RotateAround(player.transform.position, Vector3.up, speed * Time.deltaTime);
        }

    }
*/

    void RotateCameraHorizontal(float angle)
    {
        //CODE THAT ROTATES AROUND FOREVER
        Quaternion currentRotation = transform.rotation;
        Quaternion newRotation = Quaternion.AngleAxis(angle, Vector3.up) * currentRotation;
        transform.rotation = newRotation;


        //CLAMPING CODE THAT DOESN'T WORK

        //float newAngle = Mathf.Clamp(transform.rotation.eulerAngles.y + angle, minAngle, maxAngle);
        //Quaternion currentRotation = transform.rotation;
        //Quaternion newRotation = Quaternion.Euler(currentRotation.eulerAngles.x, newAngle, currentRotation.eulerAngles.z);
        //transform.rotation = newRotation;



    }
}
