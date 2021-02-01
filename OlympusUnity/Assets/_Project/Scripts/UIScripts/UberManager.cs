using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UberManager : MonoBehaviour
{
    
    private static UberManager _instance;
    public static UberManager Instance => _instance;
    
    public enum GameState
    {
        MainMenu,
        LevelSelect,
        GodSelect,
        GodPlacement,
        GamePlay,
        GameOver
    }

    public GameState currentGameState;
    
    public List<GodBehaviour> selectedGods;
    
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
        
        //DontDestroyOnLoad(gameObject);
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        currentGameState = GameState.MainMenu;
        SwitchGameState(currentGameState);
    }

    public void SwitchGameState(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu:
                break;
            case GameState.LevelSelect:
                break;
            case GameState.GodSelect: LoadGodSelect();
                break;
            case GameState.GodPlacement: LoadGodPlacement();
                break;
            case GameState.GamePlay: LoadGamePlay();
                break;
            case GameState.GameOver:
                break;
            
            
        }
    }

    private static void LoadGodSelect()
    {
        SceneManager.LoadScene("SelectionScene");
    }

    private static void LoadGodPlacement()
    {
        SceneManager.LoadScene("PlacementScene");
       // GameManager.Instance.SetPlayerGods(selectedGods);
    }

    private static void LoadGamePlay()
    {
        Debug.Log("Loading game play");
        SceneManager.LoadScene("PlayScene");
    }
}
