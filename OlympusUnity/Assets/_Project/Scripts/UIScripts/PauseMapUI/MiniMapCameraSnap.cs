using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCameraSnap : MonoBehaviour
{

    public Transform finalMonumentpos;
    // Start is called before the first frame update
    void Start()
    {
        finalMonumentpos = GameObject.Find("Final God Monument").transform;
        Debug.Log(finalMonumentpos.position.x+finalMonumentpos.position.z);
        gameObject.transform.position = new Vector3(finalMonumentpos.position.x, gameObject.transform.position.y, finalMonumentpos.position.z);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SnapToActiveGodPos()
    {
        Debug.Log("Snapping to current god pos");
        Transform currentGodPos = GameManager.Instance.currentlySelectedGod.gameObject.transform;
        
        gameObject.transform.position = new Vector3(currentGodPos.position.x, gameObject.transform.position.y, currentGodPos.position.z);
    }
}
