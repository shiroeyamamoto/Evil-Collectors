using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamehameha : SkillBase {
    private float scale = 1;
    public override void UseToDir(Vector2 dir) {
        var bullet = Instantiate(this.bullet, transform.position, Quaternion.identity);
        bullet.ActiveToDir(dir);
    }
    
    public override void UpdateSkill(SupportItem supportItem) {
        scale *= supportItem.incSpace;
    }
}
