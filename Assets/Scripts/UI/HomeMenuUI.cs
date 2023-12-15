using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeMenuUI : MonoBehaviour {

    [SerializeField] private Button btnPlay;
    [SerializeField] private LevelMenuUI levelMenuUI;
    [SerializeField] private BagMenuUI bagMenuUI;
    [SerializeField] private GameObject credit;

    [Header("Sound")]
    private AudioSource audioSource;
    [SerializeField] private AudioClip menuClip;
    [SerializeField] private AudioClip creditClip;

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
        credit.SetActive(false);
        levelMenuUI.OnClick += () => {
            GameManager.Instance.LoadSceneLevel();
            // origin of Quan dev
            //bagMenuUI.Show();
        };
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = menuClip;
        audioSource.volume = Settings.sound - 0.5f;
        audioSource.Play();
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

    public void OpenCredit()
    {
        credit.SetActive(true);
        audioSource.clip = creditClip;
        audioSource.volume = Settings.sound-0.5f;
        audioSource.Play();
    }

    public void CloseCredit()
    {
        credit.SetActive(false);
        audioSource.clip = menuClip;
        audioSource.volume = Settings.sound - 0.5f;
        audioSource.Play();
    }

    public void CloseApp()
    {
        Application.Quit();
    }
}
