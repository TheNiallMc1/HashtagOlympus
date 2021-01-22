using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GodSelectorButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GodBehaviour selectedGod;
    
    private Button btn;
    private TMP_Text buttonText;

    private bool selectDeselect;


    void Awake ( )
    {
        btn = GetComponent<Button> ( );
        buttonText = GetComponentInChildren<TMP_Text>();
        selectDeselect = false;
        btn.GetComponent<Image>().color = Color.white;
        buttonText.text = selectedGod.godName;
    }

    public void SendSelection()
    {
        selectDeselect = !selectDeselect;

        if (selectDeselect)
        {
            Debug.Log("selecting");
            GodSelectManager.Instance.AddGodToList(selectedGod);
            btn.GetComponent<Image>().color = Color.yellow;
            
        } else if (!selectDeselect)
        {
            Debug.Log("deselecting");
            GodSelectManager.Instance.RemoveGodFromList(selectedGod);
            btn.GetComponent<Image>().color = Color.white;
        }
    }
}
