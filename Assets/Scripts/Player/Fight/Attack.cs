using DG.Tweening;
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
    [SerializeField, Range(0f, 20f)] private float knockbackForce = 10f;

    private bool canAttackNormal, canAttackStrong, isUpAttack, isDownAttack;
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

        if (Settings.isCatingSkill)
            return;
        //Debug.Log("Settings.isAttacking: " + Settings.isAttacking);

        if (!Settings.PlayerDamaged )
        {

            if (Input.GetKey(KeyCode.W))
            {
                Transform weaponSize = transform.Find("WeaponSize").gameObject.transform;
                Vector3 upAttack = new Vector3(0, 0, 90);
                weaponSize.rotation = Quaternion.Euler(upAttack * transform.localScale.x);
                isUpAttack = true;
            }else if (Input.GetKey(KeyCode.S))
            {
                Transform weaponSize = transform.Find("WeaponSize").gameObject.transform;
                Vector3 downAttack = new Vector3(0, 0, -90);
                weaponSize.rotation = Quaternion.Euler(downAttack * transform.localScale.x);
                isDownAttack = true;
            }
            else if (!Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.S))
            {
                Transform weaponSize = transform.Find("WeaponSize").gameObject.transform;
                weaponSize.rotation = Quaternion.Euler(Vector3.zero);
                isUpAttack = false;
                isDownAttack = false;
            }

            if (Input.GetMouseButtonDown(0) && !Settings.isAttacking )
            {
                if (!Settings.concentrateSKill && Player.Instance.CurrentInfo.stamina >= 15)
                {
                    StartCoroutine(AttackNormal());
                }
                else if (Settings.concentrateSKill)
                {
                    StartCoroutine(AttackNormal());
                }
            }
            else if (Input.GetMouseButtonDown(1) && !Settings.isAttacking )
            {
                if (!Settings.concentrateSKill && Player.Instance.CurrentInfo.stamina >= 50)
                {
                    StartCoroutine(AttackStrong());
                }
                else if (Settings.concentrateSKill)
                {
                    StartCoroutine(AttackStrong());
                }
            }
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
        Vector3 originalPositionWeapon = new Vector3();

        if (canAttackNormal)
        {
            Settings.isAttacking = true;
            canAttackNormal = false;
            Settings.isAttackNormal = true;
            

            sword.SetActive(true);

            Player.Instance.spriteRendererPlayer.color = Color.red;

            audioSource.clip = Player.Instance.playerSound.Attack;
            audioSource.Play();

            if (!Settings.concentrateSKill)
            {
                Player.Instance.UseStamina(20);
            }
            GameController.Instance.Player.Damage(GameController.Instance.Player.CurrentInfo.damage * normalDamagePercent);

            if (isDownAttack && Settings.canKnockback)
            {
                transform.position = Vector2.Lerp(transform.position, transform.position + Vector3.up * knockbackForce, 1f);
            }

            if (isDownAttack)
            {
                if (!Settings.isFacingRight)
                {
                    playerAttack.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 135f);
                    RotateObject90Degrees(playerAttack.gameObject, normalAttackTime, 45f);
                }
                else
                {

                    playerAttack.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, -45);
                    RotateObject90Degrees(playerAttack.gameObject, normalAttackTime, -135);
                }
            }

            if (isUpAttack)
            {
                if (!Settings.isFacingRight)
                {
                    playerAttack.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 225);
                    RotateObject90Degrees(playerAttack.gameObject, normalAttackTime, 315);
                }
                else
                {
                    playerAttack.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 45);
                    RotateObject90Degrees(playerAttack.gameObject, normalAttackTime, 135);
                }
            }

            if (!isUpAttack && !isDownAttack)
            {
                if (Settings.isFacingRight)
                {
                    playerAttack.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 45f);
                    RotateObject90Degrees(playerAttack.gameObject, normalAttackTime, -45f);
                }
                else
                {
                    playerAttack.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, -45f);
                    RotateObject90Degrees(playerAttack.gameObject, normalAttackTime, 45f);
                }

                if (playerAttack.inForwardAttack)
                {
                    //playerRigid2D.velocity = new Vector2(gameObject.transform.localScale.x * attackMoveForwardForce, 0f);

                    transform.position = Vector2.Lerp(transform.position, transform.position + Vector3.right * 2*transform.localScale.x, 0.2f);

                    playerAttack.inForwardAttack = false;
                    playerAttack.inRetreatAttack = false;
                    //Settings.PlayerDamaged = false;
                }
                else if (playerAttack.inRetreatAttack)
                {
                    //playerRigid2D.velocity = new Vector2(-gameObject.transform.localScale.x * attackMoveRetreatForce, 0f);

                    transform.position = Vector2.Lerp(transform.position, transform.position + Vector3.left * 2 * transform.localScale.x, 0.2f);

                    playerAttack.inForwardAttack = false;
                    playerAttack.inRetreatAttack = false;
                    //Settings.PlayerDamaged = false;
                }
            }

            yield return new WaitForSeconds(normalAttackTime);
            Player.Instance.spriteRendererPlayer.color = Settings.playerColor;
            Settings.isAttackNormal = false;
            Settings.PlayerDamaged = false;
            sword.SetActive(false);
            Settings.isAttacking = false;
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

        // Kiếm to hơn đánh thường 
        sword.transform.localScale = new Vector2(sword.transform.localScale.x,0.5f);

        PlayerAttack playerAttack = transform.Find("WeaponSize").gameObject.transform.Find("PlayerAttack").gameObject.GetComponent<PlayerAttack>();

        if (canAttackStrong)
        {
            Settings.isAttacking = true;
            canAttackStrong = false;
            Settings.isAttackStrong = true;

            sword.SetActive(true);
            Player.Instance.spriteRendererPlayer.color = Color.red;
            audioSource.clip = Player.Instance.playerSound.Attack;
            audioSource.Play();

            if (!Settings.concentrateSKill)
            {
                Player.Instance.UseStamina(50);
            }

            GameController.Instance.Player.Damage(GameController.Instance.Player.CurrentInfo.damage * strongDamagePercent);

            if (isDownAttack && Settings.canKnockback)
            {
                transform.position = Vector2.Lerp(transform.position, transform.position + Vector3.up * knockbackForce, 1f);
            }

            if (isDownAttack)
            {
                // animation strong attack 

                if (!Settings.isFacingRight)
                {
                    playerAttack.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 135f);
                    RotateObject90Degrees(playerAttack.gameObject, normalAttackTime, 45f);
                }
                else
                {

                    playerAttack.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, -45);
                    RotateObject90Degrees(playerAttack.gameObject, normalAttackTime, -135);
                }
            }

            if (isUpAttack)
            {
                // animation strong attack 

                if (!Settings.isFacingRight)
                {
                    playerAttack.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 225);
                    RotateObject90Degrees(playerAttack.gameObject, normalAttackTime, 315);
                }
                else
                {
                    playerAttack.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 45);
                    RotateObject90Degrees(playerAttack.gameObject, normalAttackTime, 135);
                }
            }

            //Debug.Log(Settings.PlayerDamaged);
            if (!isUpAttack && !isDownAttack)
            {
                // animation strong attack 

                if (Settings.isFacingRight)
                {
                    playerAttack.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 45f);
                    RotateObject90Degrees(playerAttack.gameObject, normalAttackTime, -45f);
                }
                else
                {
                    playerAttack.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, -45f);
                    RotateObject90Degrees(playerAttack.gameObject, normalAttackTime, 45f);
                }

                if (playerAttack.inForwardAttack)
                {
                    //playerRigid2D.velocity = new Vector2(gameObject.transform.localScale.x * attackMoveForwardForce, 0f);

                    transform.position = Vector2.Lerp(transform.position, transform.position + Vector3.right * 2 * transform.localScale.x, 0.2f);

                    playerAttack.inForwardAttack = false;
                    playerAttack.inRetreatAttack = false;
                    //Settings.PlayerDamaged = false;
                }
                else if (playerAttack.inRetreatAttack)
                {
                    //playerRigid2D.velocity = new Vector2(-gameObject.transform.localScale.x * attackMoveRetreatForce, 0f);

                    transform.position = Vector2.Lerp(transform.position, transform.position + Vector3.left * 2 * transform.localScale.x, 0.2f);

                    playerAttack.inForwardAttack = false;
                    playerAttack.inRetreatAttack = false;
                    //Settings.PlayerDamaged = false;
                }
            }
            // hiệu ứng strong attack 
            /*if (Settings.isAttackStrong)
            {
                if (Settings.isFacingRight)
                {
                    PlayerManager.Instance.attackStrongParticle.transform.position = new Vector3(gameObject.transform.position.x + 2, gameObject.transform.position.y , gameObject.transform.position.z);
                }
                else if (!Settings.isFacingRight)
                {
                    PlayerManager.Instance.attackStrongParticle.transform.position = new Vector3(gameObject.transform.position.x - 2, gameObject.transform.position.y , gameObject.transform.position.z);
                }

                PlayerManager.Instance.attackStrongParticle.GetComponent<ParticleSystem>().Play();
            }*/

            yield return new WaitForSeconds(strongAttackTime);
            Player.Instance.spriteRendererPlayer.color = Settings.playerColor;
            sword.transform.localScale = new Vector2(sword.transform.localScale.x, 0.25f);
            Settings.isAttackStrong = false;
            //Settings.PlayerDamaged = false;
            sword.SetActive(false);
            Settings.isAttacking = false;
            yield return new WaitForSeconds(strongAttackCooldown);
            canAttackStrong = true;
        }   
    }
    void RotateObject90Degrees(GameObject gameObject,float duration, float value)
    {
        // Sử dụng DOTween để thực hiện tweening
        gameObject.transform.DORotate(new Vector3(0, 0, value), duration)
            .SetEase(Ease.InOutQuad).OnComplete(() => {
                gameObject.transform.DOKill();
            });
    }
    public void TweenKill()
    {
        var activeTween = DOTween.KillAll();
    }
}
