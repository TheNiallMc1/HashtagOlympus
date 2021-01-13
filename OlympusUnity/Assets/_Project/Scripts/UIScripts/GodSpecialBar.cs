using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GodSpecialBar : MonoBehaviour
{
    private Slider thisSlider;
    
    private void Awake()
    {
        thisSlider = GetComponent<Slider>();
    }
    public void Initialise(int initialValue)
    {
        thisSlider.value = initialValue;
        print("init value: " + thisSlider.value);
    }
    
    public void SetValue(int newValue)
    {
        thisSlider.value = newValue;
        print("set value: " + thisSlider.value);
    }
}