﻿using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : SingletonMonobehavious<Player>, IInteractObject
{
    [SerializeField, Range(0.1f, 5f)] private float staminaRecoveryTime;
    [SerializeField, Range(0.1f, 5f)] private float manaRecoveryTime;

    private Rigidbody2D rb2d;
    public SpriteRenderer spriteRendererPlayer;
    public Animator animator;
    public AudioSource audioSource;
    public PlayerSound playerSound;

    float staminaTimeCounter;

    [HideInInspector] public float DamageAttack;
    private bool playerDie;
    private SO_PlayerData currentInfo;
    private SO_PlayerData infoDefaultSO;

    public void Init(SO_PlayerData playerData)
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRendererPlayer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        playerSound = GetComponent<PlayerSound>();
        animator = GetComponent<Animator>();

        this.currentInfo = new SO_PlayerData(playerData);
        infoDefaultSO = playerData;
        staminaTimeCounter = staminaRecoveryTime;
        playerDie = false;
    }
    
    private void FixedUpdate()
    {
        // Giữ player không sleep
        rb2d.position += Vector2.zero;
        //OnUpdateMana?.Invoke(currentInfo.mana);
    }
    private void Update()
    {
        StaminaRecovery();
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
            if (currentInfo.stamina < infoDefaultSO.stamina)
                currentInfo.stamina += currentInfo.staminaRecovery;
            OnUpdateTP?.Invoke(currentInfo.stamina);
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
    ///

    public Action<float> OnUpdateHP, OnUpdateMana, OnUpdateTP;
    public void TakeDamage(int dmg)
    {
        if (!Settings.zombieMode)
        {
            if (currentInfo.health > 0)
            {
                currentInfo.health -= dmg;
                OnUpdateHP?.Invoke(currentInfo.health);
            }

            if (currentInfo.health <= 0)
            {
                PlayerDie();
            }
        }
    }
    public void UseMana(float manaUsed)
    {
        if (!Settings.zombieMode)
        {
            if (currentInfo.mana > 0)
            {
                currentInfo.mana -= manaUsed;
                OnUpdateMana?.Invoke(currentInfo.mana);
            }
        }
    }

    public void UseStamina(float staminaUsed)
    {
        if (!Settings.zombieMode)
        {
            if (currentInfo.stamina > 0)
            {
                currentInfo.stamina -= staminaUsed;
                OnUpdateTP?.Invoke(currentInfo.stamina);
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

    public void OnDamage()
    {
        if (!Settings.zombieMode)
        {
            if (currentInfo.health > 0)
            {
                currentInfo.health --;
                OnUpdateHP?.Invoke(currentInfo.health);
            }

            if (currentInfo.health <= 0)
            {
                PlayerDie();
            }
        }
    }
}
