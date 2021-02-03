using UnityEngine;
using UnityEngine.UI;

public class GodSelectorButton : MonoBehaviour
{
    private Text buttonText;

    private bool isSelected;
    public Button selectBtn;
    public Button deselectBtn;


    private void Awake()
    {
        //buttonText = GetComponentInChildren<Text>();
        isSelected = false;
        //buttonText.text = "SELECT";
        selectBtn.gameObject.SetActive(true);
        deselectBtn.gameObject.SetActive(false);
    }

    public void UpdateButton()
    {
        if (!ShowModelController.Instance.currentModel.isSelected)
        {
            isSelected = false;
            //buttonText.text = "SELECT";
            selectBtn.gameObject.SetActive(true);
            deselectBtn.gameObject.SetActive(false);
        }

        if (ShowModelController.Instance.currentModel.isSelected)
        {
            isSelected = true;
           // buttonText.text = "DESELECT";
            selectBtn.gameObject.SetActive(false);
            deselectBtn.gameObject.SetActive(true);
        }
    }

    public void SendSelection()
    {
        if (!isSelected)
        {
            ShowModelController.Instance.currentModel.isSelected = true;
            GodSelectManager.Instance.AddGodToList(ShowModelController.Instance.currentModel);
            UpdateButton();
        }
        
        else if (isSelected)
        {
            ShowModelController.Instance.currentModel.isSelected = false;
            GodSelectManager.Instance.RemoveGodFromList(ShowModelController.Instance.currentModel);
            UpdateButton();
        }
    }
}