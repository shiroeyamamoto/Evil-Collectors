using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : SingletonMonobehavious<Player>
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
    public SO_PlayerData CurrentInfo { get; private set; }
    public SO_PlayerData InfoDefaultSO { get; private set; }
    
    public List<SkillBase> SkillList { get; private set; }

    public void Init(SO_PlayerData playerData)
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRendererPlayer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        playerSound = GetComponent<PlayerSound>();
        animator = GetComponent<Animator>();

        this.CurrentInfo = new SO_PlayerData(playerData);
        InfoDefaultSO = playerData;
        staminaTimeCounter = staminaRecoveryTime;
        playerDie = false;
    }
    
    private void FixedUpdate()
    {
        // Giữ player không sleep
        rb2d.position += Vector2.zero;
        OnUpdateMana?.Invoke(CurrentInfo.mana);
    }
    private void Update()
    {
        StaminaRecovery();
    }

    private void OnDisable()
    {
        CurrentInfo.ResetData();
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
            if (CurrentInfo.stamina < InfoDefaultSO.stamina)
                CurrentInfo.stamina += CurrentInfo.staminaRecovery;
            
            OnUpdateTP?.Invoke(CurrentInfo.stamina);
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
    public void TakeDamage(float dmg)
    {
        if (!Settings.zombieMode)
        {
            if (Settings.isBlocking)
                dmg -= CurrentInfo.defense;

            if (CurrentInfo.health > 0)
            {
                CurrentInfo.health -= dmg;
                OnUpdateHP?.Invoke(CurrentInfo.health);
            }

            if (CurrentInfo.health <= 0)
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

    public void IncreaceHp(float value)
    {
        CurrentInfo.health += value;
        if (CurrentInfo.health > InfoDefaultSO.health) {
            CurrentInfo.health = InfoDefaultSO.health;
        }
    }
    
    public void IncreaceMana(float value)
    {
        CurrentInfo.mana += value;
        if (CurrentInfo.mana > InfoDefaultSO.mana) {
            CurrentInfo.mana = InfoDefaultSO.mana;
        }
    }

    public void UpdateSkill(SkillBase skill, SupportItem supportItem) {
        skill.UpdateSkill(supportItem);
    }

    public SkillBase GetSkillByBase(SkillBase skill)
    {
        for (int i = 0; i < SkillList.Count; i++) {
            if (SkillList[i] == skill) {
                return SkillList[i];
            }  
        }

        return null;
    }

    private List<SkillBase> skills;
    
    public SkillBase AddSkill(BoosterType type) {
        SkillBase skill = gameObject.AddComponent(MapSkillScript(type)) as SkillBase;
        if (skill) {
            //booster.SetLocalVfxParent(GetComponent<Character>().GraphicTf);
            skills.Add(skill);
        }

        return skill;
    }

    private Type MapSkillScript(BoosterType type) {
        switch (type) {
            case BoosterType.FireSphere: return typeof(FireSphere);
            case BoosterType.Kamehameha: return typeof(Kamehameha);
                
            default: return null;
        }
    }
}
