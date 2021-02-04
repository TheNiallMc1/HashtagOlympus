using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialXButton : MonoBehaviour
{

    public GameObject myTutorial;

    public void ExitTutorial()
    {
        myTutorial.gameObject.SetActive(false);
        TutorialManager.Instance.IncrementTutorialIndex();
    }
}
