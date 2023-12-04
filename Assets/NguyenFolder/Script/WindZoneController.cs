using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.AxisState;

public class WindZoneController : MonoBehaviour
{
    public bool windZoneActive = false;
    public float pushForce;

    public SO_PlayerData playerData;
    [SerializeField] private float distanceToPlayer;
    [SerializeField] private float transformSizeX;
    private float speedMove;
    private float speedAirMove;
    private void Awake()
    {
        transformSizeX = transform.localScale.x / 2;
        speedMove = Player.Instance.GetComponent<Move>().speedMove;
        speedAirMove = Player.Instance.GetComponent<Move>().speedAirMove;
    }
    public float ratio;
    public void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");
        distanceToPlayer = transform.position.x - Player.Instance.transform.position.x;
        if (Mathf.Abs(distanceToPlayer) <= transformSizeX)
        {
            ratio = 1 - ( transformSizeX - Mathf.Abs(distanceToPlayer)) / transformSizeX;
        }
        else
        {
            ratio = 1;
            Debug.Log("exit windzone");
        }
        speedMove = Player.Instance.GetComponent<Move>().ratio = ratio;
        //Player.Instance.rb2d.velocity = new Vector2(move * (Settings.isGrounded ? speedMove : speedAirMove) * ratio, Player.Instance.rb2d.velocity.y);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        windZoneActive = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        windZoneActive = false;
    }
}
