using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanielPlayerScript : MonoBehaviour
{
    public ConeAoE coneAoePrefab;

    private DanielTestingKeys testKeys;
    private bool key1;
    private bool key2;
    private bool key3;

    private void Awake()
    {
        testKeys = new DanielTestingKeys();
        testKeys.Enable();

        testKeys.TestKeys.TestKey1.started += ctx => key1 = true;
        testKeys.TestKeys.TestKey2.started += ctx => key2 = true;
        testKeys.TestKeys.TestKey3.started += ctx => key3 = true;

        testKeys.TestKeys.TestKey1.canceled -= ctx => key1 = false;
        testKeys.TestKeys.TestKey2.canceled -= ctx => key2 = false;
        testKeys.TestKeys.TestKey3.canceled -= ctx => key3 = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (key3)
        {
            key3 = false;
            ConeAoE coneAoE = Instantiate(coneAoePrefab, transform.position, Quaternion.identity, transform);
        }
    }
}
