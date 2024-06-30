using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSpeedScript : MonoBehaviour
{
    public float Speed = 6.0f;
    public bool TrainRunning = true;
    public GameObject Horde;
    public float distance = 0.0f;
    public float distanceObjective = 360.0f;
    public GameObject ProgressMeter;

    private ProgressBarScript barScript;
    private HordeScript hordeScript;
    // Start is called before the first frame update
    void Start()
    {
        barScript = ProgressMeter.GetComponent<ProgressBarScript>();
        barScript.setMax(distanceObjective);
        hordeScript = Horde.GetComponent<HordeScript>();
        hordeScript.setSpeed(Speed);
    }

    // Update is called once per frame
    void Update()
    {
        distance += Speed * Time.deltaTime;
        barScript.progress(distance);

        if(distance > distanceObjective)
        {
            win();
        }
    }

    public void UpdateSpeed(float newSpeed)
    {
        if (TrainRunning)
        {
            Speed = newSpeed;
            hordeScript.setSpeed(newSpeed);
        }
    }

    public float getSpeed()
    {
        return Speed;
    }

    public void TrainDead()
    {
        UpdateSpeed(0);
        TrainRunning = false;
    }

    void win()
    {
        Debug.Log("Won");
    }
}
