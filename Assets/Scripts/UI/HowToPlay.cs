using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlay : MonoBehaviour
{
    public GameObject howToPlayUI;

    private void Awake()
    {
        CloseUI();
    }

    public void OpenUI()
    {
        howToPlayUI.SetActive(true);
    }

    public void CloseUI()
    {
        howToPlayUI.SetActive(false);
    }
}
