using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WindZoneController : MonoBehaviour
{
    public int index;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        LevelLoader ll = GameObject.FindAnyObjectByType<LevelLoader>();
        Debug.Log(ll);
        ll.StartCoroutine(ll.LoadScene(1, "MenuScene",""));
        //SceneManager.LoadScene("MenuScene");
    }
    
}
