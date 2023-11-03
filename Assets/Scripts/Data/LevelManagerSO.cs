using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LEVEL_Manager", menuName = "Data/LevelManager")]
public class LevelManagerSO : ScriptableObject
{
    [SerializeField] private List<LevelSO> levels;
    public List<LevelSO> Levels => levels;
}
