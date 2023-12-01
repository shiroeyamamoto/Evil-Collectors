using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDataReset : MonoBehaviour
{
    public List<BossStatus_SO> listBsso;
    
    public void ResetBossData()
    {
        if(listBsso.Count !=0) {
            foreach(BossStatus_SO bsso in listBsso)
            {
                bsso.unlocked = false;
                bsso.defeated = false;
            }
            listBsso[0].unlocked = true;
        }
    }
}
