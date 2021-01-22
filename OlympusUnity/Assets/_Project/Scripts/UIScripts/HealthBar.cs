using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image bar;
    public RectTransform button;

    public float healthValue;
    

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar(healthValue);
        
    }

    public void UpdateHealthBar(float healthV)
    {
        float amount = (healthValue / 100f) * 180f / 360;
        bar.fillAmount = amount;
        float buttonAngle = amount * 360;
        button.localEulerAngles = new Vector3(0,0,-buttonAngle);
    }
}
