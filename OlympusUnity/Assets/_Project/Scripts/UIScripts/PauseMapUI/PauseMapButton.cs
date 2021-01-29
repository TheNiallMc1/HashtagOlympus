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
        Time.timeScale = 0;
        
        playModeUI.gameObject.SetActive(false);
        pauseModeUI.gameObject.SetActive(true);
        
        pauseMapCam.gameObject.SetActive(true);
        
        GameManager.Instance.SwitchCam(pauseMapCam);
    }

    public void UnpauseHideMap()
    {
       Time.timeScale = 1;
        
        pauseModeUI.gameObject.SetActive(false);
        playModeUI.gameObject.SetActive(true);
        
        pauseMapCam.gameObject.SetActive(false);
        
        GameManager.Instance.SwitchCam(mainCam);
        
    }
}
