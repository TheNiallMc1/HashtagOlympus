using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITestManager : MonoBehaviour
{

    public Transform prefab;
    public Transform left;
    public Transform right;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 50; i++)
        {
            if (i % 2 == 0)
            {
                Instantiate(prefab, left.position, Quaternion.identity);

            }
            else
            {
                Instantiate(prefab, right.position, Quaternion.identity);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
