using UnityEngine;

public class Jump : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private Vector2 _velocity;

    [SerializeField, Range(0f, 10f)] private float _jumpHeight = 10f;
    [SerializeField, Range(0, 5), Tooltip("Số lần nhảy trên không")] private int _maxAirJumps = 0;
    [SerializeField, Range(0f, 10f), Tooltip("Trọng lực khi player đang rơi")] private float _downwardMovementMultiplier = 3f;
    [SerializeField, Range(0f, 5f), Tooltip("Trọng lực khi player nhảy lên")] private float _upwardMovementMultiplier = 1.7f;
    [SerializeField, Range(0f, 1f)] private float _coyoteTime = 0.2f;
    [SerializeField, Range(0f, 0.3f)] private float _jumpBufferTime = 0.2f;


    private int _jumpPhase;
    private float _defaultGravityScale, _jumpSpeed ,_coyoteCounter, _jumpBufferCounter;

    private bool jump;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        _defaultGravityScale = 1;
    }
    private void Update()
    {
        // không cho nhảy khi đang dash
        if (Settings.isDasing)
            return;


        //Player.Instance.animator.SetBool("isJumping", Settings._isJumping);

        if (!Settings.PlayerDamaged)
        {
            jump |= Input.GetButtonDown("Jump");

            _velocity = rb2d.velocity;

            if (Settings.isGrounded && rb2d.velocity.y == 0)
            {
                _jumpPhase = 0;
                _coyoteCounter = _coyoteTime;
                Settings._isJumping = false;
            }
            else
            {
                _coyoteCounter -= Time.deltaTime;
            }

            if (jump)
            {
                jump = false;
                _jumpBufferCounter = _jumpBufferTime;
            }
            else if (!jump && _jumpBufferCounter > 0)
            {
                _jumpBufferCounter -= Time.deltaTime;
            }

            if (_jumpBufferCounter > 0)
            {
                JumpAction();
            }

            if (Input.GetButton("Jump") && rb2d.velocity.y > 0)
            {
                rb2d.gravityScale = _upwardMovementMultiplier;
            }
            else if (!Input.GetButton("Jump") || rb2d.velocity.y < 0)
            {
                rb2d.gravityScale = _downwardMovementMultiplier;
            }
            else if (rb2d.velocity.y == 0)
            {
                rb2d.gravityScale = _defaultGravityScale;
            }

            rb2d.velocity = _velocity;
        }
    }

    /// <summary>
    ///  Jump
    /// </summary>
    private void JumpAction()
    {
        if (_coyoteCounter > 0f || (_jumpPhase < _maxAirJumps && Settings._isJumping))
        {
            if (Settings._isJumping)
            {
                _jumpPhase += 1;
            }

            _jumpBufferCounter = 0;
            _coyoteCounter = 0;
            _jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * _jumpHeight * _upwardMovementMultiplier);
            Settings._isJumping = true;

            if (_velocity.y > 0f)
            {
                _jumpSpeed = Mathf.Max(_jumpSpeed - _velocity.y, 0f);
            }
            else if (_velocity.y < 0f)
            {
                _jumpSpeed += Mathf.Abs(rb2d.velocity.y);
            }
            _velocity.y += _jumpSpeed;
        }
    }

    private void PlayerFall()
    {
        rb2d.velocity = Vector2.down;
        rb2d.gravityScale = 5;
    }
}
