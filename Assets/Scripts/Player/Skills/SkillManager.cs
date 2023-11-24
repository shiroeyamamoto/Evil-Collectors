using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private HolyLight holyLightSkill;
    [SerializeField] private Skill nothingnessSkill;
    [SerializeField] private Skill concentrateSkill;

    private int currentItemQuickKey;

    [SerializeField] private ItemSwitcher itemSwitcher;

    private void Start()
    {
        holyLightSkill.gameObject.SetActive(false);
        nothingnessSkill.gameObject.SetActive(false);
        concentrateSkill.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!Settings.isGrounded)
            return;


        if (Settings.PlayerDamaged)
        {
            holyLightSkill.cancelSkill = true;
            holyLightSkill.HoldKeySkill();
            /*if (!holyLightSkill.cancelSkill)
                holyLightSkill.ActivateSkill();
            else
                holyLightSkill.cancelSkill = false;*/
            return;
        }

        if (!Settings.isAttacking && !Settings.PlayerDamaged && !Settings.isDasing && !Settings.isMove)
        {


            // Quick key
            if (Input.GetKeyDown(KeyCode.Q))
            {
                itemSwitcher.UseItem();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
            }

            // HolyLight
            if (Input.GetKey(KeyCode.Z))
            { 
                if (Settings.isMove)
                    return;

                if (Input.GetKeyDown(KeyCode.Space))
                { 
                    holyLightSkill.cancelSkill = true;
                    return;
                }

                holyLightSkill.HoldKeySkill();
            }
            if (Input.GetKeyUp(KeyCode.Z))
            {
                if (!holyLightSkill.cancelSkill)
                    holyLightSkill.ActivateSkill();
                else
                    holyLightSkill.cancelSkill = false;
            }

            // nothingness
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (GameController.Instance.Player.CurrentInfo.mana < nothingnessSkill.manaNeed)
                    return;

                nothingnessSkill.ActivateSkill();
            }

            // concentrate
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (GameController.Instance.Player.CurrentInfo.mana < nothingnessSkill.manaNeed)
                    return;

                concentrateSkill.ActivateSkill();
            }
        }

        // Kiểm tra lăn chuột
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput > 0f)
        {
            // Lăn lên, chọn item tiếp theo
            SelectNextItem();
        }
        else if (scrollInput < 0f)
        {
            // Lăn xuống, chọn item trước đó
            SelectPreviousItem();
        }
    }

    private void SelectPreviousItem()
    {

    }

    private void SelectNextItem()
    {

    }

    private void UseItemQuickKey(int index)
    {
        
    }
}
