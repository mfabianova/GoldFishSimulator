using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishMovement : MonoBehaviour
{
    public float movement_speed = 100;
    public float rotation_speed = 100;
    public float back_speed = 10;
    private float slow_down = 1;

    public GameObject main_camera;
    private Vector3 movement;
    private Vector3 offset;

    private bool IsFlyingBack = false;

    private float t = 0.0f;
    private Vector3 start_p;
    private Vector3 end_p;
    private float direction = -1;

    public TimerScript timer;
    private float time;
    private int amount = 20;
    void Start()
    {
        offset = transform.position - main_camera.transform.position;
        time = 100f;
        timer.SetMaxTime();
    }

    private void Update()
    {
        if (time < 1)
        {
            SceneManager.LoadScene("gameover", LoadSceneMode.Single);
            return;
        }
        time -= 1 * Time.deltaTime;
        timer.SetTime(time);
        Debug.Log(time);

        if (IsFlyingBack)
        {
            main_camera.transform.position = Vector3.Lerp(start_p, end_p, t);
            t += Time.deltaTime;

            if (t < 1.0f)
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

        movement = main_camera.transform.TransformDirection(Vector3.forward);
        if (transform.position.y >= 15)
        {
            main_camera.transform.position -= new Vector3(0, 0.5f, 0);
        }
        main_camera.transform.position += movement * movement_speed * Time.deltaTime;
    }

    private void LateUpdate()
    {
        transform.position = main_camera.transform.position + main_camera.transform.TransformDirection(offset);
        transform.LookAt(transform.position - (main_camera.transform.position - transform.position));
    }
    private void OnCollisionEnter(Collision collision)
    {
        time = time - amount;
        timer.SetTime(time);
        if (IsFlyingBack == true)
        {
            direction = -direction;
            Debug.Log("Change of direction: " + direction);
            slow_down = 0.5f * slow_down;
        }
        IsFlyingBack = true;

        start_p = main_camera.transform.position;
        end_p = main_camera.transform.position + main_camera.transform.TransformDirection(direction * Vector3.forward * back_speed * slow_down);

        t = 0.0f;
    }
    private void OnTriggerEnter(Collider other)
    {
        time = time + amount;
        timer.SetTime(time);
        Destroy(other.gameObject);
    }
}
