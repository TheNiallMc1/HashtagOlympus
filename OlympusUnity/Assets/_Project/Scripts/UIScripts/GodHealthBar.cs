using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GodHealthBar : MonoBehaviour
{
    public GodBehaviour correspondingGod;
    private Slider thisSlider;

    private void Start()
    {
        thisSlider = GetComponent<Slider>();
        Initialise();
    }

    public void Initialise()
    {
        thisSlider.value = thisSlider.maxValue;
        //print("init value: " + thisSlider.value);
    }

    public void SetValue(int newValue)
    {
       
        thisSlider.value = newValue;
        //print("set value: " + thisSlider.value);
    }
}
