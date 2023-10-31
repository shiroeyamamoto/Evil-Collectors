using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_force : MonoBehaviour
{
    Rigidbody2D rb2d;
    public float force;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(Vector2.one * force ,ForceMode2D.Impulse);
        //rb2d.velocity = Vector2.one * force;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
