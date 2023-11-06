using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeMenuUI : MonoBehaviour {
    [SerializeField] private Button btnPlay;
    [SerializeField] private LevelMenuUI levelMenuUI;
    [SerializeField] private BagMenuUI bagMenuUI;

    private List<UIBase> listUI;
    private void Start()
    {
        listUI = new List<UIBase>();
        btnPlay.onClick.AddListener(()=>
        {
            btnPlay.interactable = false;
            GameManager.Instance.ShowLevel();
        });
        levelMenuUI.gameObject.SetActive(false);
        levelMenuUI.OnClick += () => {
            bagMenuUI.Show();
        };
        
    }

    public void LoadLevels(LevelManagerSO data)
    {
        levelMenuUI.Show();
        levelMenuUI.LoadLevels(data);
    }

    public void TurnOnSubmit()
    {
        btnPlay.interactable = true;
    }
}
