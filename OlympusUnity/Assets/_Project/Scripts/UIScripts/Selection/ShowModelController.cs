using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowModelController : MonoBehaviour
{
    private static ShowModelController _instance;
    public static ShowModelController Instance => _instance;
    
    [SerializeField]
    public List<ModelBehaviour> models;
    public ModelBehaviour currentModel;
    
    [SerializeField]
     public List<Transform> positions;

     private GodStatDisplayController godStats;
     private GodSelectorButton selectButton;

    private void Awake()
    {
        // Creating singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        
        //models = new List<ModelBehaviour>();
        godStats = GameObject.FindObjectOfType<GodStatDisplayController>();
        selectButton = GameObject.FindObjectOfType<GodSelectorButton>();
        //positions = new List<Transform>();
    }

    private void Start()
    {
        
       // Debug.Log("intialising models");
       /* for (int i = 0; i < transform.childCount; i++)
        {
            Debug.Log("loop");
            var model = transform.GetChild(i).GetComponent<ModelBehaviour>();
            models.Add(model);

            //put this one at the others at next 2
            model.SetInitialPosition(i);
        }
        */

       for (int i = 0; i < models.Count; i++)
       {
           models[i].SetInitialPosition(i);
           //turn off outlines
       }
       
        Debug.Log("models count: "+models.Count);
        godStats.UpdateGodStatInfo(currentModel);
        selectButton.UpdateButton();
    }

    public void EnableModel()
    {
        for(int i = 0; i<models.Count;i++)
        {
            models[i].SetCurrentPosition();
            
        }
        godStats.UpdateGodStatInfo(currentModel);
        selectButton.UpdateButton();
    }

    public List<ModelBehaviour> GetModels()
    {
        return new List<ModelBehaviour>(models);
    }

    public void SetCurrentModel(ModelBehaviour modelToSet)
    {
        currentModel = modelToSet;
    }
}
