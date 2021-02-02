using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSwitchButton : MonoBehaviour
{
    public void SnapToCurrentGod()
    {Debug.Log("main clciked!");
        CameraController.Instance.FollowPlayer(GameManager.Instance.currentlySelectedGod);
    }
}
