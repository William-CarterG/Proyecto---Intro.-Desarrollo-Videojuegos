using System.Collections;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject tuetue;
    public GameObject visionRange;
    public float xOffset = 2f;

    public float minSpawnTimeLeft = 1f;
    public float maxSpawnTimeLeft = 5f;
    public float minSpawnTimeCenter = 1f;
    public float maxSpawnTimeCenter = 5f;
    public float minSpawnTimeRight = 1f;
    public float maxSpawnTimeRight = 5f;

    void Start()
    {
        StartCoroutine(SpawnCenter());
        StartCoroutine(SpawnRight());
        StartCoroutine(SpawnLeft());
    }

    IEnumerator SpawnCenter()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnTimeCenter, maxSpawnTimeCenter);
            yield return new WaitForSeconds(waitTime);
            SpawnObject(tuetue, transform.position);
        }
    }

    IEnumerator SpawnRight()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnTimeRight, maxSpawnTimeRight);
            yield return new WaitForSeconds(waitTime);
            Vector3 spawnPosition = new Vector3(transform.position.x + xOffset, transform.position.y, transform.position.z);
            SpawnObject(visionRange, spawnPosition);
        }
    }

    IEnumerator SpawnLeft()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnTimeLeft, maxSpawnTimeLeft);
            yield return new WaitForSeconds(waitTime);
            Vector3 spawnPosition = new Vector3(transform.position.x - xOffset, transform.position.y, transform.position.z);
            SpawnObject(visionRange, spawnPosition);
        }
    }

    void SpawnObject(GameObject prefab, Vector3 position)
    {
        Instantiate(prefab, position, Quaternion.identity);
    }
}
