using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : SingletonMonobehavious<GameController>
{
    [Header("UI")] 
    [SerializeField] private GamePlayUI gamePlayUI;

    [Header("Prefab")]
    [SerializeField] private Player playerPrefab;
    
    public Player Player { get; private set; }
    public LevelSO LevelSO => GameManager.Instance.CurrentLevelSelection;
    
    private void Start()
    {
        Player = Instantiate(playerPrefab);
        Player.Init(LevelSO.playerData);
        Player.OnUpdateMana += gamePlayUI.Player_OnUpdateMana;
        Player.OnUpdateHP += gamePlayUI.Player_OnUpdateHP;
        Player.OnUpdateTP += gamePlayUI.Player_OnUpdateTP;
         
        gamePlayUI.PlayerInitData(LevelSO.playerData);
    }
}
