using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "Create New Active Item", menuName = "Data/Item/ActiveItem")]
public class ActiveItem : ItemBase {
    [SerializeField] private BoosterType type;
    [SerializeField] private bool isToTarget = false;
    [SerializeField] private bool isToDir = false;

    public override bool UseToMySelf(Player player)
    {
        player.AddSkill(type);
        return true;
    }

    public BoosterType Type => type;
    public bool IsToTarget => isToTarget;
    public bool IsToDir => isToDir;
}
