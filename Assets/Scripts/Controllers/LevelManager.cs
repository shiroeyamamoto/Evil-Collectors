using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : SingletonMonobehavious<LevelManager>
{

    public GameObject firstBoss;
    public GameObject finalBoss;

    private void Awake()
    {
        base.Awake();
        firstBoss.SetActive(false);
        finalBoss.SetActive(false);
    }

    public void Start()
    {

        if (GameController.Instance.LevelSO.ToString() == "LEVEL_01 (LevelSO)")
        {
            //firstBoss = GameController.Instance.firstBossPrefab;
            firstBoss.SetActive(true);
            //=Debug.Log("LEVEL_01 (LevelSO)");
        }
        if (GameController.Instance.LevelSO.ToString() == "LEVEL_02 (LevelSO)")
        {
            //finalBoss = GameController.Instance.finalBossPrefab;
            //Debug.Log("LEVEL_02 (LevelSO)");
            finalBoss.SetActive(true);
        }
    }

}