
using UnityEditor.SearchService;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class ChallengePanelController : MonoBehaviour
{
    public string sceneName;
    private void OnEnable()
    {
       
    }
    private void OnDisable()
    {
        
    }
    public void BossFightScene()
    {
        if (sceneName != null)
        SceneManager.LoadScene(sceneName);
        Debug.Log("I'm ChallengeBoss");
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
