using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowModelButton : MonoBehaviour
{
    private Transform objectToShow;

    private Action<Transform> clickAction;

    public void Initialize(Transform objectToShow, Action<Transform> clickAction)
    {
        this.objectToShow = objectToShow;
        this.clickAction = clickAction;
        GetComponentInChildren<TMP_Text>().text = objectToShow.gameObject.name;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(HandleButtonClick);
    }

    public void HandleButtonClick()
    {
        Debug.Log("clicking");
        //clickAction(objectToShow);
        //ShowModelController.Instance.EnableModel(objectToShow);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
