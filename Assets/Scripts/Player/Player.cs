using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : SingletonMonobehavious<Player>
{
    [SerializeField, Range(0.1f, 5f)] private float staminaRecoveryTime;
    [SerializeField, Range(0.1f, 5f)] private float manaRecoveryTime;

    public SO_PlayerData playerData;

    private Rigidbody2D rb2d;
    public SpriteRenderer spriteRendererPlayer;
    public Animator animator;
    public AudioSource audioSource;
    public PlayerSound playerSound;

    [SerializeField] Slider HP;
    [SerializeField] Slider MN;
    [SerializeField] Slider TP;

    float staminaTimeCounter;

    [HideInInspector] public float DamageAttack;
    private bool playerDie;
    float maxHealth, maxMana, maxStamina;

    protected override void Awake()
    {
        base.Awake();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRendererPlayer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        playerSound = GetComponent<PlayerSound>();
        animator = GetComponent<Animator>();
        maxHealth = playerData.health;
        maxMana = playerData.mana;
        maxStamina = playerData.stamina;

        HP.maxValue = maxHealth;
        HP.minValue = 0;
        HP.value = playerData.health;

        MN.maxValue = maxMana;
        MN.minValue = 0;
        MN.value = playerData.mana;

        TP.maxValue = maxStamina;
        TP.minValue = 0;
        TP.value = playerData.stamina;
        staminaTimeCounter = staminaRecoveryTime;

        playerDie = false;
    }

    private void FixedUpdate()
    {
        // Giữ player không sleep
        rb2d.position += Vector2.zero;
        MN.value = playerData.mana;
    }
    private void Update()
    {
        StaminaRecovery();
    }

    private void OnDisable()
    {
        playerData.ResetData();
    }

    /// <summary>
    /// Stamina Recovery/time
    /// </summary>
    private void StaminaRecovery()
    {
        if(staminaTimeCounter > 0)
        {
            staminaTimeCounter -= Time.deltaTime;
        }
        else
        {
            //Debug.Log("Vừa recovery xong");
            if (playerData.stamina < maxStamina)
                playerData.stamina += playerData.staminaRecovery;
            TP.value = playerData.stamina;
            staminaTimeCounter = staminaRecoveryTime;
        }
    }

    /// <summary>
    ///  Damage player gây ra 
    /// </summary>
    public void Damage(float dmg)
    {
        DamageAttack = dmg;
    }

    public void NoneDamage()
    {
        DamageAttack = 0;
    }

    /// <summary>
    /// Take damage by enemy or trap
    /// </summary>
    public void TakeDamage(float dmg)
    {
        if (!Settings.zombieMode)
        {
            if (Settings.isBlocking)
                dmg -= playerData.defense;

            if (playerData.health > 0)
            {
                playerData.health -= dmg;
                HP.value = playerData.health;
            }

            if (playerData.health <= 0)
            {
                PlayerDie();
            }
        }
    }

    private void PlayerDie()
    {
        gameObject.SetActive(false);
        playerDie = true;
    }

    // Cheat
    public void ZombieMode()
    {
        if(!Settings.zombieMode)
        {
            Settings.zombieMode = true;
            //HP.value = playerData.health;
        }
        else
        {
            Settings.zombieMode = false;
            //HP.value = playerData.health;
        }
    }
    public void PlayerRespawn()
    {
        if (playerDie)
        {
            Vector2 position = gameObject.transform.position;

            position.y += 10f;

            gameObject.transform.position = position;

            gameObject.SetActive(true);
        }
    }
}
