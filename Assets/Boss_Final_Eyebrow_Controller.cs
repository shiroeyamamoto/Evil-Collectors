using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Final_Eyebrow_Controller : MonoBehaviour
{
    [Range(1,3)]
    public int phase;



    void Update()
    {
        GetPhase();
    }

    void GetPhase()
    {
        phase = transform.parent.parent.GetComponent<BossController>().phase;
        Debug.Log(phase);
        if (phase == 1)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 40);
        }
        else if (phase == 2)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (phase == 3)
        {
            transform.localRotation = Quaternion.Euler(0, 0, -40);
        }
    }
}
