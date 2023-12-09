using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class CameraController : MonoBehaviour
{
    public void ShakeCamera(float duration, float strength)
    {
        transform.DOShakePosition(duration, strength);
    }

    public void ShakeCamera(float duration, float strength, float delay)
    {
        transform.DOShakePosition(duration, strength).SetDelay(delay);
    }

    public void ShakeCamera(float duration, float strength, float delay, Ease ease)
    {
        transform.DOShakePosition(duration, strength).SetDelay(delay).SetEase(ease);
    }

    public void ShakeCamera(float duration, float strength, float delay, Ease ease, Action OnComplete)
    {
        transform.DOShakePosition(duration, strength).SetDelay(delay).SetEase(ease).OnComplete(() => OnComplete());
    }
}
