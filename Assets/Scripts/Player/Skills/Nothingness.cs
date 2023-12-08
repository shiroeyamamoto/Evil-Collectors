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

            GameController.Instance.Player.UseMana(Player.Instance.InfoDefaultSO.mana*(70f/100f));
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
        Debug.Log("nothingness skill");

        // niệm phép 
        Settings.isAttacking = true;
        Settings.isCatingSkill = true;
        Player.Instance.spriteRendererPlayer.color = Color.grey;
        yield return new WaitForSeconds(base.timeCastSkill);

        // Bắt đầu cast phép
        base.canUseSkill = false;
        Player.Instance.spriteRendererPlayer.color = Color.red;
        Player.Instance.spriteRendererPlayer.material.color = Color.red*3f;
        // bat tu
        if (!Settings.nothingnessSkill)
            Settings.nothingnessSkill = true;

        Settings.isAttacking = false;
        Settings.isCatingSkill = false;
        yield return new WaitForSeconds(base.timeLifeSkill); // vòng đời hào quang ánh sáng

        Player.Instance.spriteRendererPlayer.color = Settings.playerColor;
        Player.Instance.spriteRendererPlayer.material.color = Settings.playerColor;
        if (Settings.nothingnessSkill)
            Settings.nothingnessSkill = false;

        // Thời gian hồi phép 
        yield return new WaitForSeconds(base.skillCoolDown);
        base.canUseSkill = true;
        this.gameObject.SetActive(false);
    }
}
