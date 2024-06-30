using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarScript : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        setMinProgress();
    }

    public void setMinProgress()
    {
        slider.value = slider.minValue;
    }


    public void setMax(float newMax)
    {
        slider.maxValue = newMax * 1.1111f;
    }

    public void progress(float distance)
    {
        slider.value = distance;
    }
}
