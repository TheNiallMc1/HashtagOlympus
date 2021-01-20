using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITestManager : MonoBehaviour
{

    public Transform prefab;
    public Transform left;
   // public Transform right;
    public int i = 0;
    bool enter = false;
    Coroutine timer;

    // Start is called before the first frame update
    void Start()
    {

    }

    IEnumerator timerCoroutine()
    {
        enter = true;
        yield return new WaitForSeconds(120.0f);
        enter = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer = StartCoroutine(timerCoroutine());
        while (i < 20)
        {
            if (enter == true)
            {
                Instantiate(prefab, left.position, Quaternion.identity);
                i++;
            }
            
        }
        if(enter == false)
        {
            StopCoroutine(timer);
        }

    }
}
