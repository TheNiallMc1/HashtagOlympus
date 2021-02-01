using System.Collections.Generic;
using UnityEngine;

public class ShowModelController : MonoBehaviour
{
    private static ShowModelController _instance;
    public static ShowModelController Instance => _instance;

    [SerializeField] public List<ModelBehaviour> models;
    public ModelBehaviour currentModel;

    [SerializeField] public List<Transform> positions;

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

        godStats = FindObjectOfType<GodStatDisplayController>();
        selectButton = FindObjectOfType<GodSelectorButton>();
    }

    private void Start()
    {
        for (int i = 0; i < models.Count; i++)
        {
            models[i].SetInitialPosition(i);
            models[i].ToggleOutline(false);
        }

        godStats.UpdateGodStatInfo(currentModel);
        currentModel.ToggleOutline(true);
        selectButton.UpdateButton();
    }

    public void EnableModel()
    {
        for (int i = 0; i < models.Count; i++)
        {
            models[i].SetCurrentPosition();
            models[i].ToggleOutline(false);
        }

        godStats.UpdateGodStatInfo(currentModel);
        currentModel.ToggleOutline(true);
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