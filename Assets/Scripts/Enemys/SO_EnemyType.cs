using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SO_EnemyType : ScriptableObject
{
    [SerializeField] EnemyTypes enemyType = EnemyTypes.normal;
    [SerializeField] NormalEnemy normalEnemy = NormalEnemy.None;
    [SerializeField] GhostEnemy ghostEnemy = GhostEnemy.None;
    [SerializeField] BossEnemy bossEnemy = BossEnemy.None;
    [SerializeField, Range(0f, 100f)] private float enemyDamage = 20f; 

    [HideInInspector] public float health;
    [HideInInspector] public float mana;
    [HideInInspector] public float stamina;
    [HideInInspector] public float crit;
    [HideInInspector] public float critDmg;
    [HideInInspector] public float alibility;
    [HideInInspector] public float defense;
    [HideInInspector] public float damage;

    public SO_EnemyData enemyData;

    /// <summary>
    /// Khi awake kiểm tạo dữ liệu cho enemy
    /// </summary>
    public void EnemySpawn()
    {
        if(enemyType == EnemyTypes.normal)
        {
            switch (normalEnemy)
            {
                case NormalEnemy.Macho: EnemyDataUpdate(1.8f);  break;
                case NormalEnemy.MaCoDai: EnemyDataUpdate(0.5f); break;
                case NormalEnemy.MaHaiMat: EnemyDataUpdate(1.2f); break;
                case NormalEnemy.QuyNhapTrang: EnemyDataUpdate(0.8f); break;
            }
        }
        if (enemyType == EnemyTypes.ghost)
        {
            switch (ghostEnemy)
            {
                case GhostEnemy.MaTroi: EnemyDataUpdate(1.8f); break;
                case GhostEnemy.MaXo: EnemyDataUpdate(1.8f); break;
            }
        }
        if (enemyType == EnemyTypes.boss)
        {
            switch (bossEnemy)
            {
                case BossEnemy.MeoChinDuoi: EnemyDataUpdate(5f); break;
            }
        }
    }
    
    /// <summary>
    /// Data enemy tương ứng với loại quái
    /// </summary>
    /// <param name="x"></param>
    private void EnemyDataUpdate(float x)
    {
        health = enemyData.defaultHealth * x;
        mana = enemyData.defaultMana * x;
        stamina = enemyData.defaultStamina * x;
        crit = enemyData.defaultCrit * x;
        critDmg = enemyData.defaultCritDmg * x;
        alibility = enemyData.defaultAlibility * x;
        defense = enemyData.defaultDefense * x;
        damage = enemyDamage;
    }
}
