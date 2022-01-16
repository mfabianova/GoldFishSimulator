using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawMovement : MonoBehaviour
{
    public float min_depth, max_depth;
    private float depth;

    public float speed = 5;
    
    void Start()
    {
        depth = Random.Range(min_depth, max_depth);
    }

    void Update()
    {
        transform.position += new Vector3(0,-speed * Time.deltaTime);
        if(transform.position.y < 30-depth)
        {
            speed = -speed;
        }
        if(transform.position.y > 30)
        {
            Destroy(this.gameObject);
        }
    }
}
