using DG.Tweening;
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

    private float _defaultGravityScale, _coyoteCounter, spamJumpCounter;
    private int currentExtraJump;
    private bool jumpInAir;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        _defaultGravityScale = 8;
    }
    private void Update()
    {
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
            if (Settings.isWalled && Input.GetKeyDown(KeyCode.Space))
            {
                Settings._isJumping = true;
                _coyoteCounter = _coyoteTime;
                //spamJumpCounter = spamJumpTime;
                rb2d.velocity = Vector2.up * _jumpHeight * new Vector2(-transform.localScale.x*1,0);
                return;
            }
            if (Settings.isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                Settings._isJumping = true;
                _coyoteCounter = _coyoteTime;
                //spamJumpCounter = spamJumpTime;
                rb2d.velocity = Vector2.up * _jumpHeight;
                currentExtraJump = extraJump;
            }

            if (jumpInAir && Input.GetKeyDown(KeyCode.Space))
            {
                rb2d.velocity = Vector2.up * _jumpHeight*0.5f;
            }

            if (Input.GetKey(KeyCode.Space) && Settings._isJumping)
            {
                if (_coyoteCounter > 0)
                {
                    rb2d.velocity = Vector2.up * (_jumpHeight);
                    rb2d.gravityScale = _upwardMovementMultiplier;
                    _coyoteCounter -= Time.deltaTime;
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
}
