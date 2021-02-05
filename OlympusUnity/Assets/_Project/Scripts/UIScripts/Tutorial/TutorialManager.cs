using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private static TutorialManager _instance;
    public static TutorialManager Instance => _instance;

    public TutorialText tutText;
    
    [SerializeField]
    public List<GameObject> allTutorials;

    [SerializeField] public List<GameObject> pauseModeTutorials;
    
    [SerializeField] public List<GameObject> infoButtons;
    
    public int tutorialIndex;
    public int pauseTutIndex;

    public bool mainTutorialComplete = false;

    private void Awake()
    {
        // Creating singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void Start()
    {
        foreach (var tutorial in allTutorials)
        {
            tutorial.gameObject.SetActive(false);
        }
        
        if (!mainTutorialComplete) allTutorials[tutorialIndex].SetActive(true);
    }

    public void IncrementTutorialIndex()
    {
        tutorialIndex++;
        if (tutorialIndex >= allTutorials.Count)
        {
            mainTutorialComplete = true;
            tutorialIndex = 0;
        }
        
        if (!mainTutorialComplete)
        {
            allTutorials[tutorialIndex].gameObject.SetActive(true);
        }
    }

    public void DisplaySpecificTutorial(int indexToDisplay)
    {
        allTutorials[indexToDisplay].gameObject.SetActive(true);
    }
}
