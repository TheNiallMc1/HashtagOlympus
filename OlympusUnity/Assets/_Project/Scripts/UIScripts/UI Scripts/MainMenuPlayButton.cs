using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPlayButton : MonoBehaviour
{

    public void LoadNextScene()
    {
        UberManager.Instance.SwitchGameState(UberManager.GameState.GodSelect);
    }
}
