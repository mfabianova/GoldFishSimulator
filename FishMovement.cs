using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float movement_speed = 100;
    public float rotation_speed = 100;


    private Rigidbody rb;
    public GameObject main_camera;
    private Vector3 movement;
    

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //transform.eulerAngles = new Vector3(0, 0, 0);
        //Camera.main.transform.eulerAngles = new Vector3(0, 0, 0);
    }

    /*private void Update()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * rotation_speed, -Input.GetAxis("Mouse Y") * Time.deltaTime * rotation_speed);
        movement = main_camera.transform.forward;

        transform.position += movement * movement_speed * Time.deltaTime;
    }*/

    void FixedUpdate()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * rotation_speed, -Input.GetAxis("Mouse Y") * Time.deltaTime * rotation_speed, Space.World);
        Debug.Log(transform.rotation);

        movement = main_camera.transform.forward;
        Debug.Log(movement);
        rb.AddForce(movement * movement_speed * Time.deltaTime);

    }
}
