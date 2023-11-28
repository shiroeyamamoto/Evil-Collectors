using Unity.VisualScripting;
using UnityEngine;

public static class Settings
{
    // Player Movement
    [Tooltip("Player có đang dash không?")] public static bool isMove = false;
    [Tooltip("Player có đang dash không?")] public static bool isDasing = false;
    [Tooltip("Player đang dash.")] public static bool isFacingRight = true;

    // Jump
    [Tooltip("Player có đang jump không?")] public static bool _isJumping = false;

    // Dash
    [Tooltip("Khi dash có va chạm enemy không?")] public static bool enterEnemy = false;
    [Tooltip("Khi dash enemy đánh hụt?")] public static bool ememyMiss = true;

    // Block
    [Tooltip("Player đang block.")] public static bool isBlocking = false;
    [Tooltip("Player có thể parry không?")] public static bool canParry = false;
    [Tooltip("Player đang trạng thái parry?")] public static bool isParry = false;

    // Ground check
    [Tooltip("Player đang đứng trên mặt đất?")] public static bool isGrounded = false;
    [Tooltip("Player đang đụng tường?")] public static bool isWalled = false;
    [Tooltip("Player đang đứng trên enemy?")] public static bool standInEnemy = false;

    // Player Attack
    [Tooltip("Player đang sử dụng đòn tấn công khác")] public static bool isAttacking = false;
    [Tooltip("Player tấn công thường?")] public static bool isAttackNormal = false;
    [Tooltip("Player tấn công mạnh?")] public static bool isAttackStrong = false;
    [Tooltip("Player knockback")] public static bool canKnockback = false;

    // Player Status
    [Tooltip("Player có bận không?")] public static bool PlayerDamaged = false;
    public static Color playerColor = Color.green;
    public static Color playerHatColor = Color.green;

    // Player Skill
    [Tooltip("Player đang dùng chiêu")] public static bool isCatingSkill = false;
    [Tooltip("PLayer sài skill concentrate, vô hạn stamina")] public static bool concentrateSKill = false;
    [Tooltip("PLayer sài skill nothingness, bất tử")] public static bool nothingnessSkill = false;

    // Tag
    [Tooltip("LayerMask tên Ground")] public static string groundLayerMask = "Ground";
    [Tooltip("LayerMask tên Ground")] public static string wallLayerMask = "Wall";
    [Tooltip("LayerMask tên Enemy")] public static string enemyLayerMask = "Enemy";

    // Cheats
    [Tooltip("Player bất tử?")] public static bool zombieMode = true;

    public static void DefaultSetting()
    {
        // Player Movement
        Settings.isMove = false;
        Settings.isDasing = false;
        Settings.isFacingRight = true;

        // Jump
        Settings._isJumping = false;

        // Dash
        Settings.enterEnemy = false;
        Settings.ememyMiss = true;

        // Block
        Settings.isBlocking = false;
        Settings.canParry = false;
        Settings.isParry = false;

        // Ground check
        Settings.isGrounded = false;
        Settings.isWalled = false;
        Settings.standInEnemy = false;

        // Player Attack
        Settings.isAttacking = false;
        Settings.isAttackNormal = false;
        Settings.isAttackStrong = false;
        Settings.canKnockback = false;

        // Player Status
        Settings.PlayerDamaged = false;

        // Player Skill
        Settings.isCatingSkill = false;
        Settings.concentrateSKill = false;
        Settings.nothingnessSkill = false;
    }
}