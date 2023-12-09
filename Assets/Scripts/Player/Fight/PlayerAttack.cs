using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField, Range(0f, 5f)] private float pushForce = 0.5f;

    [HideInInspector] public bool inForwardAttack = false;
    [HideInInspector] public bool inRetreatAttack = false;

    private bool soundEnable;

    AudioSource audioSource;
    private void Start()
    {
        this.gameObject.SetActive(false);
        audioSource = gameObject.GetComponent<AudioSource>();
        soundEnable = false;
    }


    private void Update()
    {
        //PlayAttackBossSound();
    }

    private void PlayAttackBossSound()
    {
        if (soundEnable)
        {
            audioSource.clip = Player.Instance.gameObject.GetComponent<PlayerSound>().PlayerAttackBoss;
            audioSource.volume = 0.3f;
            audioSource.Play();
            soundEnable = false;
        }
    }

    // Va chạm của đòn tấn công tới đối thủ

    /// <summary>
    /// Đòn tấn công trúng enemy gây sát thương
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<IInteractObject>() != null)
        {
            collision.transform.GetComponent<IInteractObject>().OnDamaged(GameController.Instance.Player.DamageAttack, true);

            soundEnable = true;

            // hiệu ứng bloodParticle 
            if (Settings.isFacingRight)
            {
                PlayerManager.Instance.bloodParticle.transform.rotation = Quaternion.Euler(0, -45, 0);

                PlayerManager.Instance.bloodParticle.transform.position = new Vector3(gameObject.transform.position.x+2, gameObject.transform.position.y, gameObject.transform.position.z);
            }
            else if(!Settings.isFacingRight)
            {
                PlayerManager.Instance.bloodParticle.transform.rotation = Quaternion.Euler(0, 45, 0);
                PlayerManager.Instance.bloodParticle.transform.position = new Vector3(gameObject.transform.position.x - 2, gameObject.transform.position.y, gameObject.transform.position.z);
            }

            PlayerManager.Instance.bloodParticle.GetComponent<ParticleSystem>().Play();

            // hiệu ứng strong attack 
            /*if (Settings.isAttackStrong)
            {
                if (Settings.isFacingRight)
                {
                    PlayerManager.Instance.attackStrongParticle.transform.position = new Vector3(gameObject.transform.position.x + 2, gameObject.transform.position.y - 2, gameObject.transform.position.z);
                }
                else if (!Settings.isFacingRight)
                {
                    PlayerManager.Instance.attackStrongParticle.transform.position = new Vector3(gameObject.transform.position.x - 2, gameObject.transform.position.y - 2, gameObject.transform.position.z);
                }

                PlayerManager.Instance.attackStrongParticle.GetComponent<ParticleSystem>().Play();
            }*/

            Player.Instance.NoneDamage();

        }   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Settings.canKnockback = false;

        }
    }
}
