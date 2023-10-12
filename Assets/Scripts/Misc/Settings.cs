using UnityEngine;

public static class Settings
{
    // Player Movement
    public static float speedMove = 10f;

    public static float jumpForce = 7f;
    public static int extraJump;
    public static int extraJumpValue = 1;
    public static float jumpTime;
    public static float jumpStartTime = 0.4f;

    public static bool canDash = true;
    public static bool isDasing = false;
    public static float dashForce = 20f;
    public static float dashingTime = 0.2f;
    public static float dashCooldown = 0.7f;

    public static bool isGrounded = false;
    public static bool isFacingRight = true;

    // Player Attack
    public static bool canAttack = true;
    public static bool isAttack = false;
    public static bool normalAttack = false;
    public static bool strongAttack = false;
    public static float normalAttackTime = 0.5f;
    public static float strongAttackTime = 1f;
    public static float normalAttackCooldown = 1f;
    public static float strongAttackCooldown = 2.5f;

    // Tag
    public static string groundLayerMask = "Ground";
}
