using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SingletonMonobehavious<Player>
{
    private Rigidbody2D rigidbody2D;

    private void Awake()
    {
        base.Awake();

        rigidbody2D = this.GetComponent<Rigidbody2D>();
    }
}
