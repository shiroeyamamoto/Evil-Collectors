using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FireBallSkill : Skill
{
    [SerializeField, Range(0,100)] private int speedBullet;

    private void Start()
    {
        BulletBase butlletBase = GetComponent<BulletBase>();
        //timeLifeSkill = Player.Instance.SkillList;
    }
    public override void ActivateSkill(int amount, float scale)
    {
        if (base.canUseSkill && !Settings.isCatingSkill && base.Unlocked)
        {
            Vector3 playerPosition = Player.Instance.transform.position;
            position = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z);
            transform.position = position;

            //scale = transform.localScale;

            this.gameObject.SetActive(true);

            FireBallStart( amount,  scale);

            //GameController.Instance.Player.UseMana(base.manaNeed);
        }
    }

    public override void ActivateSkill()
    {
        return;
    }

    private void FireBallStart(int amount, float scale)
    {
        // Hướng ngẫu nhiên bắn
        float randomAngle = Random.Range(-5f, 5f); // góc bắn
        Vector2 moveDirection = Quaternion.Euler(0, 0, randomAngle) * Vector2.right;

        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();

        gameObject.transform.localScale *= scale;

        //rb.gravityScale = 0;
        rb.velocity = moveDirection.normalized * speedBullet;
    }

    public override void HoldKeySkill()
    {
        return;
    }
}
