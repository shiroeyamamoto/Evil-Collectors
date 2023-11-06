using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Concentrate : Skill
{
    public float initialStaminaDecreaseRate = 5f; // Tốc độ giảm stamina ban đầu
    private float currentStaminaDecreaseRate; // Tốc độ giảm stamina hiện tại
    public float decreaseRateIncrease = 0.5f; // Tốc độ tăng giảm stamina mỗi giây

    void Start()
    {
        currentStaminaDecreaseRate = initialStaminaDecreaseRate;
    }

    public override void ActivateSkill()
    {
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
        Player.Instance.gameObject.GetComponent<SpriteRenderer>().color = Color.green;

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
        Player.Instance.gameObject.GetComponent<SpriteRenderer>().color = Color.white;

        // Thời gian hồi phép 
        yield return new WaitForSeconds(base.skillCoolDown);
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
