using cakeslice;
using UnityEngine;

public class ModelBehaviour : MonoBehaviour
{
    public int currentPosition;
    private Transform objectToShow;

    [SerializeField] [Header("Character Stats")]
    public string godName;
    public string godHealth;
    public string godDamage;
    public string godArmour;
    public string godAbility1;
    public string godAbility2;
    public string godUltimate;
    public bool isSelected;

    public void SetInitialPosition(int pos)
    {
        currentPosition = pos;
        gameObject.transform.position = ShowModelController.Instance.positions[currentPosition].position;

        if (currentPosition == 0)
        {
            ShowModelController.Instance.SetCurrentModel(this);
        } 
    }

    public void SetCurrentPosition()
    {
        var tempPosition = currentPosition + 1;
        if (tempPosition > 2)
        {
            tempPosition = 0;
        }

        currentPosition = tempPosition;
        gameObject.transform.position = ShowModelController.Instance.positions[currentPosition].position;
        
        if (currentPosition == 0)
        {
            ShowModelController.Instance.SetCurrentModel(this);
        } 
    }

    public void ToggleOutline(bool shouldTurnOn)
    {
        var allObjects = GetComponentsInChildren<Transform>();
        foreach (var child in allObjects)
        {
            if (child.GetComponent<Outline>() != null)
            {
                child.GetComponent<Outline>().enabled = shouldTurnOn;
            }
        }
    }
}
