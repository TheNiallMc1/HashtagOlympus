using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GodPlacerButton : MonoBehaviour
{

    public int godIndex;
    private Button _btn;
    private GameObject _godToPlace;
    public GameObject blueprint;

    void Awake ( )
    {
        _godToPlace = BekahsGM.Instance.currentGods[godIndex];
        _btn = GetComponent<Button> ( );
        _btn.GetComponentInChildren<TMP_Text>().text = _godToPlace.GetComponent<GodBehaviour>().godName;

    }

    public void SendSelectionToPlacementManager()
    {
        PlacementManager.Instance.GetSelection(godIndex);
        //Instantiate(_godToPlace);
    }
    
    public void SpawnBlueprint()
    {
       
        ImprovedPlacementManager.Instance.currentGodIndex = godIndex;
        Instantiate(blueprint, new Vector3(0, 1000, 0), Quaternion.identity);
    }
}
