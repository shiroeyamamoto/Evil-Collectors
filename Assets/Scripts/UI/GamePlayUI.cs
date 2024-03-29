﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : SingletonMonobehavious<GamePlayUI>
{
    [SerializeField] private GameObject HP;
    [SerializeField] private Slider MN;
    [SerializeField] private Slider TP;
    [SerializeField] private Button btnBackMenu;
    [SerializeField] private Image hpTwinklingLight;
    [SerializeField] private Image hpTwinklingStrong;
    [SerializeField] private Image iconSwitchFirst;
    [SerializeField] private Image iconSwitchSecond;

    [SerializeField] private ResutlUI resutlUI;
    [SerializeField] private StoreUI storeUI;

    private SO_PlayerData playerData;
    int currentIndex;
    public void PlayerInitData(SO_PlayerData playerData)
    {
        //HP.minValue = 0;
        //HP.maxValue = playerData.health;
        //HP.value = playerData.health;

        currentIndex = playerData.health;

        for(int i=0; i < currentIndex; i++)
        {
            Image keyImage = HP.transform.Find($"{i+1}").gameObject.GetComponent<Image>();

            keyImage.color = Color.red;

            keyImage.gameObject.SetActive(true);
        }
        
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
            GameManager.Instance.LoadSceneMenu();

            // origin of Quan dev
            //storeUI.Show();
        };
        
        resutlUI.gameObject.SetActive(false);
        storeUI.gameObject.SetActive(false);
        iconSwitchSecond.gameObject.transform.parent.gameObject.transform.Find("Ignore").gameObject.SetActive(false);
    }

    public void Player_OnUpdateMana(float value)
    {
        MN.value = value;
    }
    
    
    public void Player_OnUpdateHP(float value)
    {
        //HP.value = value;

        //Debug.Log("Mất màus rồi;");

        Image lostHP = HP.transform.Find($"{value+1}").gameObject.GetComponent<Image>();
        lostHP.color = Color.black;

    }
    
    public void Player_OnUpdateTP(float value)
    {
        TP.value = value;
    }

    public void Player_OnDead(bool value) {

        if (resutlUI.gameObject.activeSelf)
            return;

        if (!value && !resutlUI.gameObject.activeSelf)
            resutlUI.transform.Find("YouDie").gameObject.SetActive(true);
        else
        {
            resutlUI.transform.Find("VICTORY").gameObject.SetActive(true);
            GameManager.Instance.levelTwoScene = true;
        }
        resutlUI.Show();
        // save data
    }

    public void Player_OnDamaged(int value)
    {
        if (Player.Instance.undeadCounter > 0)
        {
            hpTwinklingLight.enabled = true;
            //hpTwinklingStrong.enabled = true;

            if (value == 1)
                hpTwinklingStrong.enabled = true;
            else
                if (value == 2)
                hpTwinklingStrong.enabled = false;
        }
        else
        {
            hpTwinklingLight.enabled = false;
            hpTwinklingStrong.enabled = false;
        }
    }
    public void UI_IconSwitchKey(Sprite value1, Sprite value2, bool canUse)
    {
        //Debug.Log("Toi o day");

        if (!canUse)
        {
            Color orinalColor = iconSwitchSecond.gameObject.transform.parent.gameObject.GetComponent<Image>().color;

            iconSwitchSecond.gameObject.transform.parent.gameObject.transform.Find("Ignore").gameObject.SetActive(true);

            float alphaColor = orinalColor.a;
            iconSwitchSecond.gameObject.transform.parent.gameObject.GetComponent<Image>().color = new Color(orinalColor.r, orinalColor.g, orinalColor.b, 0.5f);
        }
        else if(canUse)
        {
            Color orinalColor = iconSwitchSecond.gameObject.transform.parent.gameObject.GetComponent<Image>().color;

            iconSwitchSecond.gameObject.transform.parent.gameObject.transform.Find("Ignore").gameObject.SetActive(false);

            float alphaColor = orinalColor.a;
            iconSwitchSecond.gameObject.transform.parent.gameObject.GetComponent<Image>().color = new Color(orinalColor.r, orinalColor.g, orinalColor.b, 1f);
        }

        Debug.Log($"CanUseQuickKey: {canUse}");

        iconSwitchFirst.sprite = value2;
        iconSwitchSecond.sprite = value1;
    }
}
