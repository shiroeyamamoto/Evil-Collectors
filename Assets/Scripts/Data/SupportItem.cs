using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Create New Support Item", menuName = "Data/Item/SupportItem")]
public class SupportItem : ItemBase
{
    [Min(1)] public int multiBullet = 1;
    [Range(1, 2)] public float incSpace = 1f;
    public List<SkillName> supportToSkill;
    
    public override bool UseToMySelf(Player player) {
        foreach (var skill in player.SkillList) {
            if (supportToSkill.Contains(skill.skillName)) {
                skill.UpdateSkill(this);
            }
        }

        return true;
    }
}