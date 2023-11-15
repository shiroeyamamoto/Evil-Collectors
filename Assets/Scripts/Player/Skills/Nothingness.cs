using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Nothingness : Skill
{
    public override void ActivateSkill()
    {
        //Debug.Log("canUseSkill: " + canUseSkill);
        //Debug.Log("isCastingSkill: " + isCastingSkill);
        Debug.Log("Unlocked: " + Unlocked);

        if (base.canUseSkill && !Settings.isCatingSkill && base.Unlocked)
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

    public override void ActivateSkill(int amount, float scale)
    {
        return;
    }

    public override void HoldKeySkill()
    {
        return;
    }

    private IEnumerator NothingnessStart()
    {
        // niệm phép 
        Settings.isAttacking = true;
        Settings.isCatingSkill = true;
        Settings.playerRenderer.color = Color.grey;
        yield return new WaitForSeconds(base.timeCastSkill);

        // Bắt đầu cast phép
        base.canUseSkill = false;
        Settings.playerRenderer.color = Color.red;
        // bat tu
        if (!Settings.nothingnessSkill)
            Settings.nothingnessSkill = true;

        Settings.isAttacking = false;
        Settings.isCatingSkill = false;
        yield return new WaitForSeconds(base.timeLifeSkill); // vòng đời hào quang ánh sáng

        Settings.playerRenderer.color = Color.white;
        if (Settings.nothingnessSkill)
            Settings.nothingnessSkill = false;

        // Thời gian hồi phép 
        yield return new WaitForSeconds(base.skillCoolDown);
        base.canUseSkill = true;
        this.gameObject.SetActive(false);
    }
}
