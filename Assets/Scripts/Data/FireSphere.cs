using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireSphere : SkillBase {
    private int amount = 1;
    public override void UseToTarget(Vector2 target) {
        for (int i = 0; i < amount; i++) {
            var bullet = Instantiate(this.bullet, transform.position, Quaternion.identity);
            bullet.ActiveToTarget(target);
        }
    }
    public override void UpdateSkill(SupportItem supportItem) {
        amount += supportItem.multiBullet;
    }
}
