using UnityEngine;

public class RespectBuff : MonoBehaviour
{
    public void ApplyBuff(Combatant currentGod, int costToUse, ref int statToBuff)
    {
        
        //_isTimedOut = false;
        // need to add a cool down to APPLYING this buff/any buff to prevent spamming

        
        Debug.Log("buffing");
        statToBuff += 10;
            GameManager.Instance.RemoveRespect(costToUse);
    }
    
    public void RemoveBuff(Combatant currentGod, int costToUse, ref int statToBuff, int buffAmount)
    {
        Debug.Log("Removing buff");
        statToBuff -= buffAmount;
    }

}
