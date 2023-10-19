using UnityEngine;

public static class Settings
{
    // Player Movement
    public const float speedMove = 10f;

    public const float jumpForce = 7f;
    public static int extraJump;
    public const int extraJumpValue = 1;
    public static float jumpTime;
    public const float jumpStartTime = 0.4f;

    public static bool canDash = true;
    public static bool isDasing = false;
    public const float dashForce = 20f;
    public const float dashingTime = 0.2f;
    public const float dashCooldown = 0.7f;

    public static bool isGrounded = false;
    public static bool isFacingRight = true;

    // Player Attack
    public static bool canAttack = true;
    public static bool isAttack = false;
    public static bool normalAttack = false;
    public static bool strongAttack = false;
    public const float normalAttackTime = 0.5f;
    public const float strongAttackTime = 1f;
    public const float normalAttackCooldown = 1f;
    public const float strongAttackCooldown = 2.5f;

    // Tag
    public static string groundLayerMask = "Ground";
}
