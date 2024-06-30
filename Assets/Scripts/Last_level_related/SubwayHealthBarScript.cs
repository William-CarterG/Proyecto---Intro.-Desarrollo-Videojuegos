using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubwayHealthBarScript : MonoBehaviour
{
    public Slider slider;
    public GameObject speedManager;

    private void Start()
    {
        setMaxHealth();
    }

    public void setMaxHealth()
    {
        slider.value = slider.maxValue;
    }

    public void substractHealth(float time)
    {
        if(slider.value > slider.minValue)
        {
            slider.value = slider.value - time;
        }
        else
        {
            TrainSpeedScript script = speedManager.GetComponent<TrainSpeedScript>();
            script.TrainDead();
        }
    }
}
