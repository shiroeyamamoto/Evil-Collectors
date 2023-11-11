using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LEVEL_", menuName = "Data/Level")]
public class LevelSO : ScriptableObject {
    public SO_PlayerData playerData;
    public SO_EnemyData enemyData;
}
