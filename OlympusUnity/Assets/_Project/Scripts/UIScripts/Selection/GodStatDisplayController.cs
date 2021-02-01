using UnityEngine;
using TMPro;

public class GodStatDisplayController : MonoBehaviour
{
     [SerializeField] [Header("Original Field Text")]
        public string origGodName;
        public string origGodHealth;
        public string origGodDamage;
        public string origGodArmour;
        public string origGodAbility1;
        public string origGodAbility2;
        public string origGodUltimate;
    
        [SerializeField] [Header("Components")]
        public TMP_Text gNameText;
        public TMP_Text gHealthText;
        public TMP_Text gDamageText;
        public TMP_Text gArmourText;
        public TMP_Text gAb1Text;
        public TMP_Text gAb2Text;
        public TMP_Text gUltText;

        private void Awake()
        {
            origGodHealth = gHealthText.text;
            origGodDamage = gDamageText.text;
            origGodArmour = gArmourText.text;
            origGodAbility1 = gAb1Text.text;
            origGodAbility2 = gAb2Text.text;
            origGodUltimate = gUltText.text;
        }

        public void UpdateGodStatInfo(ModelBehaviour currentModel)
        {
            Debug.Log("updating stats");
            gNameText.text = origGodName + currentModel.godName;
            gHealthText.text = origGodHealth + currentModel.godHealth;
            gDamageText.text = origGodDamage + currentModel.godDamage;
            gArmourText.text = origGodArmour + currentModel.godArmour;
            gAb1Text.text = origGodAbility1 + currentModel.godAbility1;
            gAb2Text.text = origGodAbility2 + currentModel.godAbility2;
            gUltText.text = origGodUltimate + currentModel.godUltimate;
        }
}
