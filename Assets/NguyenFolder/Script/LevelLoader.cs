using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public IEnumerator LoadScene(float loadTime,string sceneLoadString_Single , string scene_additive)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(loadTime);
        if(sceneLoadString_Single!="") SceneManager.LoadScene(sceneLoadString_Single);
        if(scene_additive!="") SceneManager.LoadScene(scene_additive, LoadSceneMode.Additive);
    }
}
