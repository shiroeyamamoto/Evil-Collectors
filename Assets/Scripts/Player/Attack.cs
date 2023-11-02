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
    [SerializeField, Range(0f, 20f)] private float attackMoveForce = 1f;

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
        GameObject sword = transform.Find("Attack").gameObject;

        if (canAttackNormal)
        {
            canAttackNormal = false;
            Settings.isAttackNormal = true;

            sword.SetActive(true);

            Player.Instance.spriteRendererPlayer.color = Color.red;

            audioSource.clip = Player.Instance.playerSound.Attack;
            audioSource.Play();

            if (!Settings.PlayerDamaged)
            {
                Settings.PlayerDamaged = true;
                playerRigid2D.velocity = new Vector2(gameObject.transform.localScale.x * attackMoveForce, 0f);
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
        GameObject sword = transform.Find("Attack").gameObject;

        if (canAttackStrong)
        {
            canAttackStrong = false;
            Settings.isAttackStrong = true;

            sword.SetActive(true);
            Player.Instance.spriteRendererPlayer.color = Color.red;
            audioSource.clip = Player.Instance.playerSound.Attack;
            audioSource.Play();

            GameController.Instance.PlayerData.stamina -= 25;
            Player.Instance.Damage(GameController.Instance.PlayerData.damage*strongDamagePercent);

            //Debug.Log(Settings.PlayerDamaged);

            if (!Settings.PlayerDamaged)
            {
                Settings.PlayerDamaged = true;
                playerRigid2D.velocity = new Vector2(gameObject.transform.localScale.x * attackMoveForce, 0f);
            }

            yield return new WaitForSeconds(strongAttackTime);
            Player.Instance.spriteRendererPlayer.color = Color.white;
            Settings.isAttackStrong = false;
            Settings.PlayerDamaged = false;
            sword.SetActive(false);
            yield return new WaitForSeconds(strongAttackCooldown);
            canAttackStrong = true;
        }
    }
}
