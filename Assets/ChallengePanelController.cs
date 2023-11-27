using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengePanelController : MonoBehaviour
{
    private void OnEnable()
    {
        //Time.timeScale = 0;

    }
    private void OnDisable()
    {
        //Time.timeScale = 1;
    }
    public void BossFightScene()
    {
        Debug.Log("I'm ChallengeBoss");
    }

    public void CancelChallenge()
    {
        Debug.Log("I'm F*cking Scare Him");
        ClosePanel();
    }

    public void ClosePanel()
    {
        Debug.Log("Closed Panel");
        transform.root.GetComponent<StatusBossController>().ClosePanel();
        
    }
}
