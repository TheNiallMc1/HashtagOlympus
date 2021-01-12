using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMapButton : MonoBehaviour
{
    
     
    public Camera mainCam;
    public Camera pauseMapCam;

    public Canvas playModeUI;
    public Canvas pauseModeUI;

    void Start()
    {
        mainCam = Camera.main;
    }

    public void PauseShowMap()
    {
        //sTime.timeScale = 0;
        
        playModeUI.gameObject.SetActive(false);
        pauseModeUI.gameObject.SetActive(true);
        
        //mainCam.gameObject.SetActive(false);
        pauseMapCam.gameObject.SetActive(true);
    }

    public void UnpauseHideMap()
    {
       // Time.timeScale = 1;
        
        pauseModeUI.gameObject.SetActive(false);
        playModeUI.gameObject.SetActive(true);
        
        pauseMapCam.gameObject.SetActive(false);
        //mainCam.gameObject.SetActive(true);
    }
}
