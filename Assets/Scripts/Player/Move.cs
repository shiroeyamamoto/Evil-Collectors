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

    protected void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        // Cấm hành động khi dash 
        if (Settings.isDasing)
        {
            return;
        }

        Flip();

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        // Cấm hành động khi dash 
        if (Settings.isDasing)
        {
            return;
        }
        PlayerMovement();
    }

    /// <summary>
    /// Movement
    /// </summary>
    private void PlayerMovement()
    {
        Settings.isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, LayerMask.GetMask(Settings.groundLayerMask));

        float move = Input.GetAxisRaw("Horizontal");
        
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

        float originalGravity = rb2d.gravityScale;
        rb2d.gravityScale = 0f;
        rb2d.velocity = new Vector2(transform.localScale.x * dashForce, 0f);

        yield return new WaitForSeconds(dashingTime);
        Settings.isDasing = false;
        rb2d.gravityScale = originalGravity;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
