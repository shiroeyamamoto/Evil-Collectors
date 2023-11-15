﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class BossController : MonoBehaviour,IInteractObject
{
    [Header("Health")]
    public float health;
    public float healthPhase2;
    public float healthPhase3;
    [Space]
    public Color normalColor;
    public Color attackColor;
    public Color strongAttackColor;
    public bool onGround;
    public bool onWall;
    Rigidbody2D rb2d;

    [Min(1)]
    public float velocity;

    private void Start()
    {
        listSwords = new List<Transform>();
        phase = transform.GetComponent<Animator>().GetInteger("Phase");
        rb2d = GetComponent<Rigidbody2D>();
        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        //Physics2D.IgnoreCollision(transform.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
    }
    private void Update()
    {

    }
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask wallLayer;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            Player.Instance.TakeDamage(20f);
        }
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

    public void OnDamaged(float damage)
    {
        health -= damage;
        if(health <= healthPhase3)
        {
            PhaseUp();
        }
        else 
        if (health<= healthPhase2)
        {
            PhaseUp();
        } else
        if(health <= 0)
        {
            Debug.Log("im dead");
        }
    }

    [ContextMenu("Damage Me")]
    public void damageMe()
    {
        OnDamaged(15);
    }

    public List<Transform> listSwords;

}
