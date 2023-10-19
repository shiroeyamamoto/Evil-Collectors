using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy Data", order =2)]
public class SO_EnemyData : ScriptableObject
{
    public float defaultHealth;
    public float defaultMana;
    public float defaultStamina;
    public float defaultCrit;
    public float defaultCritDmg;
    public float defaultAlibility;
    public float defaultDefense;
}
