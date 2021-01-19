using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITestManager : MonoBehaviour
{

    public Transform prefab;
    public Transform left;
    public Transform right;
    public int i = 0;
    bool enter = false;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    IEnumerator timer()
    {
        enter = true;
        yield return new WaitForSeconds(120.0f);
        enter = false;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("timer");
        while (i < 100)
        {
            if (enter == true)
            {
                if (i % 2 == 0)
                {
                    Instantiate(prefab, left.position, Quaternion.identity);

                }
                else
                {
                    Instantiate(prefab, right.position, Quaternion.identity);
                }
                i++;
            }
        }

    }
}
