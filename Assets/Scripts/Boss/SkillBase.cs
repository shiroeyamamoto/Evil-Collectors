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
    public List<SkillTag> SkillTag { get; protected set; }
    public void Init(ActiveItem activeItem) {
        this.bullet = activeItem.Bullet;
        this.SkillTag = activeItem.Tags;
    }
}   

[Serializable]
public enum SkillTag
{
    none,
    attack, 
    magic, 
    projectile, 
    fire, 
    instance, 
    channeling, 
    light, 
    aoe, 
    spell
}

public enum SkillName
{
    none,
    FireSphere,
    Kamehameha
}
