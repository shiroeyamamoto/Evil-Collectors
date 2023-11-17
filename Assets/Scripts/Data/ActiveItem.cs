using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "Create New Active Item", menuName = "Data/Item/ActiveItem")]

[Serializable]
public class ActiveItem : ItemBase {
    [SerializeField] private SkillName skillName;
    [SerializeField] private BulletBase bullet;
    [SerializeField] private float duration = 0;
    [SerializeField] private List<SkillTag> tags;
    public GameObject skillBase;
    /*public override bool UseToMySelf(Player player)
    {
        player.AddSkill(this);
        return true;
    }*/

    public SkillName SkillName => skillName;
    public List<SkillTag> Tags => tags;
    public BulletBase Bullet => bullet;
    public float Duration => duration;

    public override void UseItem()
    {
        Debug.Log("ActiveItem");
        skillBase.GetComponent<SkillBase>().Init(this);
        skillBase.GetComponent<SkillBase>().UseToDir(Player.Instance.transform.position);
    }

}
