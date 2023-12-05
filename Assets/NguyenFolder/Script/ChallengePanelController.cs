using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class ChallengePanelController : MonoBehaviour
{
    [SerializeField] public LevelSO levelSO;
    public string sceneName;
    
    public void BossFightScene()
    {
        var scene_01 = SceneManager.LoadSceneAsync("PersistentScene");
        var scene_02 = SceneManager.LoadSceneAsync("Level1", LoadSceneMode.Additive);
        Time.timeScale = 1;
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
