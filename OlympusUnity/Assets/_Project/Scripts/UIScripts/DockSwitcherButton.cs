using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DockSwitcherButton : MonoBehaviour
{
    public TMP_Text godNameDisplay;
    private Button myButton;
    public int godKey;
    public Sprite currentSprite;

    private void Start()
    {
        myButton = GetComponent<Button>();
    }

    public void SetCurrentGod(int key, string godName)
    {
        Debug.Log("setting switch button: "+key+", "+godName);
        godKey = key;
        godNameDisplay.text = godName;
        //currentSprite = newSprite;
        // myButton.image.sprite = newSprite;
    }
    
    public void SendGodSelection()
    {
    }

    public void SnapToCurrentGod()
    {
        CameraController.Instance.FollowPlayer(GameManager.Instance.currentlySelectedGod);
    }
}

