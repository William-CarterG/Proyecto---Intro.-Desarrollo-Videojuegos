using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManagerScript : MonoBehaviour
{
    public int fireCount = 0;
    public GameObject Healthbar;
    private SubwayHealthBarScript healthScript;
    public GameObject Fire;
    private float timer = 0f;
    public float spawnerTime;
    

    // Start is called before the first frame update
    void Start()
    {
        healthScript = Healthbar.GetComponent<SubwayHealthBarScript>();
    }

    // Update is called once per frame
    void Update()
    {
        healthScript.substractHealth(fireCount * Time.deltaTime);
        timer += Time.deltaTime;
        if (timer > spawnerTime)
        {
            SpawnRandomly();
            timer = 0f;
            fireCount += 1;
        }
    }

    public void FireDestroyed()
    {
        fireCount -= 1;
    }

    void SpawnRandomly()
    {
        float randomX = Random.Range(2f, 11f);
        float randomY = Random.Range(-57f, 0.5f);
   
        Vector3 randomPosition = new Vector3(randomX, randomY, 0f);

        
        Instantiate(Fire, randomPosition, Quaternion.identity);
    }
}
