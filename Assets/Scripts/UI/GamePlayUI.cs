using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] private Slider HP;
    [SerializeField] private Slider MN;
    [SerializeField] private Slider TP;

    private SO_PlayerData playerData;
    
    public void PlayerInitData(SO_PlayerData playerData)
    {
        HP.minValue = 0;
        HP.maxValue = playerData.health;
        HP.value = playerData.health;
        
        MN.minValue = 0;
        MN.maxValue = playerData.mana;
        MN.value = playerData.mana;

        TP.minValue = 0;
        TP.maxValue = playerData.stamina;
        TP.value = playerData.stamina;
    }

    public void Player_OnUpdateMana(float value)
    {
        MN.value = value;
    }
    
    
    public void Player_OnUpdateHP(float value)
    {
        HP.value = value;
    }
    
    public void Player_OnUpdateTP(float value)
    {
        TP.value = value;
    }
}
