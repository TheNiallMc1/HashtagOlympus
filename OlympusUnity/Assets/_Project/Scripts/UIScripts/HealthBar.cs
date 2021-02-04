using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image bar;
    public RectTransform button;

    [SerializeField]
    private float healthValue;
    

    // Update is called once per frame
    private void Update()
    {
         
    }

    public void UpdateHealthBar(float healthV)
    {
        print("1Health of " + this.gameObject.name + " is " + healthV);    
        float amount = (healthV / 100f) * 180f / 360;
        bar.fillAmount = amount;
        print("Amount of " + this.gameObject.name + " is " + amount);
        float buttonAngle = amount * 360;
        print("ButtonAngle of " + this.gameObject.name + " is " + buttonAngle);
        button.localEulerAngles = new Vector3(0,0,-buttonAngle);
    }
}
