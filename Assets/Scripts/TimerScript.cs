using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public Slider slider;

    public void SetTime(float time)
    {
        slider.value = time;
    }
    
    public void SetMaxTime()
    {
        Debug.Log("inside set max time");
        slider.value = slider.maxValue;
    }
}
