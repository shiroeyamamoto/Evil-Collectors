using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private Skill holyLightSkill;

    private void Start()
    {
        holyLightSkill.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!Settings.isGrounded)
            return;

        if (!Settings.isAttacking && !Settings.PlayerDamaged && !Settings.isDasing)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                holyLightSkill.ActivateSkill();
            }
        }
    }
}
