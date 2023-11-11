using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Create New Support Item", menuName = "Data/Item/SupportItem")]
public class SupportItem : ItemBase
{
    [Min(0)] public int multiBullet = 0;
    [Range(1, 2)] public float incSpace = 1f;
    public List<SkillTag> supportToTags;
    
    public override bool UseToMySelf(Player player) {
        foreach (var skill in player.SkillList) {
            foreach (var skillTag in skill.SkillTag) {
                if (supportToTags.Contains(skillTag)) {
                    skill.UpdateSkill(this);
                    break;
                }
            }
            
        }

        return true;
    }
}