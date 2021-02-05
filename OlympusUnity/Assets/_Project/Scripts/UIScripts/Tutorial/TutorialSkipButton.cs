using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSkipButton : MonoBehaviour
{

    public void SkipTutorial()
    {
        TutorialManager.Instance.mainTutorialComplete = true;
    }
}
