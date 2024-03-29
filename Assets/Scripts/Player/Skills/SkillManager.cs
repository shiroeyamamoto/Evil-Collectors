﻿using UnityEngine;

public class SkillManager : SingletonMonobehavious<SkillManager>
{
    [SerializeField] private HolyLight holyLightSkill;
    [SerializeField] private Skill nothingnessSkill;
    [SerializeField] private Skill concentrateSkill;

    private int currentItemQuickKey;

    [SerializeField] private ItemSwitcher itemSwitcher;

    public Skill ConcentrateSkill { get => concentrateSkill; set => concentrateSkill = value; }

    private void Start()
    {
        holyLightSkill.gameObject.SetActive(false);
        nothingnessSkill.gameObject.SetActive(false);
        ConcentrateSkill.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!Settings.isGrounded)
            return;


        if (Settings.PlayerDamaged)
        {
            holyLightSkill.cancelSkill = true;
            holyLightSkill.HoldKeySkill();
            return;
        }
        /*else
        {
            holyLightSkill.cancelSkill = false;
        }*/

        if (!Settings.isAttacking && !Settings.PlayerDamaged && !Settings.isDasing && !Settings.isMove)
        {
            // Quick key
            if (Input.GetKeyDown(KeyCode.Q))
            {
                itemSwitcher.UseItem();
                itemSwitcher.QuickKeyCheck();
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
                if (GameController.Instance.Player.CurrentInfo.mana < ConcentrateSkill.manaNeed)
                    return;

                ConcentrateSkill.ActivateSkill();
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
