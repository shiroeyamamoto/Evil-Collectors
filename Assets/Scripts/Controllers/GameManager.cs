using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    
    [SerializeField] private HomeMenuUI homeMenuUI;
    [SerializeField] private LevelManagerSO data;
    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    public void ShowLevel()
    {
        homeMenuUI.LoadLevels(data);
    }
    
    public LevelSO CurrentLevelSelection { get; private set; }

    public void LoadScene(LevelSO levelSO)
    {
        CurrentLevelSelection = levelSO;
        SceneManager.LoadScene(SCENENAME_per);
        SceneManager.LoadSceneAsync(SCENENAME_Level1, LoadSceneMode.Additive);
    }

    private const string SCENENAME_Level1 = "Level1";
    private const string SCENENAME_per = "PersistentScene";
    private const string SCENENAME_Menu = "MenuScene";
    
}
