using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespectBuff : MonoBehaviour
{
    //private bool _isTimedOut = false;

    public void Awake()
    {
      //  _isTimedOut = false;
    }
    
    public void ApplyBuff(GodBehaviour currentGod, int costToUse, ref int statToBuff, int buffAmount)
    {
        
        //_isTimedOut = false;
        // need to add a cool down to APPLYING this buff/any buff to prevent spamming

        
            Debug.Log("buffing");
            statToBuff += 10;
            GameManager.Instance.RemoveRespect(costToUse);
    }
    
    public void RemoveBuff(GodBehaviour currentGod, int costToUse, ref int statToBuff, int buffAmount)
    {
        Debug.Log("Removing buff");
        statToBuff -= buffAmount;
    }

}
