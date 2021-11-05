using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //objekt, ktory chcem spawnovat
    public GameObject foodPrefab;
    

    //min a max cas medzi spawnovanim vyberie sa random do spawnTime
    public float minBetweenTime = 10;
    public float maxBetweenTime = 50;

    //max a min hodnoty, kde sa jedno moze spawnovat - len orientacne so far
    public float min_x = -15 ;
    public float min_z = -15;
    public float max_x = 15;
    public float max_z = 15;

    private float currentTime;
    private float spawnTime;

    void Start()
    {
        //setne aktualny cas na nulu a najde nahodny cas kedy sa spawne food
        currentTime = 0;
        spawnTime = Random.Range(minBetweenTime, maxBetweenTime);
        
    }

    
    void FixedUpdate()
    {
        //coroutine

        //zvysi sa cas
        currentTime += Time.deltaTime;

        //ak je cas viac ako spawnTime spawn object a restart
        if (currentTime >= spawnTime)
        {
            currentTime = 0;

            float pos_x = Random.Range(min_x, max_x);
            float pos_z = Random.Range(min_z, max_z);

            Vector3 Position = new Vector3(pos_x, 20, pos_z);
            GameObject a = Instantiate(foodPrefab);
            a.transform.position = Position;

            spawnTime = Random.Range(minBetweenTime, maxBetweenTime);
        }
        
    }
}
