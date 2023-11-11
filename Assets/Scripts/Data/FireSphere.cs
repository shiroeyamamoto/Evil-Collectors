using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class FireSphere : SkillBase {
    private int amount = 1;
    private float scale = 1;
    public override void UseToTarget(Vector2 target) {
        for (int i = 0; i < amount; i++) {
            var bullet = Instantiate(this.bullet, transform.position, Quaternion.identity);
            bullet.ActiveToTarget(target);
        }
    }
    public override void UpdateSkill(SupportItem supportItem) {
        amount += supportItem.multiBullet;
        scale *= supportItem.incSpace;
    }

    public override void UseToDir(Vector2 dir)
    {
        Debug.Log("Firesphere");

        for (int i = 0; i < amount; i++)
        {
            var bullet = Instantiate(this.bullet, transform.position, Quaternion.identity);

            FireBallSkill skill = bullet.GetComponent<FireBallSkill>();
            skill.ActivateSkill(amount,scale);

            bullet.ActiveToDir(dir);
        }
    }
}
