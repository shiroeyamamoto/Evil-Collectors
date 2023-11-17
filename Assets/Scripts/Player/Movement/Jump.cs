using DG.Tweening;
using System.Runtime.InteropServices;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Jump : MonoBehaviour
{

    private Rigidbody2D rb2d;

    [SerializeField, Range(0f, 30f)] private float _jumpHeight = 10f;
    //[SerializeField, Range(0f, 20f), Tooltip("Trọng lực khi player đang rơi")] private float _downwardMovementMultiplier = 3f;
    [SerializeField, Range(0f, 20f), Tooltip("Trọng lực khi player nhảy lên")] private float _upwardMovementMultiplier = 1.7f;
    [SerializeField, Range(0f, 2f)] private float _coyoteTime = 0.2f;
    [SerializeField, Range(0f, 5f)] private float spamJumpTime = 0.2f;
    [SerializeField, Range(0f, 5f)] private int extraJump = 0;

    [SerializeField, Range(0, 100)] private int wallSlidingSpeed = 15;

    [SerializeField, Range(0f, 5f)] private float wallJumpingDirection = 15;
    [SerializeField, Range(0f, 5f)] private float wallJumpingTime = 0.2f;
    [SerializeField, Range(0f, 5f)] private float wallJumpingDuration=0.4f;
    [SerializeField] private Vector2 wallForceJump;

    public Move moveController;
    [HideInInspector] public bool isWallJumping, isSliding;

    private float _defaultGravityScale, _coyoteCounter,h, wallJumpingCounter;
    private int currentExtraJump;
    private bool jumpInAir, isJumpedInWall, flipWall;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        _defaultGravityScale = 8;
        flipWall = true;
    }
    private void Update()
    {
        //Debug.Log("currentExtraJump: " + currentExtraJump);

        // không cho nhảy khi đang dash
        if (Settings.isDasing)
        {
            _coyoteCounter = 0f;
            return;
        }
        if (Settings.isCatingSkill)
            return;


        if (!Settings.PlayerDamaged )
        {

            WallJump();

            if (!isWallJumping)
            {
                //moveController.Flip();
            }

            h = Input.GetAxisRaw("Horizontal");

            if(Settings.isWalled && !Settings.isGrounded && h!=0)
            {
                isSliding = true;
                
            }
            else
            {
                isSliding = false;
            }

            if (!isWallJumping && isJumpedInWall && Input.GetKeyDown(KeyCode.Space) && !Settings.isGrounded && !Settings._isJumping)
            {
                Settings._isJumping = true;
                _coyoteCounter = _coyoteTime;
                //currentExtraJump = extraJump;
                isJumpedInWall = false;
                return;
            }

            if (Settings.isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                Settings._isJumping = true;
                _coyoteCounter = _coyoteTime;
                //spamJumpCounter = spamJumpTime;
                rb2d.velocity = Vector2.up * _jumpHeight;
                currentExtraJump = extraJump;
                //Debug.Log("Nhảy từ mặt đất 0");
            }

            if (jumpInAir && Input.GetKeyDown(KeyCode.Space))
            {
                rb2d.velocity = Vector2.up * _jumpHeight*0.5f;
                //Debug.Log("Nhảy từ mặt đất 1");
            }

            if (Input.GetKey(KeyCode.Space) && Settings._isJumping)
            {
                if (_coyoteCounter > 0)
                {
                    rb2d.velocity = Vector2.up * (_jumpHeight);
                        rb2d.gravityScale = _upwardMovementMultiplier;
                    _coyoteCounter -= Time.deltaTime;


                    //Debug.Log("Nhảy từ mặt đất 2");
                }
                else
                {
                    Settings._isJumping = false;
                }
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                Settings._isJumping = false;
                rb2d.gravityScale = _defaultGravityScale;
                _coyoteCounter = 0;
                if (currentExtraJump > 0)
                {
                    Settings._isJumping = true;
                    _coyoteCounter = _coyoteTime;
                    currentExtraJump--;
                    jumpInAir = true;
                }
                else
                {
                    jumpInAir = false;
                }
            }
        }

       
        //Player.Instance.animator.SetBool("isJumping", Settings._isJumping);
    }

    private void FixedUpdate()
    {
        if (isSliding)
        {

            // chạm tường không cho nhảy 
            currentExtraJump = 0;

            // bám vào tường thì hướng mặt ra ngoài
            if (flipWall)
            {
                Vector2 localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;
                Settings.isFacingRight = !Settings.isFacingRight;

                if (h > 0 && transform.localScale.x>0 || h < 0 && transform.localScale.x < 0)
                {
                    localScale.x *= -1;
                    transform.localScale = localScale;
                    Settings.isFacingRight = !Settings.isFacingRight;
                }

                flipWall = false;
            }
            rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Clamp(rb2d.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
    }

    private void WallJump()
    {
        if (isSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
            //Debug.Log("flipWall: " + flipWall);
            flipWall = true;
        }

        if(Input.GetKeyDown(KeyCode.Space) && wallJumpingCounter > 0f && h==0)
        {
            isWallJumping = true;
            
            rb2d.velocity = new Vector2(wallJumpingDirection * wallForceJump.x , wallForceJump.y);


            currentExtraJump = 0;

            Debug.Log("Nhảy từ tường ra xa tường ");
            wallJumpingCounter = 0f;

            isJumpedInWall = true;

            //Debug.Log($"wallJumping: {wallJumpingDirection} - transform local x: {transform.localScale.x}");


            //if (transform.localScale.x == wallJumpingDirection)
            //{
                //Settings.isFacingRight = !Settings.isFacingRight;
                //Vector3 localScale = transform.localScale;
                //localScale.x *= -1f;
                //transform.localScale = localScale;
            //}

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void JumpInWall()
    {
        if (isJumpedInWall)
        {
            //Settings.isFacingRight = !Settings.isFacingRight;
            transform.position = Vector2.Lerp(transform.position, transform.position + Vector3.up * 2f + (transform.localScale.x < 0 ? Vector3.right * 2f : Vector3.left * 2f), 1f);
            isJumpedInWall = false;
            //Settings.isFacingRight = !Settings.isFacingRight;
        }
    }
}
