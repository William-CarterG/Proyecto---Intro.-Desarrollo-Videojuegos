using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesLastLevel : MonoBehaviour
{
    public float speed = 5f;
    private TrainSpeedScript trainSpeedScript;
    private float limitDown = -70f;
    // Start is called before the first frame update
    void Start()
    {
        GameObject speedManager = GameObject.Find("SpeedManager");
        if (speedManager != null)
        {
            trainSpeedScript = speedManager.GetComponent<TrainSpeedScript>();
            speed = trainSpeedScript.getSpeed();
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;

        if(transform.position.y < limitDown)
        {
            Destroy(gameObject);
        }
    }
}
