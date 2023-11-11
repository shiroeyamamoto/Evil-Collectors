using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Kamehameha : SkillBase {
    private float scale = 1;

    public override void UseToDir(Vector2 dir) {
        var bullet = Instantiate(this.bullet, transform.position, Quaternion.identity);

        KamehamehaSkill skill = bullet.GetComponent<KamehamehaSkill>();
        skill.ActivateSkill(0,scale);

        bullet.ActiveToDir(dir);
    }
    
    public override void UpdateSkill(SupportItem supportItem) {
        scale *= supportItem.incSpace;
    }

}
