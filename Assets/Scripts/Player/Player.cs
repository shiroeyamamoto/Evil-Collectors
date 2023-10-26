using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngineInternal;

public class Player : SingletonMonobehavious<Player>
{

    public SO_PlayerData playerData;

    private Rigidbody2D rb2d;


    [HideInInspector] public float DamageAttack;

    protected override void Awake()
    {
        base.Awake();
        rb2d = GetComponent<Rigidbody2D>();
        //positionPlayer = transform.position;
    }

    private void OnDisable()
    {
        playerData.ResetData();
    }
    private void Update()
    {
        

        // Cấm hành động khi tấn công  
        if (Settings.isAttack)
        {
            return;
        }

        // không cho tấn công khi chưa hồi chiêu
        if (!Settings.canAttack)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Attack(0));
        }
        else if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(Attack(1));
        }
    }

    /// <summary>
    /// Lật mặt
    /// </summary>
    

    /// <summary>
    /// Nhận vào giá trị là chuột trái hay phải,trả về animation tương ứng, trả về cờ sát thương mạnh hay yếu
    /// </summary>
    private IEnumerator Attack(int typeAttack)
    {
        GameObject sword = transform.Find("Attack").gameObject;

        Settings.canAttack = false;
        Settings.isAttack = true;

        // test cho animation 
        sword.SetActive(true);

        if (typeAttack == 0)
        {
            Settings.normalAttack = true;
            /// Sát thương gây ra 
            /// 
            Damage(20);
            yield return new WaitForSeconds(Settings.normalAttackTime);
            Settings.normalAttack = false;
            Settings.isAttack = false;
            sword.SetActive(false);
            yield return new WaitForSeconds(Settings.normalAttackCooldown);
        }
        else if (typeAttack == 1)
        {
            Settings.strongAttack = true;
            /// Sát thương gây ra 
            /// 
            Damage(50);
            yield return new WaitForSeconds(Settings.strongAttackTime);
            Settings.strongAttack = false;
            Settings.isAttack = false;
            sword.SetActive(false);
            yield return new WaitForSeconds(Settings.strongAttackCooldown);
        }
        Settings.canAttack = true;
    }

    /// <summary>
    ///  Damage player gây ra 
    /// </summary>
    public void Damage(float dmg)
    {
        DamageAttack = dmg;
    }

    public void NoneDamage()
    {
        DamageAttack = 0;
    }

    /// <summary>
    /// Take damage by enemy or trap
    /// </summary>
    public void TakeDamage(float dmg)
    {

        if (Settings.isBlocking)
            dmg -= playerData.defense;

        if (playerData.health > 0)
            playerData.health -= dmg;

        if (playerData.health <= 0)
            PlayerDie();
    }

    private void PlayerDie()
    {
        Destroy(this.gameObject);
    }
    
}
