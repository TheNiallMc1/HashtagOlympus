using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BekahsGM : MonoBehaviour
{
    public int totalRespect;

    
    public int activeGodCount;
    public Canvas selectorScreen;
    
    private static BekahsGM _instance = null; // the private static singleton instance variable
    public static BekahsGM Instance { get { return _instance; } } // public getter property, anyone can access it!

    private void Awake()
    {
        if (_instance == null)
        {
            // if the singleton instance has not yet been initialized
            _instance = this;
        }
        else
        {
            // the singleton instance has already been initialized
            if (_instance != this)
            {
                // if this instance of GameManager is not the same as the initialized singleton instance, it is a second instance, so it must be destroyed!
                Destroy(gameObject); // watch out, this can cause trouble!
            }
        }
    }

    private GameObject _passedGameObject;
    public GameObject PassedGameObject
    {
        get => _passedGameObject;
        set
        {
            _passedGameObject = value;
            Debug.Log ( $"Receiver[{name}] just received \'{_passedGameObject.name}\'" );
        }
    }


    void OnDestroy()
    {
        if (_instance == this)
        {
            // if the this is the singleton instance that is being destroyed...
            _instance = null; // set the instance to null
        }
    }


    public List<GameObject> currentGods;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddGodToActiveList(GameObject selectedGod)
    {
        currentGods.Add(selectedGod);

        Debug.Log("Added "+selectedGod.GetComponentInChildren<GodBehaviour>().godName+" to god list");
        activeGodCount++;

        if (activeGodCount == 3)
        {
            selectorScreen.gameObject.SetActive(false);
        }

    }
    
    
}
