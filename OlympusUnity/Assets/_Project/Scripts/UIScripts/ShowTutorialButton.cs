using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTutorialButton : MonoBehaviour
{

    public int tutorialIndex;

    public void ShowTutorial()
    {
        TutorialManager.Instance.mainTutorialComplete = true;
        TutorialManager.Instance.allTutorials[tutorialIndex].SetActive(true);
    }
}
