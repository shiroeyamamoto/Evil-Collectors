using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            // Nguyen Code
            StartCoroutine(LoadScene(loadTime));

            // Quan Code
            //GameManager.Instance.ShowLevel();

        });
        levelMenuUI.gameObject.SetActive(false);
        levelMenuUI.OnClick += () => {
            GameManager.Instance.LoadSceneLevel();
            // origin of Quan dev
            //bagMenuUI.Show();
        };
        
    }
    [Space]
    public Animator animator;
    public float loadTime;
    [SerializeField] private string sceneLoadString;
    public IEnumerator LoadScene(float loadTime)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(loadTime);
        SceneManager.LoadScene("PersistentScene");
        SceneManager.LoadScene(sceneLoadString,LoadSceneMode.Additive);
    }
    public void LoadLevels(LevelManagerSO data)
    {
        levelMenuUI.Show();
        levelMenuUI.LoadLevels(data);
    }
}
