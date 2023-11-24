using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZoneController : MonoBehaviour
{
    public bool windZoneActive = false;
    public float pushForce;
    public void Update()
    {
        if (windZoneActive)
        {
            float distanceX = Mathf.Abs(Player.Instance.transform.position.x- transform.position.x);
            
            Player.Instance.GetComponent<Rigidbody2D>().velocity = new Vector2(Player.Instance.GetComponent<Rigidbody2D>().velocity.x - pushForce, Player.Instance.GetComponent<Rigidbody2D>().velocity.y);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        windZoneActive = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        windZoneActive = false;
    }
}
