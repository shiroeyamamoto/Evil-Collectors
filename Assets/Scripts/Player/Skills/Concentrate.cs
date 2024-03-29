﻿using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Concentrate : Skill
{
    [SerializeField, Range(10, 200)] private int extendSpeed = 1; // Tốc độ kéo dài
    [SerializeField, Range(10, 100)] private int maxSize = 10;
    [SerializeField, Range(0f, 5f)] private float pushForce = 0.5f;

    [SerializeField, Range(0f, 5f)] private float timeExitAura =0.5f;

    private int currentSize = 0;
    private float timeAuraCounter;

    private float timeToExhaust = 10f; // Thời gian để hết stamina (ví dụ: 10 giây)
    private float staminaDecreaseRate; // Tốc độ giảm stamina mặc định
    private float decreaseInterval; // Thời gian giảm stamina (ví dụ: 1 giây)
    private float elapsedTime = 0f; // Thời gian đã trôi qua
    private Color gameObjectColor;

    void Start()
    {
        decreaseInterval = timeLifeSkill / 100f;
    }
    int x = 0, y=0,z=0;
    private void Update()
    {
        

        if (timeAuraCounter > 0)
            timeAuraCounter -= Time.deltaTime;
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }


        if(timeToExhaust > 0)
        {
            elapsedTime += Time.deltaTime;
            timeToExhaust -= Time.deltaTime;

            if (elapsedTime > decreaseInterval)
            {
                elapsedTime = 0f;

                if (timeToExhaust > (timeLifeSkill * (2f / 3f)) && timeToExhaust < timeLifeSkill)
                {
                    // Trong 1/3 thời gian đầu, stamina mặc định sẽ trừ bằng 20% stamina max
                    staminaDecreaseRate = ((Player.Instance.InfoDefaultSO.stamina * (1f / 5f)) * (1f / 33.3f));

                    Player.Instance.UseStamina(staminaDecreaseRate);
                    x++;
                    //Debug.Log(x);
                }
                else if (timeToExhaust > (timeLifeSkill * (1f / 3f)) && timeToExhaust < (timeLifeSkill * (2f / 3f)))
                {
                    // Trong khoảng 1/3 - 2/3 thời gian, stamina mặc định sẽ trừ bằng 35% stamina max
                    staminaDecreaseRate = ((Player.Instance.InfoDefaultSO.stamina * (35f / 100f)) * (1f / 33.3f));

                    Player.Instance.UseStamina(staminaDecreaseRate);
                    y++;
                    //Debug.Log(y);
                }
                else if (timeToExhaust < (timeLifeSkill * (1f / 3f)))
                {
                    // Trong khoảng <1/3 thời gian, stamina mặc định sẽ trừ bằng 35% stamina max
                    staminaDecreaseRate = ((Player.Instance.InfoDefaultSO.stamina * (45f / 100f)) * (1f / 33.3f));

                    Player.Instance.UseStamina(staminaDecreaseRate);
                    z++;
                    //Debug.Log(z);
                }
            }
        }
    }


   /* private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Rigidbody2D enemyRigid2D = collision.gameObject.GetComponent<Rigidbody2D>();

            // Đẩy kẻ địch khi đánh trúng
            if (Player.Instance.transform.position.x < collision.transform.position.x)
            {
                enemyRigid2D.velocity = new Vector2(collision.gameObject.transform.localScale.x * pushForce, 0f);
            }
            else if (Player.Instance.transform.position.x > collision.transform.position.x)
            {
                enemyRigid2D.velocity = new Vector2(-collision.gameObject.transform.localScale.x * pushForce, 0f);
            }

            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Player.Instance.NoneDamage();
        }
    }*/


    public override void ActivateSkill()
    {
        if (Player.Instance.CurrentInfo.health > 1)
            return;

        if (base.canUseSkill && !Settings.isCatingSkill && base.Unlocked)
        {
            Vector3 playerPosition = Player.Instance.transform.position;
            position = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z);
            transform.position = position;

            scale = transform.localScale;

            this.gameObject.SetActive(true);

            gameObjectColor = gameObject.GetComponent<SpriteRenderer>().color;

            StartCoroutine(ConcentrateStart());

            GameController.Instance.Player.UseMana(base.manaNeed);
        }
    }

            

    private IEnumerator ConcentrateStart()
    {
        // niệm phép 
        Settings.isAttacking = true;
        Settings.isCatingSkill = true;

        Player.Instance.spriteRendererPlayer.color = Color.grey;

        yield return new WaitForSeconds(base.timeCastSkill);

        // Bắt đầu cast phép
        base.canUseSkill = false;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        Player.Instance.spriteRendererPlayer.color = Color.white;
        Player.Instance.spriteRendererPlayer.material.color = Color.green * 3f;

        timeAuraCounter = timeExitAura;

        float alphaColor = 0;

        while (maxSize > currentSize)
        {
            float newScaleY = transform.localScale.x + Time.deltaTime * extendSpeed;

            transform.localScale = new Vector3(newScaleY, transform.localScale.y, transform.localScale.z);

            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObjectColor.r, gameObjectColor.g,gameObjectColor.b, alphaColor+0.01f);
            alphaColor+= 0.01f;
            Debug.Log("this.gameObject.GetComponent<SpriteRenderer>().color: " + this.gameObject.GetComponent<SpriteRenderer>().color);
            currentSize++;
            yield return null; // Chờ một frame
        }

        // Chuc nang
        if (!Settings.concentrateSKill)
        {
            Settings.concentrateSKill = true;
            Player.Instance.CurrentInfo.stamina = Player.Instance.InfoDefaultSO.stamina;
            Player.Instance.UpdatePlayerUI();
        }
        Settings.isAttacking = false;
        Settings.isCatingSkill = false;
        timeToExhaust = timeLifeSkill;
        elapsedTime = 0f;
        yield return new WaitForSeconds(base.timeLifeSkill); // vòng đời hào quang ánh sáng 

        if (Settings.concentrateSKill)
            Settings.concentrateSKill = false;


        transform.localScale = scale;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Player.Instance.spriteRendererPlayer.color = Settings.playerColor;
        Player.Instance.spriteRendererPlayer.material.color = Color.green;

        // Thời gian hồi phép 
        yield return new WaitForSeconds(base.skillCoolDown);

        currentSize = 0;
        //base.canUseSkill = true;
        this.gameObject.GetComponent<SpriteRenderer>().color = gameObjectColor;
        this.gameObject.SetActive(false);
    }

    public override void ActivateSkill(int amount, float scale)
    {
        return;
    }

    public override void HoldKeySkill()
    {
        return;
    }
}
