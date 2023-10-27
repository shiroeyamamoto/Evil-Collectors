using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraController : MonoBehaviour
{
    public void ShakeCamera(float duration,float strength)
    {
        transform.DOShakePosition(duration, strength);
    }
}
