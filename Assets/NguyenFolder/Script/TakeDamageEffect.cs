using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum TakeDamageEffectEnum {
    ChangeColor,Flash
}
public class TakeDamageEffect : MonoBehaviour
{
    public Action<Color> action;
    public Action<Color> actionStop;

    public void DoEffect(TakeDamageEffectEnum effect, float time, Color oldColor,Color newColor)
    {
        StartCoroutine(OnEffect(effect, time, oldColor, newColor));
    }

    public IEnumerator OnEffect(TakeDamageEffectEnum effect, float time, Color oldColor, Color newColor)
    {
        switch (effect)
        {
            case TakeDamageEffectEnum.ChangeColor:
                action = ChangeColor; actionStop = StopColor; break;
                case TakeDamageEffectEnum.Flash: action = Flash; actionStop = StopFlash; break;
                default: break;
        }
        
        action(newColor);
        yield return new WaitForSeconds(time);
        actionStop(oldColor);
    }

    public void ChangeColor(Color newColor)
    {
        transform.Find("Body").GetComponent<SpriteRenderer>().color = newColor;
        Debug.Log("Start Change Color");
    }

    public void Flash(Color newColor)
    {
        Debug.Log("start change material");
    }

    public void StopColor(Color oldColor)
    {
        transform.Find("Body").GetComponent<SpriteRenderer>().color = oldColor;
        Debug.Log("Stop Change Color");
    }

    public void StopFlash(Color oldColor)
    {
        Debug.Log("Stop change material");
    }
}
