using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawSpawner : MonoBehaviour
{
    public GameObject PawPrefab;

    public float minBetweenTime = 10;
    public float maxBetweenTime = 50;

    public float min_x = -15;
    public float min_z = -15;
    public float max_x = 15;
    public float max_z = 15;

    private float currentTime;
    private float spawnTime;

    void Start()
    {
        currentTime = 0;
        spawnTime = Random.Range(minBetweenTime, maxBetweenTime);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= spawnTime)
        {
            currentTime = 0;

            float pos_x = Random.Range(min_x, max_x);
            float pos_z = Random.Range(min_z, max_z);

            Vector3 Position = new Vector3(pos_x, 30, pos_z);
            GameObject a = Instantiate(PawPrefab);
            a.transform.position = Position;

            spawnTime = Random.Range(minBetweenTime, maxBetweenTime);
            
        }
    }
}
