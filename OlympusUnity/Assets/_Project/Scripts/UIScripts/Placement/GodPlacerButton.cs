using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GodPlacerButton : MonoBehaviour
{
    public int godIndex;
    public GodBehaviour selectedGod;
    private Button _btn;
    private GameObject _godToPlace;
    public GameObject blueprint;

    void Awake ()
    {
        //_godToPlace = selectedGod;
        _btn = GetComponent<Button>();
        //_btn.GetComponentInChildren<TMP_Text>().text = selectedGod.godName;
        
    }

    private void Start()
    {
        selectedGod = UberManager.Instance.selectedGods[godIndex];
        _btn.GetComponentInChildren<TMP_Text>().text = selectedGod.godName;
    }

    public void SpawnBlueprint()
    {
        PlacementManager.Instance.ChangeCurrentGodIndex(godIndex);
        GameObject godGO = PlacementManager.Instance.ReturnCurrentGod();
        godGO.transform.position = new Vector3(0, -1000,0);
        Instantiate(blueprint, new Vector3(0, 1000, 0), Quaternion.Euler(new Vector3(0, 180, 0)));
    }
}
