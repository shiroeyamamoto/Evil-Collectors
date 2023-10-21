using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] float jumpHeight;
    Rigidbody2D rb2d;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    [ContextMenu("JumpToTarget")]
    public void JumpToTarget()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player) {
            float distanceX = player.transform.position.x - transform.position.x;
            float middlePointX = distanceX / 2;
            float jumpHeightY = transform.position.y + jumpHeight;

            Vector2 jumpVector = new Vector2(middlePointX, jumpHeightY);
            rb2d.velocity = jumpVector;
            Debug.Log($"found {player.name}");
            

            Debug.Log($"Found {player.name}");
        }
        
    }

    public float jumpForce = 5f;
    public Transform player;
    //private Rigidbody2D rb2d;


    private void Update()
    {
        
    }
}
