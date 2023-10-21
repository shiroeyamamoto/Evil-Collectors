using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public bool onGround;
    public bool onWall;
    private void FixedUpdate()
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
}
