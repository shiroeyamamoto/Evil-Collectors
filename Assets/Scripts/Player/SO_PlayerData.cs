using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SO_PlayerData : ScriptableObject
{
    [SerializeField] GameObject gameObject;

    [SerializeField] float defaultHealth;
    [SerializeField] float defaultMana;
    [SerializeField] float defaultStamina;
    [SerializeField] float defaultCrit;
    [SerializeField] float defaultCritDmg;
    [SerializeField] float defaultAlibility;
    [SerializeField] float defaultDefense;

    [HideInInspector] public float health;
    [HideInInspector] public float mana;
    [HideInInspector] public float stamina;
    [HideInInspector] public float crit;
    [HideInInspector] public float critDmg;
    [HideInInspector] public float alibility;
    [HideInInspector] public float defense;

    [HideInInspector] public Vector2 currentPosititon;

    public void ResetData()
    {
        health = defaultHealth;
        mana = defaultMana;
        stamina = defaultStamina;
        crit = defaultCrit;
        critDmg = defaultCritDmg;
        alibility = defaultAlibility;
        defense = defaultDefense;

    }
}
