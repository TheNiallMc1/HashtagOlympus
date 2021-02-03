using UnityEngine;
using TMPro;

public class GodStatDisplayController : MonoBehaviour
{
    
        [SerializeField] [Header("Components")]
        public TMP_Text gHealthText;
        public TMP_Text gDamageText;
        public TMP_Text gArmourText;

        //identify active panel
        public GameObject currentPanel;

        private void Awake()
        {
            
        }

        public void UpdateGodStatInfo(ModelBehaviour currentModel)
        {
            currentPanel = ShowModelController.Instance.currentPanel;

            gHealthText = currentPanel.GetComponent<GodStatDisplayComponents>().gHealth;
            gDamageText = currentPanel.GetComponent<GodStatDisplayComponents>().gDamage;
            gArmourText = currentPanel.GetComponent<GodStatDisplayComponents>().gArmour;
            
            gHealthText.text = currentModel.godHealth;
            gDamageText.text = currentModel.godDamage;
            gArmourText.text = currentModel.godArmour;
        }
}
