using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class DamgableObjectController : MonoBehaviour
{
    Rigidbody2D rb2d;
    public float downSpeed;
    public float rotateDuration;
    // Start is called before the first frame update
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        transform.DORotate(new Vector3(0, 0, 360f), rotateDuration, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
    }
    private void Update()
    {
        rb2d.velocity = Vector2.down * downSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.DOKill();
        Destroy(gameObject);
    }
}
