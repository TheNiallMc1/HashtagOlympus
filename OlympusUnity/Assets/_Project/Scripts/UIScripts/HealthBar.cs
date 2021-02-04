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
        float amount = (healthV / 100f) * 180f / 360;
        bar.fillAmount = amount;
        float buttonAngle = amount * 360;
        button.localEulerAngles = new Vector3(0,0,-buttonAngle);
    }
}
