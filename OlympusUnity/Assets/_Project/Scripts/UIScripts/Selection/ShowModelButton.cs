using UnityEngine;
using UnityEngine.UI;

public class ShowModelButton : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(HandleButtonClick);
    }

    public void HandleButtonClick()
    {
        Debug.Log("clicking");
    }
}
