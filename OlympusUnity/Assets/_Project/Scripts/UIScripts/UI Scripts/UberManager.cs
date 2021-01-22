using System.Collections;
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
    
    public int selectedLevel;
    public List<GodBehaviour> selectedGods;
    public int totalRespect;
    
    //structs for god stats, upgrades?
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    // Start is called before the first frame update
    void Start()
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

        currentGameState = GameState.MainMenu;
        SwitchGameState(currentGameState);
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void LoadMainMenu()
    {
        
    }

    public void LoadGodSelect()
    {
        SceneManager.LoadScene("SelectionScene");
    }
    
    public void LoadGodPlacement()
    {
        SceneManager.LoadScene("PlacementScene");
       // GameManager.Instance.SetPlayerGods(selectedGods);
    }

    public void LoadGamePlay()
    {
        Debug.Log("Loading game play");
        SceneManager.LoadScene("GamePlay");
    }
    public void AddSelectedGodList(List<GodBehaviour> finalGodSelections)
    {
        selectedGods = finalGodSelections;
        currentGameState = GameState.GodPlacement;
        SwitchGameState(currentGameState);
    }
}
