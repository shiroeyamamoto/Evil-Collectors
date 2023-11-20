using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Lv1_Eyebrow_Controller : MonoBehaviour
{
    [Range(1,2)]
    public float phase;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        GetPhase();
    }

    void GetPhase()
    {
        phase = transform.parent.GetComponent<Boss_Level_1_Controller>().currentPhase;
        if(phase == 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        } else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 30);
        }
    }
}
