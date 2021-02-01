using UnityEngine;

public class GodDontDestroy : MonoBehaviour
{
    public bool isChosen;
    // Start is called before the first frame update

    private void Awake()
    {
       // if(isChosen) 
            DontDestroyOnLoad(gameObject);
    }
 
    // void OnLevelWasLoaded()
    // {
    //     //if(!isChosen) Destroy(gameObject);
    // }

    public void ChooseUnchooseThisGod(bool choice)
    {
        isChosen = choice;
        
    }
}
