using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.AxisState;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private EnemyBody enemyBody;

    // Movement
    [SerializeField, Range(0f, 100f)] private float speedMove = 10f;
    [SerializeField, Range(0f, 100f)] private float speedAirMove = 20f;

    private Transform bodyTransform;


    float move;
    private float Horizontal;

    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = gameObject.GetComponentInParent<Rigidbody2D>();
        bodyTransform = transform.parent.transform;

    }

    private void Update()
    {
        Flip();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EnemyMovement();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EnemyMovement();
        }
    }

    private void EnemyMovement()
    {
        enemyBody.isGrounded = Physics2D.OverlapCircle(enemyBody.groundCheck.position, 0.1f, LayerMask.GetMask(Settings.groundLayerMask));

        if (Player.Instance.transform.position.x > bodyTransform.position.x)
            move = 1f;
        else
            move = -1f;

        rb2d.velocity = new Vector2(move * (enemyBody.isGrounded ? speedMove : speedAirMove), rb2d.velocity.y);

        Horizontal = move;
    }

    /// <summary>
    /// Lật mặt 
    /// </summary>
    private void Flip()
    {
        if (enemyBody.isFacingRight && Horizontal < 0f || !enemyBody.isFacingRight && Horizontal > 0f)
        {
            Vector2 localScale = bodyTransform.localScale;
            enemyBody.isFacingRight = !enemyBody.isFacingRight;
            localScale.x *= -1;
            bodyTransform.localScale = localScale;
        }
    }

}
