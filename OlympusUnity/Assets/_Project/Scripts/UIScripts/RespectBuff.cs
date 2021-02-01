using UnityEngine;

public class RespectBuff : MonoBehaviour
{
    public void ApplyBuff(Combatant currentGod, int costToUse, ref int statToBuff)
    {
        statToBuff += 10;
        GameManager.Instance.RemoveRespect(costToUse);
    }

    public void RemoveBuff(Combatant currentGod, int costToUse, ref int statToBuff, int buffAmount)
    {
        statToBuff -= buffAmount;
    }
}