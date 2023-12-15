using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamehamehaSkill : Skill
{
    [SerializeField, Range(10, 200)] private int extendSpeed = 1; // Tốc độ kéo dài
    [SerializeField, Range(10, 100)] private int maxSize = 10;

    private int currentSize = 0;
    Vector3 scaleOrigin;
    private AudioSource audioSource;

    private void Start()
    {
        BulletBase butlletBase = GetComponent<BulletBase>();
        audioSource = gameObject.GetComponent<AudioSource>();
        //timeLifeSkill = Player.Instance.SkillList;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter");

        if (collision.transform.GetComponent<IInteractObject>() != null)
        {
            Player.Instance.DamageAttack = 5;

            collision.transform.GetComponent<IInteractObject>().OnDamaged(GameController.Instance.Player.DamageAttack, false);

            Debug.Log("GameController.Instance.Player.DamageAttack: " + GameController.Instance.Player.DamageAttack);

            Player.Instance.NoneDamage();

            Debug.Log("AttackDamaga");
        }
    }

    public override void ActivateSkill(int amount, float scale)
    {
        if (Player.Instance.CurrentInfo.mana < manaNeed)
        {
            Destroy(this.gameObject);
            return;
        }

        if (base.canUseSkill && !Settings.isCatingSkill && base.Unlocked)
        {
            Vector3 playerPosition = Player.Instance.transform.position;
            position = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z);
            transform.position = position;

            //scale = transform.localScale;
            transform.localScale = new Vector2(transform.localScale.x,transform.localScale.y*scale);
            scaleOrigin = transform.localScale;
            this.gameObject.SetActive(true);
            Player.Instance.UseMana(10);

            /*// Sound
            if (!audioSource.isPlaying)
            {
                audioSource.clip = PlayerSound.Instance.Kamehamameha;
                audioSource.volume = Settings.sound;
                audioSource.Play();
            }
            else
            {
                audioSource.Stop();
            }*/

            StartCoroutine(KamehamehaStart());

            //GameController.Instance.Player.UseMana(base.manaNeed);
        }
    }

    public override void ActivateSkill()
    {
        return;
    }

    private IEnumerator KamehamehaStart()
    {

        // niệm phép 
        Settings.isAttacking = true;
        Settings.isCatingSkill = true;
        Player.Instance.spriteRendererPlayer.color = Color.grey;
        yield return new WaitForSeconds(base.timeCastSkill);

        // Bắt đầu cast phép
        base.canUseSkill = false;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        Player.Instance.spriteRendererPlayer.color = Color.red;

        while (maxSize > currentSize)
        {
            float newScaleY = transform.localScale.x + Time.deltaTime * extendSpeed;
            float deltaScaleY = newScaleY - transform.localScale.x;

            float newPosY;
            if (!Settings.isFacingRight)
                newPosY = transform.position.x - deltaScaleY * 0.5f;
            else
                newPosY = transform.position.x + deltaScaleY * 0.5f;

            transform.localScale = new Vector3(newScaleY, transform.localScale.y, transform.localScale.z);

            transform.position = new Vector3(newPosY, transform.position.y, transform.position.z);
            currentSize++;
            yield return null; // Chờ một frame
        }
        Settings.isCatingSkill = false;
        yield return new WaitForSeconds(base.timeLifeSkill); // vòng đời hào quang ánh sáng 


        // Hoàn thành phép được tự do di chuyển
        Settings.isAttacking = false;
        Player.Instance.spriteRendererPlayer.color = Settings.playerColor;

        transform.localPosition = position;
        transform.localScale = scaleOrigin;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        
        // Thời gian hồi phép 
        yield return new WaitForSeconds(base.skillCoolDown);
        currentSize = 0;
        base.canUseSkill = true;
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

    public override void HoldKeySkill()
    {
        return;
    }
}
