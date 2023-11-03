using System.Collections;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;

    // Movement
    [SerializeField, Range(0f, 100f)] private float speedMove = 10f;
    [SerializeField, Range(0f, 100f)] private float speedAirMove = 20f;

    // Dash
    private bool canDash = true;
    //private bool isDasing = false;
    [SerializeField, Range(0f, 100f)] private float dashForce = 100f;
    [SerializeField, Range(0f, 5f)] private float dashingTime = 0.2f;
    [SerializeField, Range(0f, 5f)] private float dashCooldown = 0.7f;

    private float Horizontal;

    private Rigidbody2D rb2d;
    private TrailRenderer trail;

    protected void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        trail = GetComponent<TrailRenderer>();
    }
    private void Update()
    {
        // Cấm hành động khi dash 
        if (Settings.isDasing)
        {
            return;
        }

        Flip();


        //Player.Instance.animator.SetBool("isDashing", Settings.isDasing);
        if(!Settings.PlayerDamaged)
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                StartCoroutine(Dash());
            }

        if(Settings.isGrounded)
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
        if(!Settings.PlayerDamaged)
            PlayerMovement();
    }

    /// <summary>
    /// Player di chuyển trái và phải 
    /// </summary>
    private void PlayerMovement()
    {
        Settings.isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, LayerMask.GetMask(Settings.groundLayerMask));

        float move = Input.GetAxisRaw("Horizontal");

        Vector2 movement = new Vector2(move, 0f);

        //Player.Instance.animator.SetFloat("Speed", Mathf.Abs(move));

        if (move != 0 && !Settings.isMove)
            Settings.isMove = true;
        else if(move == 0 && Settings.isMove)
            Settings.isMove = false;

       
        rb2d.velocity = new Vector2(move * (Settings.isGrounded ? speedMove : speedAirMove), rb2d.velocity.y);

        Horizontal = move;
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
    /// Dash 
    /// </summary>
    /// <returns></returns>
    private IEnumerator Dash()
    {
        canDash = false;
        Settings.isDasing = true;
        trail.emitting = true;

        float originalGravity = rb2d.gravityScale;
        rb2d.gravityScale = 0f;
        rb2d.velocity = new Vector2(transform.localScale.x * dashForce, 0f);

        yield return new WaitForSeconds(dashingTime);
        trail.emitting = false;
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
}
