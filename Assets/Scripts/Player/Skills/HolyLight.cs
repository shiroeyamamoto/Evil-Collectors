using System.Collections;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HolyLight : Skill
{
    [SerializeField, Range(10, 200)] private int extendSpeed = 1; // Tốc độ kéo dài
    [SerializeField, Range(10, 100)] private int maxSize = 10;

    private int currentSize = 0;
    //private bool isCastingHolyLight = false, canUseHolyLight = true;
    //Vector3 position, scale;

    public override void ActivateSkill()
    {
        //Debug.Log("Mana: " + GameController.Instance.LevelSO.playerData.mana);
        //Debug.Log("TP: " + GameController.Instance.LevelSO.playerData.stamina);
        //Debug.Log("base.manaNeed: " + base.manaNeed);

        //if (GameController.Instance.LevelSO.playerData.mana < base.manaNeed)
        //    return;

        if (base.canUseSkill && !base.isCastingSkill && base.Unlocked)
        {
            Vector3 playerPosition = Player.Instance.transform.position;
            position = new Vector3(playerPosition.x, playerPosition.y + 20, playerPosition.z);
            transform.position = position;

            scale = transform.localScale;

            this.gameObject.SetActive(true);
            StartCoroutine(HolyLightStart());

            //GameController.Instance.Player.UseMana(base.manaNeed);
        }
    }
    private IEnumerator HolyLightStart()
    {
        // niệm phép 
        Settings.isAttacking = true;
        base.isCastingSkill = true;
        yield return new WaitForSeconds(base.timeCastSkill);

        // Bắt đầu cast phép
        base.canUseSkill = false;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        this.gameObject.transform.Find("HolyLighAura").gameObject.GetComponent<SpriteRenderer>().enabled = true;

        while (maxSize > currentSize)
        {
            float newScaleY = transform.localScale.y + Time.deltaTime * extendSpeed;
            float deltaScaleY = newScaleY - transform.localScale.y;
            float newPosY = transform.position.y - deltaScaleY * 0.5f; // Dịch chuyển vị trí theo chiều y để giữ cho object ở vị trí trung tâm

            transform.localScale = new Vector3(transform.localScale.x, newScaleY, transform.localScale.z);
            transform.position = new Vector3(transform.position.x, newPosY, transform.position.z);
            currentSize++;
            yield return null; // Chờ một frame
        }
        Settings.isAttacking = false;
        base.isCastingSkill = false;
        yield return new WaitForSeconds(base.timeLifeSkill); // vòng đời hào quang ánh sáng 

        // Hoàn thành phép được tự do di chuyển
        transform.localPosition = position;
        transform.localScale = scale;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.transform.Find("HolyLighAura").gameObject.GetComponent<SpriteRenderer>().enabled = false;
        
        // Thời gian hồi phép 
        yield return new WaitForSeconds(base.skillCoolDown);
        currentSize = 0;
        base.canUseSkill = true;
        this.gameObject.SetActive(false);
    }
}

