using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GodSelectorButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject selectedGod;
    
    private Button btn;


    void Start ( )
    {
        btn = GetComponent<Button> ( );
        
    }

   
    
    
    public void SendSelection()
    {
       
        //GameManager.Instance.AddGodToActiveList(this.selectedGod);
        //BekahsGM.Instance.PassedGameObject = selectedGod;
        btn.gameObject.SetActive(false);
    }
}
