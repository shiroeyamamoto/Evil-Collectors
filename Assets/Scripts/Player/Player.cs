using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngineInternal;

public class Player : SingletonMonobehavious<Player>
{
    [SerializeField]private Transform groundCheck;

    [Tooltip("Scriptable Object player stats")]
    //public SO_PlayerStats playerStats;


    private void testPull()
    {
        // Xin chao
    }

    private Rigidbody2D rb2d;

    private float Horizontal;

    protected override void Awake()
    {
        base.Awake();

        rb2d = GetComponent<Rigidbody2D>();

        Settings.extraJump = Settings.extraJumpValue;
    }

    private void FixedUpdate()
    {
        // Cấm hành động khi dash
        if (Settings.isDasing)
        {
            return;
        }

        // Cấm hành động khi tấn công  
        if (Settings.isAttack)
        {
            return;
        }
        PlayerMovement();
    }
    private void Update()
    {
        // Cấm hành động khi dash 
        if (Settings.isDasing)
        {
            return;
        }

        // Cấm hành động khi tấn công  
        if (Settings.isAttack)
        {
            return;
        }
        Flip();
        PlayerJump();
        if (Input.GetKeyDown(KeyCode.LeftShift) && Settings.canDash)
        {
            StartCoroutine(Dash());
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

    // Lật mặt
    private void Flip()
    {
        if(Settings.isFacingRight && Horizontal<0f || !Settings.isFacingRight && Horizontal > 0f)
        {
            Vector2 localScale = transform.localScale;
            Settings.isFacingRight = !Settings.isFacingRight;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

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
                Damage(0);
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
                Damage(0);
                yield return new WaitForSeconds(Settings.strongAttackTime);
                Settings.strongAttack = false;
                Settings.isAttack = false;
                sword.SetActive(false);
                yield return new WaitForSeconds(Settings.strongAttackCooldown);
            }
            Settings.canAttack = true;
    }

    /// <summary>
    ///  Damage
    /// </summary>
    private void Damage(int dmg)
    {

    }

    private void PlayerMovement()
    {
        Settings.isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.5f, LayerMask.GetMask(Settings.groundLayerMask));

        float move = Input.GetAxis("Horizontal");

        rb2d.velocity = new Vector2(move * Settings.speedMove, rb2d.velocity.y);

        Horizontal = move;
    }

    // Nhảy 
    private void PlayerJump()
    {
        if(Settings.isGrounded == true)
        {
            Settings.extraJump = Settings.extraJumpValue;
        }

        if (Input.GetKeyDown(KeyCode.Space) && Settings.extraJump > 0)
        {
            rb2d.velocity = Vector2.up * Settings.jumpForce;
            Settings.extraJump--;
        } else if (Input.GetKeyDown(KeyCode.W) && Settings.extraJump == 0 && Settings.isGrounded == true)
        {
            rb2d.velocity = Vector2.up * Settings.jumpForce;
        }
    }

    // dash 
    private IEnumerator Dash()
    {
        Settings.canDash = false;
        Settings.isDasing = true;

        float originalGravity = rb2d.gravityScale;
        rb2d.gravityScale = 0f;
        rb2d.velocity = new Vector2(transform.localScale.x*Settings.dashForce, 0f);
        yield return new WaitForSeconds(Settings.dashingTime);
        Settings.isDasing = false;
        rb2d.gravityScale = originalGravity;
        yield return new WaitForSeconds(Settings.dashCooldown);
        Settings.canDash = true;
    }
}
