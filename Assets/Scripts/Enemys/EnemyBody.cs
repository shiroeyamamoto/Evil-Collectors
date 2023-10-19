using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    private float currentHealth;
    private float currentMana;
    private float currentStamina;
    private float currentCrit;
    private float currentCritDmg;
    private float currentAlibility;
    private float currentDefense;

    [SerializeField] SO_EnemyType enemyType;
    [SerializeField] SO_EnemyData enemyData;

    private void Start()
    {
        enemyType.EnemySpawn();

        currentHealth = enemyType.health;
        currentMana = enemyType.mana;
        currentStamina = enemyType.stamina;
        currentCrit = enemyType.crit;
        currentCritDmg = enemyType.critDmg;
        currentAlibility = enemyType.alibility;
        currentDefense = enemyType.defense;
    }

    private void Update()
    {
        Debug.Log("Enemy max health: "+enemyType.health);
        Debug.Log("Enemy current health: " + currentHealth);
        Debug.Log("Enemy default heath: " + enemyData.defaultHealth);
        EnemyDie();
    }
    /// <summary>
    /// Enemy chết
    /// </summary>
   public void EnemyDie()
    {
        if (currentHealth <= 0)
            Destroy(this.gameObject);
    }

    /// <summary>
    /// Enemy Nhận damage
    /// </summary>
    /// <param name="dmg"></param>
    public void EnemyTakeDamge(float dmg)
    {
        if (currentHealth > 0)
            currentHealth -= dmg;
    }
}
