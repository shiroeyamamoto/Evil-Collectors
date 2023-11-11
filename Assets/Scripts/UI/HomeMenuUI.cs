using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeMenuUI : MonoBehaviour
{
    [SerializeField] private Button btnPlay;
    [SerializeField] private LevelMenuUI levelMenuUI;

    private void Start()
    {
        btnPlay.onClick.AddListener(GameManager.Instance.ShowLevel);
        levelMenuUI.gameObject.SetActive(false);
    }

    public void LoadLevels(LevelManagerSO data)
    {
        levelMenuUI.gameObject.SetActive(true);
        levelMenuUI.LoadLevels(data);
    }
}
