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
    public List<ItemBase> ItemBases => GameManager.Instance.CurrentItemsInventory;

    private void Start()
    {
        Player = Instantiate(playerPrefab);
        Player.Init(LevelSO.playerData);
        /*foreach (var item in ItemBases) {
            if (item as ActiveItem) {
                item.UseToMySelf(Player);
            }
        }*/

        /*foreach (var item in ItemBases) {
            if (item.isAutoUse) {
                item.UseToMySelf(Player);
                //item.UseToMyTeam(playerTeam);
                //item.UseToTarget(player);
                //item.UseToTargetTeam(playerTeam);
            }
        }*/

        /*foreach (var item in ItemBases)
        {
            Debug.Log($"{item.itemName} - {item.itemTag}");
        }*/

        Player.OnUpdateMana += gamePlayUI.Player_OnUpdateMana;
        Player.OnUpdateHP += gamePlayUI.Player_OnUpdateHP;
        Player.OnUpdateTP += gamePlayUI.Player_OnUpdateTP;
        Player.OnDead += gamePlayUI.Player_OnDead;
        Player.OnDamagedTwinkling += gamePlayUI.Player_OnDamaged;
        ItemSwitcher.Instance.OnIconSwitch += gamePlayUI.UI_IconSwitchKey;
        ItemSwitcher.Instance.Init();

        gamePlayUI.PlayerInitData(LevelSO.playerData);
    }
}
