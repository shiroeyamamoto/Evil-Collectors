using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeMenuUI : MonoBehaviour {
    public Action OnSubmit;
    [SerializeField] private Button btnPlay;
    [SerializeField] private LevelMenuUI levelMenuUI;
    [SerializeField] private BagMenuUI bagMenuUI;

    private List<UIBase> listUI;
    private void Start()
    {
        listUI = new List<UIBase>();
        btnPlay.onClick.AddListener(GameManager.Instance.ShowLevel);
        levelMenuUI.gameObject.SetActive(false);
        levelMenuUI.OnClick += () => {
            bagMenuUI.Show();
        };
        listUI.Add(levelMenuUI);
        GameManager.Instance.SetHomeUI(this);
        bagMenuUI.OnSubmit += OnSubmit;
    }

    public void LoadLevels(LevelManagerSO data)
    {
        levelMenuUI.Show();
        levelMenuUI.LoadLevels(data);
    }
}
