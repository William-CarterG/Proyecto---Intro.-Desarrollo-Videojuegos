using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    public GameObject boidPrefab;
    public int boidCount = 5;
    public Vector2 spawnArea = new Vector2(10, 10);

    void Start()
    {
        for (int i = 0; i < boidCount; i++)
        {
            Vector2 spawnPosition = new Vector2(
                Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
                Random.Range(-spawnArea.y / 2, spawnArea.y / 2)
            );

            Instantiate(boidPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
