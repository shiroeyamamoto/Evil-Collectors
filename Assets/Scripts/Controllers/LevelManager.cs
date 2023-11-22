using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : SingletonMonobehavious<LevelManager>
{

    public GameObject firstBoss;
    public GameObject finalBoss;

    public void Start()
    {

        if (GameController.Instance.LevelSO.ToString() == "LEVEL_01 (LevelSO)")
        {

            firstBoss = GameController.Instance.firstBossPrefab;
            Instantiate(firstBoss);
            //=Debug.Log("LEVEL_01 (LevelSO)");
        }
        if(GameController.Instance.LevelSO.ToString() == "LEVEL_02 (LevelSO)")
        {
            finalBoss = GameController.Instance.finalBossPrefab;
            //Debug.Log("LEVEL_02 (LevelSO)");
            Instantiate(finalBoss);
        }
    }

}
