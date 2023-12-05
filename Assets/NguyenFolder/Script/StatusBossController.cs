using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class StatusBossController : MonoBehaviour
{
    [Space]
    public Material defaultMaterial;
    public Material glowMaterial;
    [Space]
    public bool isEnterArea;
    [Space]
    public float fadeValueMax;
    public float fadeDuration;
    public float intensity;
    [Space]
    public bool PanelIsOpened;
    [Space]
    public bool statusUnlocked;
    public void Start()
    {
        intensity = transform.GetComponentInChildren<Light2D>().intensity;
        statusUnlocked = transform.GetComponentInChildren<UnlockStateController>().isUnlocked;
    }
    private void Update()
    {
        float fadeDurationStep = fadeValueMax / fadeDuration;
        if (isEnterArea && intensity <= fadeValueMax)
        {
            intensity += fadeDurationStep * Time.deltaTime;
        }
        else if (!isEnterArea && intensity >= 0)
        {
            intensity -= fadeDurationStep * Time.deltaTime;
        }
        intensity = Mathf.Clamp(intensity, 0, fadeValueMax);

        transform.GetComponentInChildren<Light2D>().intensity = intensity;
        transform.GetComponentInChildren<MeshRenderer>().enabled = (intensity == fadeValueMax) ? true : false;
        AnimationBody();
        if (Input.GetKeyDown(KeyCode.W) && transform.GetComponentInChildren<MeshRenderer>().enabled && Settings.isGrounded && !PanelIsOpened)
        {
            PanelIsOpened = true;
            Player.Instance.GetComponent<Move>().enabled = false;
            Player.Instance.transform.DOMoveX(transform.position.x - Mathf.Abs(transform.lossyScale.x) , 1).SetEase(Ease.Linear).OnStart(() =>
            {
                FlipX((Player.Instance.transform.position.x <= transform.position.x - Mathf.Abs(transform.lossyScale.x) ? true : false));
            }).OnComplete(() =>
            {
                FlipX((Player.Instance.transform.position.x <= transform.position.x ? true : false), statusUnlocked? OpenPanel: OpenLockedPanel);
                //OpenPanel();
            });
        }
    }

    private void AnimationBody()
    {
        // change material
        transform.Find("Body").Find("Eyes").GetComponent<SpriteRenderer>().material = !statusUnlocked ? defaultMaterial : glowMaterial;
        transform.Find("Body").Find("Eyebrow").GetComponent<SpriteRenderer>().material = !statusUnlocked ? defaultMaterial : glowMaterial;

        // look at player
        bool isLeft = (Player.Instance.transform.position.x - transform.Find("Body").position.x)<0 ? true:false;
        if(statusUnlocked) transform.Find("Body").localScale = new Vector3(MathF.Abs(transform.Find("Body").localScale.x) * (isLeft ? 1 : -1), transform.Find("Body").localScale.y);
    }

    public void FlipX(bool flip)
    {
        Player.Instance.transform.DOScaleX(flip ? 1 : -1, 0);
    }

    public void FlipX(bool flip,Action action)
    {
        Player.Instance.transform.DOScaleX(flip ? 1 : -1, 0).OnComplete(() =>
        {
            Player.Instance.transform.DOScaleX(flip ? 1 : -1, 0.1f).OnComplete(() =>
            {
                action();
            });
        });
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == Player.Instance.transform.Find("Body").tag)
        {
            isEnterArea = true;
            Debug.Log("Enter Area");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == Player.Instance.transform.Find("Body").tag)
        {
            isEnterArea = false;
            Debug.Log("Enter Area");
        }
    }
    public void OpenLockedPanel()
    {
        Player.Instance.GetComponent<Move>().enabled = false;
        transform.Find("Canvas")?.GetChild(1).gameObject.SetActive(true);
        transform.Find("Canvas")?.GetChild(1).GetComponent<RectTransform>().DOScale(0, 0);
        // disable all child
        foreach (Transform child in transform.Find("Canvas")?.GetChild(1))
        {
            child.gameObject.SetActive(false);
        }
        //
        transform.Find("Canvas")?.GetChild(1).GetComponent<RectTransform>().DOScale(1, openTime).OnComplete(() =>
        {
            foreach (Transform child in transform.Find("Canvas")?.GetChild(1))
            {
                child.gameObject.SetActive(true);
                Settings.isFacingRight = true;
                //Time.timeScale = 0;
            }
        });
    }
    public void OpenPanel()
    {
        transform.Find("Canvas")?.GetChild(0).gameObject.SetActive(true);
        transform.Find("Canvas")?.GetChild(0).GetComponent<RectTransform>().DOScale(0, 0);
        // disable all child
        foreach(Transform child in transform.Find("Canvas")?.GetChild(0))
        {
            child.gameObject.SetActive(false);
        }
        //
        transform.Find("Canvas")?.GetChild(0).GetComponent<RectTransform>().DOScale(1, openTime).OnComplete(() =>
        {
            foreach (Transform child in transform.Find("Canvas")?.GetChild(0))
            {
                child.gameObject.SetActive(true);
                Settings.isFacingRight = true;
                //Time.timeScale = 0;
            }
        });


    }

    public float openTime;
    public float closeTime;
    public void ClosePanel()
    {
        Time.timeScale = 1;
        // close index 0
        foreach (Transform child in transform.Find("Canvas")?.GetChild(0))
        {
            child.gameObject.SetActive(false);
        }
        transform.Find("Canvas")?.GetChild(0).GetComponent<RectTransform>().DOScale(0, openTime).OnComplete(() =>
        {
            transform.Find("Canvas")?.GetChild(0).gameObject.SetActive(false);
            Player.Instance.GetComponent<Move>().enabled = true;
            PanelIsOpened = false;
        });

        //close index 1
        foreach (Transform child in transform.Find("Canvas")?.GetChild(1))
        {
            child.gameObject.SetActive(false);
        }
        transform.Find("Canvas")?.GetChild(1).GetComponent<RectTransform>().DOScale(0, openTime).OnComplete(() =>
        {
            transform.Find("Canvas")?.GetChild(1).gameObject.SetActive(false);
            Player.Instance.GetComponent<Move>().enabled = true;
            PanelIsOpened = false;
        });
    }
}
