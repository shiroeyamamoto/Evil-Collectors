using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditScript : MonoBehaviour
{
    [SerializeField] private HomeMenuUI homeMenuUI;

    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            homeMenuUI.CloseCredit();
        }

        /*if (!animator.GetCurrentAnimatorStateInfo(0).IsName("CreditAnimation"))
        {
            // Animation đã hoàn tất
            gameObject.SetActive(false);
        }*/
    }
}
