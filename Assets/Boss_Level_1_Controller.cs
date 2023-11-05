using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Boss_Level_1_Controller : MonoBehaviour
{
    public int currentPhase;
    public int phaseMax;

    public int maxAttackType;

    Animator animator;
    public float duration;
    public float force;

    Transform damagableObject;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        currentPhase = animator.GetInteger("Phase");
        maxAttackType = maxAttackTypeOfPhase(currentPhase);
        animator.GetBehaviour<B_Boss_Attack>().maxAttackType = maxAttackType;
    }
    void ShakeCamera()
    {
        Camera.main.GetComponent<CameraController>().ShakeCamera(duration, force);
    }

    void ShakeCameraStrong()
    {
        Camera.main.GetComponent<CameraController>().ShakeCamera(duration*2, force *2);
        animator.GetBehaviour<Boss_Lv1_Ground_Shake>().SpawnDamagableObject();
    }

    [ContextMenu("Phase Up")]
    public void PhaseUp()
    {
        if(currentPhase < phaseMax)
        {
            currentPhase++; animator.SetInteger("Phase", currentPhase);
            maxAttackType = maxAttackTypeOfPhase(currentPhase);
            animator.GetBehaviour<B_Boss_Attack>().maxAttackType = maxAttackType;
        }
    }

    public int maxAttackTypeOfPhase(int currentPhase)
    {
        int attackTypeNumbs = 0;
        switch (currentPhase)
        {
            case 1:attackTypeNumbs = 2; break;
            case 2:attackTypeNumbs = 3; break;
                default:attackTypeNumbs = 2;break;

        }
        return attackTypeNumbs;
        

    }
}
