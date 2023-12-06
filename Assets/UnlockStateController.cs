using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockStateController : MonoBehaviour
{
    public Color curentColor;
    [Header("Color Phase")]
    public Color notUnlockColor;
    public Color noDefeatColor;
    public Color defeatedColor;

    [Space]
    // variant return if boss is unlock
    public bool isUnlocked;
    public bool isDefeatedBoss;

    [Space]
    public BossStatus_SO bsso;
    public void Awake()
    {
        if(bsso == null)
        {
            Debug.LogError("Not Found Bsso");
            return;
        }
        // get unlocked Status
        isUnlocked = bsso.unlocked;
        // get boss defeated
        isDefeatedBoss = bsso.defeated;
        // Set Color Status
        curentColor = notUnlockColor;
        curentColor = (!isUnlocked) ? notUnlockColor : ((!isDefeatedBoss) ? noDefeatColor : defeatedColor);

        transform.GetComponent<SpriteRenderer>().color = curentColor;
    }
}
