using UnityEngine;

public class GodDontDestroy : MonoBehaviour
{
    public bool isChosen;
    // Start is called before the first frame update
    
    void Awake()
    {
       // if(isChosen) 
            DontDestroyOnLoad(gameObject);
    }
 
    void OnLevelWasLoaded()
    {
        //if(!isChosen) Destroy(gameObject);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseUnchooseThisGod(bool choice)
    {
        isChosen = choice;
        
    }
}
