using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class BossController : MonoBehaviour
{
    public bool onGround;
    public bool onWall;
    Rigidbody2D rb2d;

    [Min(1)]
    public float velocity;

    private void Start()
    {
        phase = transform.GetComponent<Animator>().GetInteger("Phase");
        rb2d = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(transform.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
    }
    private void Update()
    {

    }
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask wallLayer;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        int layer = collision.gameObject.layer;
        if(groundLayer == ( 1<< layer))
        {
            onGround = true;
        }
        if (wallLayer == (1 << layer))
        {
            onWall = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        int layer = collision.gameObject.layer;
        if (groundLayer == (1 << layer))
        {
            onGround = false ;
        }
        if (wallLayer == (1 << layer))
        {
            onWall = false ;
        }
    }

    [SerializeField] int phase;
    [ContextMenu("Phase Up")]
    void PhaseUp()
    {
        
        phase++;
        transform.GetComponent<Animator>().SetInteger("Phase",phase);
    }
}
