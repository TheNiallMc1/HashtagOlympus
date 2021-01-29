using UnityEngine;

// Used for basic healing. Heals a character once, immediately, and then is removed
[CreateAssetMenu(fileName = "RespectBlessing", menuName = "Status Effect/Blessing", order = 1)]
public class Status_Blessing : StatusEffect
{
    [Header("Blessing Variables")] 
    public int respectCost;
    [SerializeField] protected int statIncreaseAmount;
    
    public override void TickEffect()
    {
        throw new System.NotImplementedException();
    }

    public override void EntryEffect()
    {
        // Heal target
        Debug.Log("Cast blessing");
    }

    public override void ExitEffect()
    {
        Debug.Log("Removed blessing");
    }
}