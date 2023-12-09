using System.Collections;
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
    public Color takeDamgeColor;
    [Space]
    public bool onGround;
    public bool onWall;
    Rigidbody2D rb2d;

    [Min(1)]
    public float velocity;

    private void Start()
    {
        animator = GetComponent<Animator>();
        listSwords = new List<Transform>();
        phase = transform.GetComponent<Animator>().GetInteger("Phase");
        rb2d = GetComponent<Rigidbody2D>();
        
    }
    private void Update()
    {

    }
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask wallLayer;
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (Settings.nothingnessSkill || Settings.concentrateSKill)
        {
            Debug.Log("Dang bất tử ");
            return;
        }
        if (collision.transform.CompareTag("Player"))
        {
            Player.Instance.OnDamaged(20f);
        }
        int layer = collision.gameObject.layer;
        if(groundLayer == ( 1 << layer))
        {
            onGround = true;
        }
        if (wallLayer == ( 1 << layer))
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

    [SerializeField]public int phase;
    [ContextMenu("Phase Up")]
    void PhaseUp()
    {
        
        phase++;
        phase = Mathf.Clamp(phase,1, 3);
        transform.GetComponent<Animator>().SetInteger("Phase",phase);
    }

    // true sẽ cộng mana khi gây sát thương 
    public void OnDamaged(float damage, bool value)
    {
        OnDamaged(damage);
        //Player.Instance.ShowDamage(damage, gameObject);
        if (!value)
            return;

        Player.Instance.UseMana(-damage);
    }
    public void OnDamaged(float damage)
    {
        Player.Instance.ShowDamage(damage, gameObject);
        health -= damage;
        DoEffect();
        if (health <= 0)
        {
            OnDead();
            
            Debug.Log("im dead");
        }
        if (health <= healthPhase2 && phase == 1)
        {
            PhaseUp();
        }
        if (health <= healthPhase3 && phase == 2)
        {
            PhaseUp();
        }
        
    }

    [ContextMenu("Damage Me")]
    public void damageMe()
    {
        OnDamaged(15);
        DoEffect();
    }
    Animator animator;
    public void OnDead()
    {
        
        //Disable animator
        if (animator)
        {
            animator.SetTrigger("Dead");
        }
    }

    public List<Transform> listSwords;

    [ContextMenu("Damage Effect")]
    void DoEffect()
    {
        TakeDamageEffect takeDamageEffect = transform.GetComponent<TakeDamageEffect>();
        if (takeDamageEffect != null)
        {
            Color oldColor = transform.Find("Body").GetComponent<SpriteRenderer>().color;
            takeDamageEffect.DoEffect(TakeDamageEffectEnum.ChangeColor, 0.2f, oldColor, takeDamgeColor);
        }
    }
}
