using UnityEngine;

public class Jump : MonoBehaviour
{

    private Rigidbody2D rb2d;

    [SerializeField, Range(0f, 30f)] private float _jumpHeight = 10f;
    //[SerializeField, Range(0f, 20f), Tooltip("Trọng lực khi player đang rơi")] private float _downwardMovementMultiplier = 3f;
    [SerializeField, Range(0f, 20f), Tooltip("Trọng lực khi player nhảy lên")] private float _upwardMovementMultiplier = 1.7f;
    [SerializeField, Range(0f, 2f)] private float _coyoteTime = 0.2f;
    [SerializeField, Range(0f, 5f)] private float spamJumpTime = 0.2f;

    private float _defaultGravityScale, _coyoteCounter, spamJumpCounter;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        _defaultGravityScale = 8;
        //defaultJumpHeight = _jumpHeight;
       //spamJumpCounter = spamJumpTime;
    }
    private void Update()
    {
        // không cho nhảy khi đang dash
        if (Settings.isDasing)
        {
            _coyoteCounter = 0f;
            return;
        }

        //  Debug.Log("Settings._isJumping: " + Settings._isJumping);
        //Debug.Log("Settings.isGrounded: " + Settings.isGrounded);
        /*if (!Settings.isGrounded && !Input.GetKey(KeyCode.Space))
        {
            Settings._isJumping = false;
            rb2d.gravityScale = _defaultGravityScale;
        }*/
        //Debug.Log("Settings.PlayerDamaged: "+ Settings.PlayerDamaged);
        if (!Settings.PlayerDamaged)
        {
            if (Settings.isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                Settings._isJumping = true;
                _coyoteCounter = _coyoteTime;
                //spamJumpCounter = spamJumpTime;
                rb2d.velocity = Vector2.up * _jumpHeight;
            }

            if (Input.GetKey(KeyCode.Space) && Settings._isJumping)
            {



                if (_coyoteCounter > 0)
                {
                    rb2d.velocity = Vector2.up * (_jumpHeight);
                    /*if (spamJumpCounter > 0)
                    {
                        //rb2d.velocity = Vector2.up * _jumpHeight;
                        rb2d.gravityScale = -_downwardMovementMultiplier;
                        spamJumpCounter -= Time.deltaTime;

                    }
                    else
                    {
                        rb2d.gravityScale = _upwardMovementMultiplier;
                    }*/
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
            }
        }

        //Player.Instance.animator.SetBool("isJumping", Settings._isJumping);
    }
}
