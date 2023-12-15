using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : SingletonMonobehavious<MusicManager>
{
    public AudioSource source;
    public float duration;
    public float targetVolumn;

    // Start is called before the first frame update
    void Start()
    {
        LevelManager.Instance.Init();
        //source = GetComponent<AudioSource>();
        source.clip = LevelManager.Instance.bossSound;
        source.volume = 0.0f;
        source.Play();
        StartCoroutine(AudioFade(true, source, duration, targetVolumn));
        StartCoroutine(AudioFade(false, source, duration, targetVolumn));
    }

    public void StopMusic()
    {
        Debug.Log("Co tat nhac k");
        source.Stop();
    }

    IEnumerator AudioFade(bool fadeIn ,AudioSource source, float duration ,float targetVolumn)
    {
        if (!fadeIn)
        {
            double lengthObSource = (double)source.clip.samples / source.clip.frequency;
            yield return new WaitForSecondsRealtime((float)(lengthObSource - duration));

        }

        float time = 0f;
        float startVol = source.volume;
        while (time < duration)
        {
            time += Time.deltaTime;
            source.volume = Mathf.Lerp(startVol, targetVolumn, time / duration);
            yield return null;
        }

        yield break;
    }
}
