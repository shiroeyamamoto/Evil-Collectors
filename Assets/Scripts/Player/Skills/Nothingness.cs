using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Nothingness : Skill
{
    [SerializeField, Range(10, 200)] private int extendSpeed = 1; // Tốc độ kéo dài
    [SerializeField, Range(10, 100)] private int maxSize = 10;

    private int currentSize = 0;
    public override void ActivateSkill()
    {
        Debug.Log("canUseSkill: " + canUseSkill);
        Debug.Log("isCastingSkill: " + isCastingSkill);
        Debug.Log("Unlocked: " + Unlocked);


        if (base.canUseSkill && !base.isCastingSkill && base.Unlocked)
        {
            Vector3 playerPosition = Player.Instance.transform.position;
            position = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z);
            transform.position = position;

            scale = transform.localScale;

            this.gameObject.SetActive(true);
            StartCoroutine(NothingnessStart());

            GameController.Instance.Player.UseMana(base.manaNeed);
        }
    }

    private IEnumerator NothingnessStart()
    {
        // niệm phép 
        Settings.isAttacking = true;
        base.isCastingSkill = true;
        yield return new WaitForSeconds(base.timeCastSkill);

        // Bắt đầu cast phép
        base.canUseSkill = false;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        //this.gameObject.transform.Find("HolyLighAura").gameObject.GetComponent<SpriteRenderer>().enabled = true;

        while (maxSize > currentSize)
        {
            float newScaleY = transform.localScale.x + Time.deltaTime * extendSpeed;
            //float deltaScaleY = newScaleY - transform.localScale.y;
            //float newPosY = transform.position.y - deltaScaleY * 0.5f; // Dịch chuyển vị trí theo chiều y để giữ cho object ở vị trí trung tâm

            transform.localScale = new Vector3(newScaleY, transform.localScale.y, transform.localScale.z);
            //transform.position = new Vector3(transform.position.x, newPosY, transform.position.z);
            currentSize++;
            yield return null; // Chờ một frame
        }
        Settings.isAttacking = false;
        base.isCastingSkill = false;
        //yield return new WaitForSeconds(base.timeLifeSkill); // vòng đời hào quang ánh sáng 

        //transform.localPosition = position;
        transform.localScale = scale;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        //this.gameObject.transform.Find("HolyLighAura").gameObject.GetComponent<SpriteRenderer>().enabled = false;

        // Thời gian hồi phép 
        yield return new WaitForSeconds(base.skillCoolDown);
        currentSize = 0;
        base.canUseSkill = true;
        this.gameObject.SetActive(false);
    }
}
