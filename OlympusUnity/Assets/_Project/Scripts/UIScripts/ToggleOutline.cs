using System.Collections;
using System.Collections.Generic;
using cakeslice;
using UnityEngine;

public class ToggleOutline : MonoBehaviour
{
    public void ToggleOutlineOnOff(bool shouldTurnOn)
    {
        var allObjects = gameObject.GetComponentsInChildren<Transform>();
        foreach (var child in allObjects)
        {
            if (child.GetComponent<Outline>() !=null)
            {
                child.GetComponent<Outline>().enabled = shouldTurnOn;
            }
        }
    }
}
