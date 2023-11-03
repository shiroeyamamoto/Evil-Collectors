using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ActorBase : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rg;
    [SerializeField] private float forceJump = 10;
    [SerializeField] private Transform target;
    [SerializeField] private Transform weapon;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DoThrow();
        }
    }

    protected void DoThrow()
    {
        weapon.transform.DOMoveX(transform.position.x - 10, 0.5f).SetEase(Ease.InOutCubic).OnComplete(() =>
        {
            weapon.transform.DOMoveX(transform.position.x, 0.2f);
        });
    }

    protected void DoFly(){
        StartCoroutine(DoFLyAsync());
    }

    private IEnumerator DoFLyAsync(){
        rg.gravityScale = 1;
        currentPositionY = transform.position.y;
        detalJump = 10;
        rg.AddForce(new Vector2(0, 1) * forceJump);
        yield return new WaitUntil(() =>
        {
            return currentPositionY + detalJump < transform.position.y;
        });
        rg.gravityScale = 55;
        yield return new WaitUntil(() =>
        {
            return rg.velocity.y < 0f;
        });
        rg.gravityScale = 0;
        rg.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        rg.gravityScale = 1;
        rg.AddForce((target.position - transform.position).normalized * forceJump);
        yield return new WaitUntil(() =>
        {
            return IsGroundCheck();
        });
        rg.gravityScale = 1;
        rg.velocity = Vector2.zero;
    }
    
    

    protected void DoJump(){
        StartCoroutine(DoJumpAsync());
    }

    private float currentPositionY;
    private float detalJump;
    private IEnumerator DoJumpAsync(){
        rg.gravityScale = 1;
        currentPositionY = transform.position.y;
        detalJump = 10;
        rg.AddForce(new Vector2(0, 1) * forceJump);
        yield return new WaitUntil(() =>
        {
            return currentPositionY + detalJump < transform.position.y;
        });
        rg.gravityScale = 70;
        yield return new WaitUntil(() =>
        {
            return IsGroundCheck();
        });
        rg.gravityScale = 1;
    }

    private bool IsGroundCheck(){
        RaycastHit2D[] hits;
        var x = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, filter, 0, 1);
        Debug.DrawRay(transform.position, Vector2.down, Color.blue);
        if(x.collider != null)
        {
            Debug.Log(x.transform.gameObject.name);
            return true;
        }

        return false;
    }

    [SerializeField] private LayerMask filter;
}
