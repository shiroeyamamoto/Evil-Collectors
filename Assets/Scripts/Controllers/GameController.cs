using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : SingletonMonobehavious<GameController>
{
    [Header("UI")] 
    [SerializeField] private GamePlayUI gamePlayUI;

    [Header("Data")]
    
    [SerializeField] private Player playerPrefab;
    [SerializeField] private SO_PlayerData playerData;
    
    
    
    public Player Player { get; private set; }
    public SO_PlayerData PlayerData => playerData;
    private void Start()
    {
        Player = Instantiate(playerPrefab);
        
        Player.Init(playerData);
        Player.OnUpdateMana += gamePlayUI.Player_OnUpdateMana;
        Player.OnUpdateHP += gamePlayUI.Player_OnUpdateHP;
        Player.OnUpdateTP += gamePlayUI.Player_OnUpdateTP;
         
        gamePlayUI.PlayerInitData(playerData);
    }
}
