using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillBase : MonoBehaviour {
    public BulletBase bullet;
    public float duration;

    public virtual void UseToDir(Vector2 dir) { }
    public virtual void UseToTarget(Vector2 target) { }
    public virtual void UpdateSkill(SupportItem supportItem) { }
    public SkillName skillName { get; protected set; }
    public void Init(ActiveItem activeItem) {
        this.bullet = activeItem.Bullet;
        this.skillName = activeItem.SkillName;
    }
}
[Serializable]
public enum SkillName
{
    none,
    FireSphere,
    Kamehameha
}
