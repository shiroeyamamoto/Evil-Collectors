﻿using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using SpriteTrailRenderer;
using DG.Tweening;
using static UnityEngine.Rendering.DebugUI;

public class Move : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;

    // Movement
    [SerializeField, Range(0f, 100f)] public float speedMove = 10f;
    [SerializeField, Range(0f, 100f)] private float speedAirMove = 20f;

    [Range(0f, 90f)] public float rotationWhenMove = 18f;

    // Dash
    private bool canDash = true;
    //private bool isDasing = false;
    [SerializeField, Range(0f, 100f)] private float staminaNeed = 20f;
    [SerializeField, Range(0f, 100f)] private float dashForce = 100f;
    [SerializeField, Range(0f, 5f)] private float dashingTime = 0.2f;
    [SerializeField, Range(0f, 5f)] private float dashCooldown = 0.7f;
    public Jump jumpController;

    [HideInInspector] public float Horizontal;

    private Rigidbody2D rb2d;
    private AudioSource audioSource;
    //private TrailRenderer trail;
    private SpriteTrailRenderer.SpriteTrailRenderer trailRenderer;

    protected void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<SpriteTrailRenderer.SpriteTrailRenderer>();
        trailRenderer.enabled = false;
        audioSource = GetComponent<AudioSource>();
        //trail = GetComponent<TrailRenderer>();
    }
    private void Update()
    {

        // Cấm hành động khi dash 
        if (Settings.isDasing || Settings.isAttacking)
        {
            return;
        }

        if (Settings.isCatingSkill)
            return;
        Flip();


        //Player.Instance.animator.SetBool("isDashing", Settings.isDasing);
        if (!Settings.PlayerDamaged)
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {

                // coliision trên đường lướt của player, dùng cho chức năng khi enemy đánh trúng đoạn này => hồi mana 
                if(Settings.isFacingRight)
                    PlayerManager.Instance.DashCollison.transform.position = new Vector3(Player.Instance.transform.position.x + 3.5f, Player.Instance.transform.position.y, 0);
                else if(!Settings.isFacingRight)
                    PlayerManager.Instance.DashCollison.transform.position = new Vector3(Player.Instance.transform.position.x - 3.5f, Player.Instance.transform.position.y, 0);


                // stamina tiêu thụ
                if (!Settings.concentrateSKill 
                    && Player.Instance.CurrentInfo.stamina >= 20)
                {
                    StartCoroutine(Dash());
                }
                else if (Settings.concentrateSKill)
                {
                    StartCoroutine(Dash());
                }
            }


        if (Settings.isGrounded)
            canDash = true;
        //MovementSound();
    }

    private void FixedUpdate()
    {
        // Cấm hành động khi dash 
        if (Settings.isDasing)
        {
            return;
        }

        if (Settings.isAttacking)
        {
            return;
        }

        if (Settings.isCatingSkill)
            return;

        if (!Settings.PlayerDamaged)
            if (!jumpController.isWallJumping)
                PlayerMovement();
    }

    /// <summary>
    /// Player di chuyển trái và phải 
    /// </summary>
    private void PlayerMovement()
    {

        Settings.isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, LayerMask.GetMask(Settings.groundLayerMask));
        //Settings.isWalled = Physics2D.OverlapBox(wallCheck.position, new Vector2(1.1f, 0.1f), LayerMask.GetMask(Settings.wallLayerMask));
        Settings.isWalled = Physics2D.OverlapCircle(wallCheck.position, 0.65f, LayerMask.GetMask(Settings.wallLayerMask));

        float move = Input.GetAxisRaw("Horizontal");

        //Player.Instance.animator.SetFloat("Speed", Mathf.Abs(move));

        if (move != 0 && !Settings.isMove)
            Settings.isMove = true;
        else if (move == 0 && Settings.isMove)
            Settings.isMove = false;

        if (jumpController.isSliding)
            PlayerRotation(0);
        else
            PlayerRotation(move);

        rb2d.velocity = new Vector2(move * (Settings.isGrounded ? speedMove : speedAirMove), rb2d.velocity.y);

        if (rb2d.velocity.x != 0 && Settings.isGrounded)
        {
            audioSource.clip = PlayerSound.Instance.MovementSound;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }

        Horizontal = move;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(wallCheck.position, 0.65f);
    }

    /// <summary>
    /// Lật mặt 
    /// </summary>
    public void Flip()
    {

        if (jumpController.isSliding)
            return;

        if (Settings.isFacingRight && Horizontal < 0f || !Settings.isFacingRight && Horizontal > 0f)
        {
            Vector2 localScale = transform.localScale;
            Settings.isFacingRight = !Settings.isFacingRight;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    /// <summary>
    /// Dash 
    /// </summary>
    /// <returns></returns>
    private IEnumerator Dash()
    {
        canDash = false;
        Settings.isDasing = true;
        trailRenderer.enabled = true;
        trailRenderer._startScale = new Vector2(0.75f,0.75f);
        trailRenderer._endScale = new Vector2(1.5f, 1.5f);
        //trail.emitting = true;

        Settings.enterEnemy = false;
        Settings.ememyMiss = true;
        PlayerManager.Instance.DashCollison.gameObject.GetComponent<BoxCollider2D>().enabled = true;

        float originalGravity = rb2d.gravityScale;
        rb2d.gravityScale = 0f;

        rb2d.velocity = new Vector2(transform.localScale.x * dashForce, 0f);

        if (!Settings.concentrateSKill)
        {
            Player.Instance.UseStamina(20);
        }

        yield return new WaitForSeconds(dashingTime);
        //trail.emitting = false;
        trailRenderer.enabled = false;
        Debug.Log("Settings.enterEnemy: " + Settings.enterEnemy);
        Debug.Log("Settings.ememyMiss: " + Settings.ememyMiss);
        if (!Settings.enterEnemy && !Settings.ememyMiss)
        {
            /*if (GameController.Instance.Player.CurrentInfo.mana < GameController.Instance.Player.InfoDefaultSO.mana)
            {
                GameController.Instance.Player.CurrentInfo.mana += 10;
                if (GameController.Instance.Player.CurrentInfo.mana > GameController.Instance.Player.InfoDefaultSO.mana)
                    GameController.Instance.Player.CurrentInfo.mana = GameController.Instance.Player.InfoDefaultSO.mana;
                Player.Instance.OnUpdateMana?.Invoke(Player.Instance.CurrentInfo.mana);
            }*/
            Player.Instance.UseMana(-10);
        }

        PlayerManager.Instance.DashCollison.gameObject.GetComponent<BoxCollider2D>().enabled = false;

        Settings.isDasing = false;
        rb2d.gravityScale = originalGravity;
        yield return new WaitForSeconds(dashCooldown);

    }

    /// <summary>
    /// Âm thanh khi di chuyển
    /// </summary>
    private void MovementSound()
    {
        if (Settings.isMove)
        {
            Player.Instance.audioSource.clip = Player.Instance.playerSound.Running;
            Player.Instance.audioSource.Play();
        }
        else if (!Settings.isMove)
        {
            Player.Instance.audioSource.clip = null;
            Player.Instance.audioSource.Stop();
        }
    }

    public void PlayerRotation(float move)
    {
        if (move > 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, -rotationWhenMove);
        }
        else if (move < 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, rotationWhenMove);
        }
        else if (move == 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public bool PlayerDontCanMove(bool canMove)
    {
        if (canMove)
            return true;
        if (!canMove)
            return false;

        return false;
    }
}