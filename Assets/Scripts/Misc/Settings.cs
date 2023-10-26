using UnityEngine;

public static class Settings
{
    // Player Movement
    public static bool isDasing = false;

    // Jump
    public static bool _isJumping = false;

    // Block
    public static bool isBlocking = false;

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
