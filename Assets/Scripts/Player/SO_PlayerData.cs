using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SO_PlayerData : ScriptableObject
{
    [SerializeField] GameObject defaultGameObject;

    [HideInInspector] public GameObject gameObject;

    [SerializeField] public float health;
    [SerializeField] public float mana;
    [SerializeField] public float stamina;
    [SerializeField] public float crit;
    [SerializeField] public float critDmg;
    [SerializeField] public float alibility;
    [SerializeField] public float defense;
    [SerializeField] public float damage;
    [SerializeField] public float inventoryWeight;

    [HideInInspector] public Vector2 currentPosititon;

    [HideInInspector] public float staminaRecovery;

    public SO_PlayerData(SO_PlayerData data)
    {
        this.health = data.health;
        this.mana = data.mana;
        this.crit = data.crit;
        this.critDmg = data.critDmg;
        this.alibility = data.alibility;
        this.defense = data.defense;
        this.damage = data.damage;
        this.inventoryWeight = data.inventoryWeight;
    }
}
