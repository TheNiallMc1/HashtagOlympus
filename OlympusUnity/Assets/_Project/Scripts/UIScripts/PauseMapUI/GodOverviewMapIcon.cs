using UnityEngine;

public class GodOverviewMapIcon : MonoBehaviour
{

    public GodBehaviour currentGod;
    // Start is called before the first frame update

    public void SendGodSelection()
    {
        GameManager.Instance.SelectGod(currentGod);
    }
}
