using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FishMovement : MonoBehaviour
{
    public float movement_speed = 100;
    public float rotation_speed = 100;
    public float back_speed = 10;
    private float slow_down = 1;

    private Rigidbody rb;
    public GameObject main_camera;
    private Vector3 movement;
    private Vector3 offset;

    //private bool IsColliding = false;
    private bool IsFlyingBack = false;

    private float t = 0.0f;
    private Vector3 start_p;
    private Vector3 end_p;
    private float direction = -1;
    
    
    void Start()
    {
        offset =  main_camera.transform.position - transform.position;
        rb = GetComponent<Rigidbody>();
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
                slow_down = 1;
                
            }
            
            return;
        }

        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * Time.deltaTime * rotation_speed, 0), Space.World);
        transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * Time.deltaTime * rotation_speed, 0, 0));

        //main_camera.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * Time.deltaTime * rotation_speed, 0), Space.World);
        //main_camera.transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * Time.deltaTime * rotation_speed, 0, 0));

        
        movement = transform.TransformDirection(Vector3.forward);
        

        if (transform.position.y >=15)
        {
            transform.position -= new Vector3 (0,0.5f,0);
        }
        transform.position += movement * movement_speed * Time.deltaTime;
        
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
            
            if(IsFlyingBack == true)
            {
                direction = -direction;
                Debug.Log("Change of direction: "+ direction);
                slow_down = 0.5f * slow_down;
                
            }
            IsFlyingBack = true;

            start_p = transform.position;
            end_p = transform.position + transform.TransformDirection(direction * Vector3.forward * back_speed * slow_down);
            

            t = 0.0f;
        }
    }        
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
