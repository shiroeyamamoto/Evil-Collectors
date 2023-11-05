using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private Skill holyLightSkill;
    [SerializeField] private Skill nothingnessSkill;

    private void Start()
    {
        holyLightSkill.gameObject.SetActive(false);
        nothingnessSkill.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!Settings.isGrounded)
            return;

        if (!Settings.isAttacking && !Settings.PlayerDamaged && !Settings.isDasing)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {

                //if (GameController.Instance.Player.CurrentInfo.mana < holyLightSkill.manaNeed)
                //    return;

                holyLightSkill.ActivateSkill();
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                //if (GameController.Instance.Player.CurrentInfo.mana < nothingnessSkill.manaNeed)
                //    return;

                nothingnessSkill.ActivateSkill();
            }
        }
    }
}
