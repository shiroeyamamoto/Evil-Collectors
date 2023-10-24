using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngineInternal;

public class Player : SingletonMonobehavious<Player>
{
    [SerializeField] private Transform groundCheck;

    public SO_PlayerData playerData;

    private Rigidbody2D rb2d;
    private Vector2 _velocity;

    private float Horizontal;

    // Player controller
    private bool canJump = true;
    [SerializeField, Range(0f, 10f)] private float _jumpHeight = 10f;
    [SerializeField, Range(0, 5)] private int _maxAirJumps = 0;
    [SerializeField, Range(0f, 5f)] private float _downwardMovementMultiplier = 3f;
    [SerializeField, Range(0f, 5f)] private float _upwardMovementMultiplier = 1.7f;
    private int _jumpPhase;
    private float _defaultGravityScale, _jumpSpeed;
    Vector2 positionPlayer = new Vector2();


    [HideInInspector] public float DamageAttack;

    protected override void Awake()
    {
        base.Awake();
        rb2d = GetComponent<Rigidbody2D>();
        _defaultGravityScale = 1;
        positionPlayer = transform.position;
    }

    private void OnDisable()
    {
        playerData.ResetData();
    }
    private void FixedUpdate()
    {
        /*// Cấm hành động khi dash
        if (Settings.isDasing)
        {
            return;
        }

        // Cấm hành động khi tấn công  
        if (Settings.isAttack)
        {
            return;
        }*/
        if (positionPlayer.y + _jumpHeight < transform.position.y && !Settings.isGrounded && rb2d.velocity.y > 0)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
            Debug.LogError("Loi roi");
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
        Debug.LogError("transform.position " + transform.position);
        Debug.Log("Settings.isGrounded" + Settings.isGrounded);
        Debug.LogError("rb2d.velocity.y " + rb2d.velocity.y);
        Debug.LogError("positionPlayer.position.y + _jumpHeight < transform.position.y " + (positionPlayer.y + _jumpHeight < transform.position.y));
        
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

    /// <summary>
    /// Lật mặt
    /// </summary>
    private void Flip()
    {
        if (Settings.isFacingRight && Horizontal < 0f || !Settings.isFacingRight && Horizontal > 0f)
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
        if (playerData.health > 0)
            playerData.health -= dmg;
        PlayerDie();
    }

    private void PlayerDie()
    {
        if (playerData.health <= 0)
            Destroy(this.gameObject);
    }

    /// <summary>
    /// Movement
    /// </summary>
    private void PlayerMovement()
    {
        Settings.isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, LayerMask.GetMask(Settings.groundLayerMask));

        float move = Input.GetAxisRaw("Horizontal");

        rb2d.velocity = new Vector2(move * Settings.speedMove, rb2d.velocity.y);

        Horizontal = move;
    }

    /// <summary>
    /// Jump
    /// </summary>
    private void PlayerJump()
    {
        _velocity = rb2d.velocity;

        if (Settings.isGrounded == true)
        {
            Settings.extraJump = Settings.extraJumpValue;
            //rb2d.gravityScale = 1;
            //if(Input.GetKeyDown(KeyCode.Space))
            canJump = true;
            
            Settings.jumpTime = Settings.jumpStartTime;
        }
        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {

            positionPlayer.y = transform.position.y;
            JumpAction();
            canJump = false;



            
            /*if (Input.GetKey(KeyCode.Space))
            {
                //rb2d.gravityScale = 0;
                if (Settings.jumpTime > 0)
                {
                    JumpAction();
                    //rb2d.velocity = new Vector2(rb2d.velocity.x, Vector2.up.y * Settings.jumpForce);
                    //rb2d.AddForce(new Vector2(rb2d.velocity.x, Vector2.up.y * Settings.jumpForce*Time.deltaTime), ForceMode2D.Impulse);
                    Settings.jumpTime -= Time.deltaTime;
                }
                if (Settings.jumpTime <= 0)
                {
                    canJump = false;
                    PlayerFall();
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                PlayerFall();
            }*/
        }
        if ( rb2d.velocity.y > 0)
        {
            rb2d.gravityScale = _upwardMovementMultiplier;
        }
        else if ( rb2d.velocity.y < 0)
        {
            rb2d.gravityScale = _downwardMovementMultiplier;
        }
        else if (rb2d.velocity.y == 0)
        {
            rb2d.gravityScale = _defaultGravityScale;
        }

        rb2d.velocity = _velocity;
    }

    private void JumpAction()
    {
        if (Settings.isGrounded || _jumpPhase < _maxAirJumps)
        {
            _jumpPhase += 1;


            rb2d.velocity = new Vector2(rb2d.velocity.x, Vector2.up.y * Settings.jumpForce);

            //positionPlayer.position = transform.position;

            _jumpSpeed = rb2d.velocity.y;
            //_jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * _jumpHeight);

            if (_velocity.y > 0f)
            {
                _jumpSpeed = Mathf.Max(_jumpSpeed - _velocity.y, 0f);
            }
            else if (_velocity.y < 0f)
            {
                _jumpSpeed += Mathf.Abs(rb2d.velocity.y);
            }

            /*if (Input.GetKey(KeyCode.Space))
            {
                rb2d.gravityScale = 0;
                if (Settings.jumpTime > 0)
                {
                    _velocity.y += _jumpSpeed;
                    //rb2d.velocity = new Vector2(rb2d.velocity.x, Vector2.up.y * Settings.jumpForce);
                    //rb2d.AddForce(new Vector2(rb2d.velocity.x, Vector2.up.y * Settings.jumpForce*Time.deltaTime), ForceMode2D.Impulse);
                    Settings.jumpTime -= Time.deltaTime;
                }
                if (Settings.jumpTime <= 0)
                {
                    PlayerFall();
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                PlayerFall();
            }*/

            _velocity.y += _jumpSpeed;
        }
    }

    private void PlayerFall()
    {
        rb2d.velocity = Vector2.down;
        rb2d.gravityScale = 5;
    }

    /// <summary>
    /// Dash 
    /// </summary>
    /// <returns></returns>
    private IEnumerator Dash()
    {
        Settings.canDash = false;
        Settings.isDasing = true;

        float originalGravity = rb2d.gravityScale;
        rb2d.gravityScale = 0f;
        rb2d.velocity = new Vector2(transform.localScale.x * Settings.dashForce, 0f);
        yield return new WaitForSeconds(Settings.dashingTime);
        Settings.isDasing = false;
        rb2d.gravityScale = originalGravity;
        yield return new WaitForSeconds(Settings.dashCooldown);
        Settings.canDash = true;
    }
}
