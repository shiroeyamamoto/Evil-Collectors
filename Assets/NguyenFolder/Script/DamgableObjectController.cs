using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;
using static UnityEngine.RuleTile.TilingRuleOutput;

[RequireComponent(typeof(AudioSource))]
public class DamgableObjectController : MonoBehaviour
{
    Rigidbody2D rb2d;
    public float downSpeed;
    public float rotateDuration;
    public AudioSource source;
    // Start is called before the first frame update
    private void Awake()
    {
        source = GetComponent<AudioSource>();
        rb2d = GetComponent<Rigidbody2D>();
        transform.DORotate(new Vector3(0, 0, 360f), rotateDuration, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
    }
    private void Update()
    {
        rb2d.velocity = Vector2.down * downSpeed;
    }
    public AudioClip clip;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        transform.DOKill();
        Camera.main.GetComponent<CameraController>().ShakeCamera(0.5f, 0.1f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        source.clip = clip;
        source.volume = 0.3f;
        source.Play();
        source.volume = 1;
    }
}
