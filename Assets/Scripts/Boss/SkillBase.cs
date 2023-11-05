using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : MonoBehaviour {
    public BulletBase bulletBase;
    public Collider collider;
    public virtual void UseToDir(Vector2 dir) { }
    public virtual void UseToTarget(Vector2 target) { }
    public virtual void UpdateSkill(SupportItem supportItem) { }
    
    
    
    
}

public enum BoosterType
{
    FireSphere,
    Kamehameha
}

public class FireSphere : SkillBase {
    private int amount = 1;
    public override void UseToTarget(Vector2 target) {
        for (int i = 0; i < amount; i++) {
            var bullet = Instantiate(bulletBase, transform.position, Quaternion.identity);
            bullet.ActiveToTarget(target);
        }
    }
    public override void UpdateSkill(SupportItem supportItem) {
        amount += supportItem.multiBullet;
    }
}

public class Kamehameha : SkillBase {
    private float scale = 1;
    public override void UseToDir(Vector2 dir) {
        var bullet = Instantiate(bulletBase, transform.position, Quaternion.identity);
        bullet.ActiveToDir(dir);
    }
    
    public override void UpdateSkill(SupportItem supportItem) {
        scale *= supportItem.incSpace;
    }
}
