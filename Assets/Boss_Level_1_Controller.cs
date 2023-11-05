using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Boss_Level_1_Controller : MonoBehaviour
{
    Animator animator;
    public float duration;
    public float force;

    Transform damagableObject;
    private void Awake()
    {
        animator = GetComponent<Animator>();
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
}
