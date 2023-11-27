using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SingletonMonobehavious<Player>, IInteractObject
{
    [SerializeField, Range(0.1f, 5f)] private float staminaRecoveryTime;
    [SerializeField, Range(0.1f, 5f)] private float manaRecoveryTime;
    [SerializeField, Range(0.5f, 5f)] private float undeadTime =2f;

    [HideInInspector] public Rigidbody2D rb2d ;
    public SpriteRenderer spriteRendererPlayer, hatSpriteRenderer;
    public Animator animator;
    public AudioSource audioSource;
    public PlayerSound playerSound;

    float staminaTimeCounter;

    [HideInInspector] public float DamageAttack;
    private bool playerDie;
    public float undeadCounter;
    public SO_PlayerData CurrentInfo { get; private set; }
    public SO_PlayerData InfoDefaultSO { get; private set; }
    
    public List<SkillBase> SkillList { get; private set; }

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    public void Init(SO_PlayerData playerData)
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRendererPlayer = transform.Find("Body").gameObject.GetComponent<SpriteRenderer>();
        hatSpriteRenderer = transform.Find("Hat").gameObject.GetComponent<SpriteRenderer>();
        Settings.playerColor = spriteRendererPlayer.color;
        Settings.playerHatColor = hatSpriteRenderer.color;
        audioSource = GetComponent<AudioSource>();
        playerSound = GetComponent<PlayerSound>();
        animator = GetComponent<Animator>();

        this.CurrentInfo = new SO_PlayerData(playerData);
        InfoDefaultSO = playerData;

        // bắt đầu game mana = 0
        CurrentInfo.mana = 0;

        staminaTimeCounter = staminaRecoveryTime;
        playerDie = false;
        SkillList = new List<SkillBase>();

        Settings.isFacingRight = true;
    }
    
    private void FixedUpdate()
    {
        // Giữ player không sleep
        rb2d.position += Vector2.zero;
        //OnUpdateMana?.Invoke(CurrentInfo.mana);
    }
    private void Update()
    {
        UndeadTime();

        if (!Settings.concentrateSKill) 
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
            if (CurrentInfo.stamina < InfoDefaultSO.stamina)
                CurrentInfo.stamina += CurrentInfo.staminaRecovery;
            
            OnUpdateTP?.Invoke(CurrentInfo.stamina);
            staminaTimeCounter = staminaRecoveryTime;
        }
    }

    public void Damage(float dmg)
    {
        DamageAttack = dmg;
    }
    
    public void NoneDamage()
    {
        DamageAttack = 0;
    }

    public Action<float> OnUpdateHP, OnUpdateMana, OnUpdateTP;
    public Action<bool> OnDead;
    public Action<int> OnDamagedTwinkling;

    public void UseHealth(int healthUsed)
    {
        if (!Settings.zombieMode)
        {
            if (CurrentInfo.health > 0)
            {
                CurrentInfo.health -= healthUsed;
                if (CurrentInfo.health > InfoDefaultSO.health)
                    CurrentInfo.health = InfoDefaultSO.health;
                OnUpdateHP?.Invoke(CurrentInfo.health);
            }
        }
    }

    public void UseMana(float manaUsed)
    {
        if (!Settings.zombieMode)
        {
            if (CurrentInfo.mana > 0)
            {
                CurrentInfo.mana -= manaUsed;
                if (CurrentInfo.mana < 0)
                    CurrentInfo.mana = 0;
                OnUpdateMana?.Invoke(CurrentInfo.mana);
            }
        }
    }

    public void UseStamina(float staminaUsed)
    {
        if (!Settings.zombieMode)
        {
            if (CurrentInfo.stamina > 0)
            {
                CurrentInfo.stamina -= staminaUsed;
                if (CurrentInfo.stamina < 0)
                    CurrentInfo.stamina = 0;
                OnUpdateTP?.Invoke(CurrentInfo.stamina);
            }
        }
    }

    private void PlayerDie()
    {
        gameObject.SetActive(false);
        playerDie = true;
        OnDead?.Invoke(false);
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

    public void IncreaseHp(int value)
    {
        CurrentInfo.health += value;
        if (CurrentInfo.health > InfoDefaultSO.health) {
            CurrentInfo.health = InfoDefaultSO.health;
        }
    }
    
    public void IncreaseMana(float value)
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

    /*public SkillBase AddSkill(ActiveItem activeItem) {
        SkillBase skill = gameObject.AddComponent(MapSkillScript(activeItem.SkillName)) as SkillBase;
        if (skill) {
            skill.Init(activeItem);
            SkillList.Add(skill);
        }

        return skill;
    }*/

    private Type MapSkillScript(SkillName skillName)
    {
        switch (skillName)
        {
            case SkillName.FireSphere: return typeof(FireSphere);
            case SkillName.Kamehameha: return typeof(Kamehameha);

            default: return null;
        }
    }

    public void OnDamaged(float dmgTake)
    {
        if (!Settings.zombieMode)
        {
            if (Settings.nothingnessSkill || Settings.concentrateSKill)
                return;

            if (CurrentInfo.health > 0)
            {
                CurrentInfo.health --;
                OnUpdateHP?.Invoke(CurrentInfo.health);
                Settings.zombieMode = true;
                undeadCounter = undeadTime;

            }

            if (CurrentInfo.health <= 0)
            {
                PlayerDie();
            }
        }

        
    }

    private void UndeadTime()
    {
        if (undeadCounter > 0)
        {
            undeadCounter -= Time.deltaTime;

            GetComponent<CapsuleCollider2D>().enabled = true;

            gameObject.transform.Find("Body").gameObject.GetComponent<CapsuleCollider2D>().enabled = false;

            StartCoroutine(Twinkling());
        }
        else
        {
            gameObject.transform.Find("Body").gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
            GetComponent<CapsuleCollider2D>().enabled = false;
            undeadCounter = 0;
            Settings.zombieMode = false;
            spriteRendererPlayer.color = Settings.playerColor;
            //hatSpriteRenderer.color = Settings.playerHatColor;
            OnDamagedTwinkling?.Invoke(1);
            OnDamagedTwinkling?.Invoke(2);
            //StopCoroutine(Twinkling());
        }
    }

    private IEnumerator Twinkling()
    {
        while (undeadCounter > 0)
        {
            spriteRendererPlayer.color = Color.white;
            hatSpriteRenderer.color = Color.white;
            OnDamagedTwinkling?.Invoke(1);
            yield return new WaitForSeconds(0.3f);
            spriteRendererPlayer.color = Settings.playerColor;
            hatSpriteRenderer.color = Settings.playerHatColor;
            OnDamagedTwinkling?.Invoke(2);
            yield return new WaitForSeconds(0.3f);
        }
    }

        public void UpdatePlayerUI()
    {
        OnUpdateHP?.Invoke(Player.Instance.CurrentInfo.health);
        OnUpdateMana?.Invoke(Player.Instance.CurrentInfo.mana);
        OnUpdateTP?.Invoke(Player.Instance.CurrentInfo.stamina);
    }

    public void OnDamaged(float damage, bool value)
    {
        return;
    }
}
