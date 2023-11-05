using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Skill : SingletonMonobehavious<Skill>
{
    [SerializeField] string skillName = "skill name";
    [SerializeField] string decription = "skill decription";

    public float manaNeed = 0;
    public bool Unlocked = false;
    [Range(0.1f, 60f)] public float timeCastSkill = 0.5f;
    [Range(0.1f, 60f)] public float timeLifeSkill = 2f;
    [Range(0.1f, 120f)] public float skillCoolDown = 1f;

    public abstract void ActivateSkill();

    public virtual void Size(int size)
    {

    }
}

