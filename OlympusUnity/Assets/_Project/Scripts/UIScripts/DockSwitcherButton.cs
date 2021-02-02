using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DockSwitcherButton : MonoBehaviour
{
    public TMP_Text godNameDisplay;
    private Button myButton;
    
    
    private int godKey;
    public Sprite currentSprite;

    public void Start()
    {
        myButton = GetComponent<Button>();
    }

    public void Update()
    {
        Debug.Log("godkey : "+godKey);
    }

    public int GetGodKey()
    {
        return godKey;
    }

    public void SetGodKey(int newValue)
    {
        godKey = newValue;
    }

    public void SetCurrentGod(int key, string godName)
    {
        godKey = key;
        godNameDisplay.text = godName;

        Debug.Log("switch button: "+godName);
        Debug.Log("SET UP CURRENT GOD: "+key+godKey+", "+godName);
    }

    public void SendSwitchInfo()
    {
        Debug.Log("I am sending key: "+godKey);
        
        InterimUIManager.Instance.UpdateHUD(godKey);
    }

    public void SnapToCurrentGod()
    {
        CameraController.Instance.FollowPlayer(GameManager.Instance.currentlySelectedGod);
    }
}

