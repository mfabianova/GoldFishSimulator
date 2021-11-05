using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FishMovement : MonoBehaviour
{
    public float movement_speed = 100;
    public float rotation_speed = 100;
    public float back_speed = 10;

    public GameObject main_camera;
    private Vector3 movement;
    private Vector3 offset;

    //private bool IsColliding = false;
    private bool IsFlyingBack = false;

    private float t = 0.0f;
    private Vector3 start_p;
    private Vector3 end_p;
    void Start()
    {
        offset =  main_camera.transform.position - transform.position;
        
        //transform.eulerAngles = new Vector3(0, 0, 0);
        //Camera.main.transform.eulerAngles = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        
        if (IsFlyingBack)
        {
            transform.position = Vector3.Lerp(start_p, end_p, t);
            t += Time.deltaTime;
            
            if(t < 1.0f)
            {
                IsFlyingBack = true;
            }
            else
            {
                IsFlyingBack = false;
            }
            
            return;
        }

        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * Time.deltaTime * rotation_speed, 0), Space.World);
        transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * Time.deltaTime * rotation_speed, 0, 0));
        //main_camera.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * Time.deltaTime * rotation_speed, 0), Space.World);
        //main_camera.transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * Time.deltaTime * rotation_speed, 0, 0));

        movement = transform.TransformDirection(Vector3.forward);
        //Debug.Log(movement);

        //if (IsColliding == false)
        {
            transform.position += movement * movement_speed * Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        //if (IsColliding == false)
        {
            main_camera.transform.position = transform.position + transform.TransformDirection(offset);
            main_camera.transform.LookAt(transform.position);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //if(collision.gameObject.CompareTag("CatPaw"))
        {
            IsFlyingBack = true; 
           
            start_p = transform.position;
            end_p = transform.position + transform.TransformDirection(- Vector3.forward * back_speed);
            Debug.Log(start_p);
            Debug.Log(end_p);
            
            t = 0.0f;
        }
        //else
        {
            //IsColliding = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
