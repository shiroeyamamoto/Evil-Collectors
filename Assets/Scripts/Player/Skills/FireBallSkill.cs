using DG.Tweening;
using System;
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

    private float timeCounter;

    private void Update()
    {
        TimeLife();
    }

    public override void ActivateSkill(int amount, float scale)
    {
        if (Player.Instance.CurrentInfo.mana < manaNeed)
        {
            Destroy(this.gameObject);
            return;
        }

        if (base.canUseSkill && !Settings.isCatingSkill && base.Unlocked)
        {
            Vector3 playerPosition = Player.Instance.transform.position;
            position = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z);
            transform.position = position;

            //scale = transform.localScale;

            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;

            this.gameObject.SetActive(true);

            Player.Instance.UseMana(5);

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
        timeCounter = timeLifeSkill;
        if (Settings.isFacingRight)
        {
            gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        }

        // Hướng ngẫu nhiên bắn
        float randomAngle = UnityEngine.Random.Range(-5f, 5f); // góc bắn
        Vector2 moveDirection = Quaternion.Euler(0, 0, randomAngle) * (Player.Instance.transform.localScale.x>0? Vector2.right : Vector2.left);

        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();

        gameObject.transform.localScale *= scale;

        //rb.gravityScale = 0;
        rb.velocity = moveDirection.normalized * speedBullet;
    }

    private void TimeLife()
    {
        if(timeCounter > 0)
        {
            timeCounter -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public override void HoldKeySkill()
    {
        return;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter");

        if (collision.gameObject.layer == 3) ;
        {
            //Debug.Log("Wall");
            Destroy(this.gameObject);
        }

        if (collision.transform.GetComponent<IInteractObject>() != null)
        {
            collision.transform.GetComponent<IInteractObject>().OnDamaged(Player.Instance.CurrentInfo.damage * 0.2f,false);
            Debug.Log("AttackDamaga");
        }
    }
}
