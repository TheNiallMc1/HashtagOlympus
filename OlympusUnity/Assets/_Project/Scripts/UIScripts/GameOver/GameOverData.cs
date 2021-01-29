using UnityEngine;

public class GameOverData : MonoBehaviour
{
    public enum GameOverCondition
    {
        Win,
        Lose
    }

    //private static GameOverCondition wonOrLost {get; set;}
    //private static int totalRespect {get; set;}

    public GameOverCondition wonOrLost;
    public int totalRespect;

    void Awake()
    {
     DontDestroyOnLoad(gameObject);   
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
