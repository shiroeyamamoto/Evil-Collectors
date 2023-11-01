using UnityEngine;

public class Jump : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private Vector2 _velocity;

    [SerializeField, Range(0f, 30f)] private float _jumpHeight = 10f;
    [SerializeField, Range(0f, 30f)] private float _jumpHeightHold = 10f;
    [SerializeField, Range(0, 5), Tooltip("Số lần nhảy trên không")] private int _maxAirJumps = 0;
    [SerializeField, Range(0f, 20f), Tooltip("Trọng lực khi player đang rơi")] private float _downwardMovementMultiplier = 3f;
    [SerializeField, Range(0f, 20f), Tooltip("Trọng lực khi player nhảy lên")] private float _upwardMovementMultiplier = 1.7f;
    [SerializeField, Range(0f, 2f)] private float _coyoteTime = 0.2f;
    [SerializeField, Range(0f, 0.3f)] private float _jumpBufferTime = 0.2f;
    [SerializeField, Range(0f, 5f)] private float spamJumpTime = 0.2f;

    private bool spacePressed = false;  // Biến này sẽ chỉ đúng vào frame mà nút Space được nhấn (Press)
    private bool spaceHeld = false;    // Biến này sẽ đúng khi nút Space được giữ (Hold)

    private int _jumpPhase;
    private float _defaultGravityScale, defaultJumpHeight, _jumpSpeed, _coyoteCounter, _jumpBufferCounter, spamJumpCounter;

    private float lowJumpGravity = 20f;

    private bool jump, isHoldJump, isPress;

    private float maxJumpDistance = 3f; // Khoảng cách tối đa khi nhảy
    private Vector2 jumpStartPosition; // Vị trí xuất phát của nhảy

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        _defaultGravityScale = 8;
        //defaultJumpHeight = _jumpHeight;
       spamJumpCounter = spamJumpTime;
        isHoldJump = false;
    }
    private void Update()
    {
        // không cho nhảy khi đang dash
        if (Settings.isDasing)
            return;

        //JumpWhenHold();

        if (Settings.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Settings._isJumping = true;
            _coyoteCounter = _coyoteTime;
            spamJumpCounter = spamJumpTime;
            rb2d.velocity = Vector2.up * _jumpHeight;
        }

        if (Input.GetKey(KeyCode.Space) && Settings._isJumping)
        {



            if (_coyoteCounter > 0)
            {
                rb2d.velocity = Vector2.up * (_jumpHeight);
                if (spamJumpCounter > 0)
                {
                    //rb2d.velocity = Vector2.up * _jumpHeight;
                    rb2d.gravityScale = -_downwardMovementMultiplier;
                    spamJumpCounter -= Time.deltaTime;

                }
                else
                {
                    rb2d.gravityScale = _upwardMovementMultiplier;
                }
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
            rb2d.gravityScale =_defaultGravityScale;
        }

        /*if(spamJumpCounter > 0 && !isHoldJump && Input.GetButton("Jump"))
        {
            if (!Settings.PlayerDamaged && isPress == false)
            {
                Debug.Log("Đang nhấn");
                isPress = true;
                rb2d.velocity = Vector2.up * _jumpHeight;
            }   

            spamJumpCounter -= Time.deltaTime;
        }
        else
        {
            isHoldJump = true;
        }
        if(isHoldJump && Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Đang giữ");

            JumpWhenHold();
            
        }

        if (!Input.GetButton("Jump"))
        {
            isHoldJump = false;
            spamJumpCounter = spamJumpTime;
        }

        if (Settings.isGrounded)
        {
            isPress = false;
        }*/

        //Player.Instance.animator.SetBool("isJumping", Settings._isJumping);

    }

    /// <summary>
    /// Jump when hold
    /// </summary>
    private void JumpWhenHold()
    {
        if (!Settings.PlayerDamaged)
        {
            jump |= Input.GetButtonDown("Jump");

            if (!jump)
            {
                jumpStartPosition = transform.position;
                Debug.Log("jumpStartPosition: "+ jumpStartPosition);
            }

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
            }/*
                if (!Input.GetButton("Jump") && rb2d.velocity.y > 0)
                {
                    rb2d.gravityScale = lowJumpGravity;
                }
                else*/
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
            //_jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * _jumpHeight * _upwardMovementMultiplier);
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
}
