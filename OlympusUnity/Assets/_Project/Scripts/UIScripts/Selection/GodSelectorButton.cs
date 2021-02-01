using UnityEngine;
using UnityEngine.UI;

public class GodSelectorButton : MonoBehaviour
{
    private Text buttonText;

    private bool isSelected;


    private void Awake()
    {
        buttonText = GetComponentInChildren<Text>();
        isSelected = false;
        buttonText.text = "SELECT";
    }

    public void UpdateButton()
    {
        if (!ShowModelController.Instance.currentModel.isSelected)
        {
            isSelected = false;
            buttonText.text = "SELECT";
        }

        if (ShowModelController.Instance.currentModel.isSelected)
        {
            isSelected = true;
            buttonText.text = "DESELECT";
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