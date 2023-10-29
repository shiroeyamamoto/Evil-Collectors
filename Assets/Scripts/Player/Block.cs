using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    [SerializeField] private GameObject blockTest;
    [SerializeField] private GameObject parryTest;
    [SerializeField, Range(0.1f, 1f), Tooltip("Thời gian thực hiện block trước khi được parry")] private float timeToParry = 0.5f;
    [SerializeField, Range(0.1f, 1f), Tooltip("Thời gian parry")] private float parryTime=0.5f;

    private bool canBlock, canParry;
    private float parryCounter, blockCounter;

    private void Awake()
    {
        Settings.isBlocking = false;
        canBlock = true;
        canParry = true;
    }

    private void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.LeftControl))
        {
            BlockAction();
        }
        if (!Input.GetKey(KeyCode.LeftControl))
        {
            canBlock = true;
            Settings.isBlocking = false;
            blockTest.SetActive(false);
            canParry = true;
        }
        //Debug.Log("isBlocking: " + Settings.isBlocking);
    }

    /// <summary>
    /// Block action
    /// </summary>
    private void BlockAction()
    {
        // Kiểm tra có được block không?
        // Được block thì đặt các counter về mặc định 
        if (canBlock)
        { 
            parryCounter = parryTime;
            blockCounter = timeToParry;
            canBlock = false;
        }

        // Thời gian deplay trước khi block, tránh spam nút block
        if (blockCounter > 0 && !Settings.isBlocking)
        {
            blockCounter -= Time.deltaTime;
            //Debug.Log("blockCounter:" + blockCounter);
        }
        // Hết thời gian chờ trước khi được vào trạng thái blocking
        else
        {
            Settings.isBlocking = true;
            blockTest.SetActive(true);

            // canParry đảm bảo parry chỉ 1 lần là hủy bỏ
            if(canParry)
            {
                canParry = false;
                Settings.canParry = true;
            }
            if (parryCounter > 0)
            {
                ParryAction();
            }
        }
    }

    /// <summary>
    /// Parry action
    /// </summary>
    /// <returns></returns>
    private void ParryAction()
    {
        // animation

        // Thời gian parry
        parryCounter -= Time.deltaTime;

        // Test parry in scene 
        if(parryCounter>0)
        {
            parryTest.SetActive(true);
            Settings.isParry = true;
        }
        else
        {
            parryTest.SetActive(false);
            Settings.isParry = false;
            Settings.canParry = false;
        }
    }
}
