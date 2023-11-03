using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // Attack Controller

    [SerializeField, Range(0.1f, 1f)] private float normalAttackTime = 0.5f;
    [SerializeField, Range(0.1f, 3f)] private float strongAttackTime = 1f;
    [SerializeField, Range(0.1f, 5f)] private float normalAttackCooldown = 1f;
    [SerializeField, Range(0.1f, 5f)] private float strongAttackCooldown = 2.5f;
    [SerializeField, Range(0f, 10f)] private float normalDamagePercent = 1f;
    [SerializeField, Range(0f, 10f)] private float strongDamagePercent = 2.5f;
    [SerializeField, Range(0f, 20f)] private float attackMoveForwardForce = 1f;
    [SerializeField, Range(0f, 20f)] private float attackMoveRetreatForce = 1f;

    private bool canAttackNormal, canAttackStrong;
    private Rigidbody2D playerRigid2D;
    private AudioSource audioSource;

    private void Awake()
    {
        canAttackNormal = true;
        canAttackStrong = true;
        playerRigid2D = gameObject.GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (Settings.isDasing)
            return;
        
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(AttackNormal());
        }
        else if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(AttackStrong());
        }
    }

    /// <summary>
    /// Normal Attack
    /// </summary>
    /// <returns></returns>
    private IEnumerator AttackNormal()
    {
        GameObject sword = transform.Find("WeaponSize").gameObject.transform.Find("PlayerAttack").gameObject;

        PlayerAttack playerAttack = transform.Find("WeaponSize").gameObject.transform.Find("PlayerAttack").gameObject.GetComponent<PlayerAttack>();

        if (canAttackNormal)
        {
            canAttackNormal = false;
            Settings.isAttackNormal = true;

            sword.SetActive(true);

            Player.Instance.spriteRendererPlayer.color = Color.red;

            audioSource.clip = Player.Instance.playerSound.Attack;
            audioSource.Play();

            GameController.Instance.LevelSO.playerData.stamina -= 15;
            Player.Instance.Damage(GameController.Instance.LevelSO.playerData.damage * normalDamagePercent);

            if (!Settings.PlayerDamaged)
            {
                Settings.PlayerDamaged = true;
                if (playerAttack.inForwardAttack)
                {
                    playerRigid2D.velocity = new Vector2(gameObject.transform.localScale.x * attackMoveForwardForce, 0f);
                    playerAttack.inForwardAttack = false;
                    playerAttack.inRetreatAttack = false;
                }
                else if (playerAttack.inRetreatAttack)
                {
                    playerRigid2D.velocity = new Vector2(-gameObject.transform.localScale.x * attackMoveRetreatForce, 0f);
                    playerAttack.inForwardAttack = false;
                    playerAttack.inRetreatAttack = false;
                }
            }

            yield return new WaitForSeconds(normalAttackTime);
            Player.Instance.spriteRendererPlayer.color = Color.white;
            Settings.isAttackNormal = false;
            Settings.PlayerDamaged = false;
            sword.SetActive(false);
            yield return new WaitForSeconds(normalAttackCooldown);
            canAttackNormal = true;
        }
    }

    /// <summary>
    /// Strong Attack
    /// </summary>
    /// <returns></returns>
    private IEnumerator AttackStrong()
    {
        GameObject sword = transform.Find("WeaponSize").gameObject.transform.Find("PlayerAttack").gameObject;

        PlayerAttack playerAttack = transform.Find("WeaponSize").gameObject.transform.Find("PlayerAttack").gameObject.GetComponent<PlayerAttack>();

        if (canAttackStrong)
        {
            canAttackStrong = false;
            Settings.isAttackStrong = true;

            sword.SetActive(true);
            Player.Instance.spriteRendererPlayer.color = Color.red;
            audioSource.clip = Player.Instance.playerSound.Attack;
            audioSource.Play();

            GameController.Instance.LevelSO.playerData.stamina -= 25;
            Player.Instance.Damage(GameController.Instance.LevelSO.playerData.damage*strongDamagePercent);

            //Debug.Log(Settings.PlayerDamaged);

            if (!Settings.PlayerDamaged)
            {
                //Settings.PlayerDamaged = true;
                if (playerAttack.inForwardAttack)
                {
                    playerRigid2D.velocity = new Vector2(gameObject.transform.localScale.x * attackMoveForwardForce, 0f);
                    playerAttack.inForwardAttack = false;
                    playerAttack.inRetreatAttack = false;
                }
                else if (playerAttack.inRetreatAttack)
                {
                    playerRigid2D.velocity = new Vector2(-gameObject.transform.localScale.x * attackMoveRetreatForce, 0f);
                    playerAttack.inForwardAttack = false;
                    playerAttack.inRetreatAttack = false;
                }
            }

            yield return new WaitForSeconds(strongAttackTime);
            Player.Instance.spriteRendererPlayer.color = Color.white;
            Settings.isAttackStrong = false;
            //Settings.PlayerDamaged = false;
            sword.SetActive(false);
            yield return new WaitForSeconds(strongAttackCooldown);
            canAttackStrong = true;
        }
    }
}
