using UnityEngine;

public class GodOverviewMapIcon : MonoBehaviour
{

    public GodBehaviour currentGod;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendGodSelection()
    {
        GameManager.Instance.SelectGod(currentGod);
    }
}
