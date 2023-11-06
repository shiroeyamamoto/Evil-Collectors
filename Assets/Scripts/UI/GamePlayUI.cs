using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] private Slider HP;
    [SerializeField] private Slider MN;
    [SerializeField] private Slider TP;
    [SerializeField] private Button btnBackMenu;

    [SerializeField] private ResutlUI resutlUI;
    [SerializeField] private StoreUI storeUI;

    private SO_PlayerData playerData;
    public void PlayerInitData(SO_PlayerData playerData)
    {
        HP.minValue = 0;
        HP.maxValue = playerData.health;
        HP.value = playerData.health;
        
        MN.minValue = 0;
        MN.maxValue = playerData.mana;
        MN.value = 0;

        TP.minValue = 0;
        TP.maxValue = playerData.stamina;
        TP.value = playerData.stamina;
        
    }

    private void Start() {
        btnBackMenu.onClick.AddListener(GameManager.Instance.LoadSceneMenu);
        resutlUI.OnClick += () => {
            resutlUI.Hide();
            storeUI.Show();
        };
        
        resutlUI.gameObject.SetActive(false);
        storeUI.gameObject.SetActive(false);
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

    public void Player_OnDead() {
        resutlUI.Show();
        // save data
    }

}
