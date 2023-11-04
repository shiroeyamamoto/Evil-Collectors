using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeMenuUI : MonoBehaviour
{
    [SerializeField] private Button btnPlay;
    [SerializeField] private LevelMenuUI levelMenuUI;

    private List<UIBase> listUI;
    private void Start()
    {
        listUI = new List<UIBase>();
        btnPlay.onClick.AddListener(GameManager.Instance.ShowLevel);
        levelMenuUI.gameObject.SetActive(false);
        listUI.Add(levelMenuUI);
    }

    public void LoadLevels(LevelManagerSO data)
    {
        levelMenuUI.Show();
        levelMenuUI.LoadLevels(data);
    }
}
