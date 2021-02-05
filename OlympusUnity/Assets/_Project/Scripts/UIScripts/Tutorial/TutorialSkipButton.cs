using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSkipButton : MonoBehaviour
{
    public GameObject myTutorial;
    public void SkipTutorial()
    {
        TutorialManager.Instance.mainTutorialComplete = true;
        myTutorial.gameObject.SetActive(false);
       
    }
}
