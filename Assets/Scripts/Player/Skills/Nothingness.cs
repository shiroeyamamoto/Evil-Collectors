using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Nothingness : Skill
{
    [SerializeField, Range(10, 200)] private int extendSpeed = 1; // Tốc độ kéo dài
    [SerializeField, Range(10, 100)] private int maxSize = 10;
    [SerializeField, Range(0f, 5f)] private float pushForce = 0.5f;

    private int currentSize = 0;

    public override void ActivateSkill()
    {
        //Debug.Log("canUseSkill: " + canUseSkill);
        //Debug.Log("isCastingSkill: " + isCastingSkill);
        //Debug.Log("Unlocked: " + Unlocked);

        if (base.canUseSkill && !base.isCastingSkill && base.Unlocked)
        {
            Vector3 playerPosition = Player.Instance.transform.position;
            position = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z);
            transform.position = position;

            scale = transform.localScale;

            this.gameObject.SetActive(true);
            StartCoroutine(NothingnessStart());

            //GameController.Instance.Player.UseMana(base.manaNeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Rigidbody2D enemyRigid2D = collision.gameObject.GetComponent<Rigidbody2D>();

            // Đẩy kẻ địch khi đánh trúng
            if (Player.Instance.transform.position.x < collision.transform.position.x)
            {
                enemyRigid2D.velocity = new Vector2(collision.gameObject.transform.localScale.x * pushForce, 0f);
            }
            else if (Player.Instance.transform.position.x > collision.transform.position.x)
            {
                enemyRigid2D.velocity = new Vector2(-collision.gameObject.transform.localScale.x * pushForce, 0f);
            }

            Player.Instance.NoneDamage();
        }
    }

    private IEnumerator NothingnessStart()
    {
        // niệm phép 
        Settings.isAttacking = true;
        base.isCastingSkill = true;
        Player.Instance.gameObject.GetComponent<SpriteRenderer>().color = Color.grey;
        yield return new WaitForSeconds(base.timeCastSkill);

        // Bắt đầu cast phép
        base.canUseSkill = false;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        Player.Instance.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        // bat tu
        if (!Settings.nothingnessSkill)
            Settings.nothingnessSkill = true;

        while (maxSize > currentSize)
        {
            float newScaleY = transform.localScale.x + Time.deltaTime * extendSpeed;

            transform.localScale = new Vector3(newScaleY, transform.localScale.y, transform.localScale.z);
            currentSize++;
            yield return null; // Chờ một frame
        }
        Settings.isAttacking = false;
        base.isCastingSkill = false;
        yield return new WaitForSeconds(base.timeLifeSkill); // vòng đời hào quang ánh sáng

        Player.Instance.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        if (Settings.nothingnessSkill)
            Settings.nothingnessSkill = false;

        transform.localScale = scale;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;

        // Thời gian hồi phép 
        yield return new WaitForSeconds(base.skillCoolDown);
        currentSize = 0;
        base.canUseSkill = true;
        this.gameObject.SetActive(false);
    }
}
