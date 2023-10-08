using UnityEngine;

public static class Settings
{
    // Player Movement
    public static float speedMove = 10f;
    public static float jumpForce = 10f;
    public static bool canDash = true;
    public static bool isDasing = false;
    public static float dashForce = 20f;
    public static float dashingTime = 0.2f;
    public static float dashCooldown = 1.2f;
    public static bool isGrounded = false;
    public static int extraJump;
    public static int extraJumpValue = 1;
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
