using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Concentrate : Skill
{
    [SerializeField, Range(0.1f,5f)] private float initialStaminaDecreaseRate = 1f; // Tốc độ giảm stamina ban đầu
    private float currentStaminaDecreaseRate; // Tốc độ giảm stamina hiện tại
    [SerializeField, Range(0.1f, 5f)] public float decreaseRateIncrease = 0.5f; // Tốc độ tăng giảm stamina mỗi giây
    //[SerializeField, Range(0.1f, 20f)] private float 


    [SerializeField, Range(10, 200)] private int extendSpeed = 1; // Tốc độ kéo dài
    [SerializeField, Range(10, 100)] private int maxSize = 10;
    [SerializeField, Range(0f, 5f)] private float pushForce = 0.5f;

    [SerializeField, Range(0f, 5f)] private float timeExitAura =0.5f;

    private int currentSize = 0;
    private float timeAuraCounter;


    void Start()
    {
        currentStaminaDecreaseRate = initialStaminaDecreaseRate;
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


    public override void ActivateSkill()
    {
        if (Player.Instance.CurrentInfo.health > 1)
            return;

        if (base.canUseSkill && !base.isCastingSkill && base.Unlocked)
        {
            Vector3 playerPosition = Player.Instance.transform.position;
            position = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z);
            transform.position = position;

            scale = transform.localScale;

            this.gameObject.SetActive(true);
            StartCoroutine(ConcentrateStart());
        }
    }

            

    private IEnumerator ConcentrateStart()
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
        Player.Instance.gameObject.GetComponent<SpriteRenderer>().color = Color.green;


        while (maxSize > currentSize)
        {
            float newScaleY = transform.localScale.x + Time.deltaTime * extendSpeed;

            transform.localScale = new Vector3(newScaleY, transform.localScale.y, transform.localScale.z);
            currentSize++;
            yield return null; // Chờ một frame
        }

        // Chuc nang
        if (!Settings.concentrateSKill)
        {
            Settings.concentrateSKill = true;
            Player.Instance.CurrentInfo.stamina = Player.Instance.InfoDefaultSO.stamina;

            Player.Instance.UpdatePlayerUI();
        }
        StartCoroutine(DecreaseStaminaOverTime());

        Settings.isAttacking = false;
        base.isCastingSkill = false;
        yield return new WaitForSeconds(base.timeLifeSkill); // vòng đời hào quang ánh sáng 

        if (Settings.concentrateSKill)
            Settings.concentrateSKill = false;


        transform.localScale = scale;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Player.Instance.gameObject.GetComponent<SpriteRenderer>().color = Color.white;

        // Thời gian hồi phép 
        yield return new WaitForSeconds(base.skillCoolDown);

        currentSize = 0;
        base.canUseSkill = true;
        this.gameObject.SetActive(false);
    }

    private IEnumerator DecreaseStaminaOverTime()
    {
        while (Player.Instance.CurrentInfo.stamina > 0)
        {
            // Giảm giá trị stamina dựa trên tốc độ giảm stamina hiện tại
            Player.Instance.UseStamina(currentStaminaDecreaseRate * Time.deltaTime);

            // Tăng tốc độ giảm stamina theo thời gian
            currentStaminaDecreaseRate += decreaseRateIncrease * Time.deltaTime;

            yield return null;
        }
    }
}
