using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class StatusBossController : MonoBehaviour
{
    public bool isEnterArea;
    public float fadeValueMax;
    public float fadeDuration;
    public float intensity;
    public void Start()
    {
        intensity = transform.GetComponent<Light2D>().intensity;
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

        if (intensity == fadeValueMax)
        {
            transform.GetComponentInChildren<MeshRenderer>().enabled = true;
        } else
        {
            transform.GetComponentInChildren<MeshRenderer>().enabled = false;
        }
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


}
