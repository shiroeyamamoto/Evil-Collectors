using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]public static AudioSource source;
    [SerializeField]AudioClip[] soundBossSOs;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    private void Reset()
    {
        soundBossSOs = new AudioClip[100];
        soundBossSOs = Resources.LoadAll<AudioClip>("Sound Effect Collector");
        Debug.Log(soundBossSOs.Length);
    }
    public static void PlaySound(AudioClip audio)
    {
        source.clip = audio;
        source.Play();

    }
}
