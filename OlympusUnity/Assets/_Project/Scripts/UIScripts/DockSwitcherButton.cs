using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DockSwitcherButton : MonoBehaviour
{
    public TMP_Text godNameDisplay;
    private Button myButton;
    public int godKey;

    private void Start()
    {
        myButton = GetComponent<Button>();
    }

    public void SetCurrentGod(int key, string godName, Sprite newSprite)
    {
        godKey = key;
        godNameDisplay.text = godName;
        myButton.image.sprite = newSprite;
    }

    public void SendSwitchInfo()
    {        
        InterimUIManager.Instance.UpdateHUD(godKey);
    }
}

