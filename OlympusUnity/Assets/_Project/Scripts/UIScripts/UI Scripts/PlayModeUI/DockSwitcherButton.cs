using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using TMPro;

public class DockSwitcherButton : MonoBehaviour
{
    public TMP_Text godNameDisplay;
    public int godKey;

    public void SetCurrentGod(int key, string godName)
    {
        godKey = key;
        godNameDisplay.text = godName;
    }

    public void SendSwitchInfo()
    {
        
        InterimUIManager.Instance.UpdateHUD(godKey);
    }
}

