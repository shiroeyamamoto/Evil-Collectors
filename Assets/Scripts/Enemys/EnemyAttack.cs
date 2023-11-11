using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRendererGameObject;
    [SerializeField, Range(0f, 1f)] private float chargingAttack;
    [SerializeField, Range(0f, 10f)] private float timeAttack = 0.5f;
    [SerializeField, Range(1f, 10f)] private float timeCooldown = 2f;
    [SerializeField, Range(0f, 10f)] private float normalDamagePercent = 1f;
    [SerializeField, Range(0f, 5f)] private float pushForce = 2f;

    private bool canAttack = true;
    private float chargingAttackCooldown;
    private SpriteRenderer spriteRendererBackup = new SpriteRenderer();

    private void Awake()
    {
        spriteRendererBackup = spriteRendererGameObject;
    }
    private void Start()
    {
        canAttack = true;
        chargingAttackCooldown = chargingAttack;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (chargingAttackCooldown > 0)
            {
                chargingAttackCooldown -= Time.deltaTime;
                spriteRendererGameObject.color = Color.red;
            }
            else
            {
                //Debug.Log("Đang tấn công");
                EnemyBody thisEnemyBody = gameObject.GetComponentInParent<EnemyBody>();
                StartCoroutine(AttackNormal(thisEnemyBody, collision));
                //  canAttack = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        spriteRendererGameObject.color = Color.yellow;
        chargingAttackCooldown = chargingAttack;
    }
    private IEnumerator AttackNormal(EnemyBody thisEnemyBody, Collider2D collision)
    {
        GameObject sword = transform.Find("Attack").gameObject;
        if (canAttack)
        {
            canAttack = false;
            Settings.isAttackNormal = true;

            sword.SetActive(true);
            if (Settings.canParry)
            {
                // khi bị player parry
                // damage parry mặc định bằng 10% sát thương của player + 10% sát thương đòn đánh của enemy
                if (Settings.canParry)
                {
                    thisEnemyBody.EnemyTakeDamge(thisEnemyBody.parryDamaged);
                    Settings.canParry = false;
                }
                //Debug.Log("Parry Damage:" + thisEnemyBody.parryDamaged);
            }
            else
            {
                if (!Settings.PlayerDamaged)
                {
                    Settings.PlayerDamaged = true;
                    //Debug.Log("Enemy đã đánh trúng player");
                    //Player.Instance.TakeDamage(1);

                    Rigidbody2D playerRidid2D = collision.gameObject.GetComponent<Rigidbody2D>();

                    // Đẩy player khi đánh trúng
                    // Kiểm tra vị trí của player đang ở bên trái hay bên phải enemy
                    // Kiểm tra player đang quay lưng hay đối mặt enemy
                    if (gameObject.transform.position.x < collision.transform.position.x)
                    {
                        if (collision.gameObject.transform.localScale.x > 0)
                        {
                            playerRidid2D.velocity = new Vector2(collision.gameObject.transform.localScale.x * pushForce, 0f);
                        }
                        else
                        {
                            playerRidid2D.velocity = new Vector2(-collision.gameObject.transform.localScale.x * pushForce, 0f);
                        }
                    }
                    else if(gameObject.transform.position.x > collision.transform.position.x)
                    {
                        if (collision.gameObject.transform.localScale.x > 0)
                        {
                            playerRidid2D.velocity = new Vector2(-collision.gameObject.transform.localScale.x * pushForce, 0f);
                        }
                        else
                        {
                            playerRidid2D.velocity = new Vector2(collision.gameObject.transform.localScale.x * pushForce, 0f);
                        }
                    }
                }
            }
            yield return new WaitForSeconds(timeAttack);
            Settings.isAttackNormal = false;
            Settings.PlayerDamaged = false;
            //Debug.Log(spriteRendererGameObject.color);
            //Debug.Log(spriteRendererBackup.color);
            spriteRendererGameObject.color = Color.yellow;
            sword.SetActive(false);
            yield return new WaitForSeconds(timeCooldown);
            canAttack = true;
            chargingAttackCooldown = chargingAttack;
        }
    }
}
