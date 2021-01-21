using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlacementManager : MonoBehaviour
{

    public Button button1;
    public TMP_Text button1Text;
    public GodPlacerButton button1Script;
    
    public Button button2;
    public TMP_Text button2Text;
    public GodPlacerButton button2Script;
    
    public Button button3;
    public TMP_Text button3Text;
    public GodPlacerButton button3Script;
    
    
    // Start is called before the first frame update
    void Start()
    {
        button1Script.selectedGod = UberManager.Instance.selectedGods[0];
        button2Script.selectedGod = UberManager.Instance.selectedGods[1];
        button3Script.selectedGod = UberManager.Instance.selectedGods[2];

        //button1Text.text = UberManager.Instance.selectedGods[0].godName;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
