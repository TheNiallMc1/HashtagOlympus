using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class TutorialText : MonoBehaviour
{

    [Header("Tutorial Text")]
    public string header;
    [TextArea(5,20)]
    public string body;

    public TMP_Text headerText;
    public TMP_Text bodyText;
    
        
    // Start is called before the first frame update
    void Start()
    {
        headerText.text = header;
        bodyText.text = body;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
