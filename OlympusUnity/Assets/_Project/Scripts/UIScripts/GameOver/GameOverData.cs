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

    private void Awake()
    {
     DontDestroyOnLoad(gameObject);   
    }
}
