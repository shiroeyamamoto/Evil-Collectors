using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private Skill holyLightSkill;
    [SerializeField] private Skill nothingnessSkill;
    [SerializeField] private Skill concentrateSkill;
    [SerializeField] private Skill kamehamehaSkill;
    [SerializeField] private Skill fireBallSkill;

    private void Start()
    {
        holyLightSkill.gameObject.SetActive(false);
        nothingnessSkill.gameObject.SetActive(false);
        concentrateSkill.gameObject.SetActive(false);
        kamehamehaSkill.gameObject.SetActive(false);
        fireBallSkill.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!Settings.isGrounded)
            return;

        if (!Settings.isAttacking && !Settings.PlayerDamaged && !Settings.isDasing)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {

                //kamehamehaSkill.ActivateSkill(0, 5);
                foreach (var item in Player.Instance.SkillList)
                {
                    item.UseToDir(new Vector2());
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                //fireBallSkill.ActivateSkill(2,5f);
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {

                if (GameController.Instance.Player.CurrentInfo.mana < holyLightSkill.manaNeed)
                    return;

                holyLightSkill.ActivateSkill();
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (GameController.Instance.Player.CurrentInfo.mana < nothingnessSkill.manaNeed)
                    return;

                nothingnessSkill.ActivateSkill();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (GameController.Instance.Player.CurrentInfo.mana < nothingnessSkill.manaNeed)
                    return;

                concentrateSkill.ActivateSkill();
            }
        }
    }
}
