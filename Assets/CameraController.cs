using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraController : MonoBehaviour
{
    private void Start()
    {
        ShakeCamera(0.5f,0.25f);
    }
    public void ShakeCamera(float duration,float strength)
    {
        transform.DOShakePosition(duration, strength);
    }
}
