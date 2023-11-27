using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Boss_Level_1_Controller : MonoBehaviour,IInteractObject
{
    [Header("Health")]
    public float health = 100;
    public float healthPhase2 = 50;
    [Space]
    [Header("Phase")]
    public int currentPhase;
    public int phaseMax;
    public int maxAttackType;
    public int currentAttackType = 0;
    public int previousAttackType = 0;

    [Header("Animator")]
    Animator animator;
    public float duration;
    public float force;

    Transform damagableObject;
    private void Awake()
    {
        scaleDefault = transform.Find("Body").Find("Eyes").localScale.y;
        animator = GetComponent<Animator>();
        currentPhase = animator.GetInteger("Phase");
        maxAttackType = maxAttackTypeOfPhase(currentPhase);
        animator.GetBehaviour<Boss_Lv1_Attack>().maxAttackType = maxAttackType;
        
        //SpawnDamagableObject2();
    }

    

    void ShakeCamera()
    {
        Camera.main.GetComponent<CameraController>().ShakeCamera(duration, force);
    }

    void ShakeCameraStrong()
    {
        Camera.main.GetComponent<CameraController>().ShakeCamera(duration*2, force *2);
        animator.GetBehaviour<Boss_Lv1_Ground_Shake>().SpawnDamagableObject();
    }

    [ContextMenu("Phase Up")]
    public void PhaseUp()
    {
        if(currentPhase < phaseMax)
        {
            currentPhase++; animator.SetInteger("Phase", currentPhase);
            maxAttackType = maxAttackTypeOfPhase(currentPhase);
            animator.GetBehaviour<Boss_Lv1_Attack>().maxAttackType = maxAttackType;
        }
    }

    public int maxAttackTypeOfPhase(int currentPhase)
    {
        int attackTypeNumbs = 0;
        switch (currentPhase)
        {
            case 1:attackTypeNumbs = 2; break;
            case 2:attackTypeNumbs = 3; break;
                default:attackTypeNumbs = 2;break;

        }
        return attackTypeNumbs;
        

    }

    [Space]
    [Header("Spawn Weapon 2")]
    public Transform prefab;
    public int n;
    public Vector3 center;
    public float distanceToOther;
    public float distanceToBoss;
    
    public void SpawnDamagableObject2()
    {
        Vector3 startPosition = transform.Find("Weapon2Spawner").position;
        float offset = n % 2 == 1 ? 0f : distanceToOther / 2;
        int directionInt = (Player.Instance.transform.position.x < transform.position.x) ? -1 : 1;
        for (int i = 0; i < n; i++)
        {
            float x = transform.position.x + (distanceToBoss + offset + distanceToOther * i) * directionInt;

            //Debug.Log($"tranform boss = {transform.position.x} ,distanceToBoss = {distanceToBoss} ,offset = {offset}, distanceToOther = {distanceToOther}, i = {i} , x = {x}");

            Vector3 point = new Vector3(x, transform.position.y- transform.lossyScale.y, 0);
            GameObject o = Instantiate(prefab, startPosition, Quaternion.identity).gameObject;
            o.transform.DOJump(point, Random.Range(8, 15), 1, Random.Range(1f, 1.2f)).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(o);
            });
        }
    }

    [ContextMenu("OnDamaged")]
    public void Damage()
    {
        OnDamaged(10);
    }


    public float eyeScale;
    public float scaleDuration;

    // true sẽ cộng mana khi gây sát thương 
    public void OnDamaged(float damage, bool value)
    {
        OnDamaged(damage);

        if (!value)
            return;

            if (GameController.Instance.Player.CurrentInfo.mana < GameController.Instance.Player.InfoDefaultSO.mana)
        {
            GameController.Instance.Player.CurrentInfo.mana += damage;
            if (GameController.Instance.Player.CurrentInfo.mana > GameController.Instance.Player.InfoDefaultSO.mana)
                GameController.Instance.Player.CurrentInfo.mana = GameController.Instance.Player.InfoDefaultSO.mana;
            Player.Instance.OnUpdateMana?.Invoke(Player.Instance.CurrentInfo.mana);
        }
    }
    public float scaleDefault;
    public void OnDamaged(float damage)
    {
        //damage = 10;
        //damage animation
        Debug.Log(scaleDefault);

        transform.Find("Body").Find("Eyes").DOScaleY(eyeScale, scaleDuration / 2).SetEase(Ease.Linear).OnComplete(() =>
        {
            transform.Find("Body").Find("Eyes").DOScaleY(scaleDefault, scaleDuration / 2).SetEase(Ease.Linear);
        });
        //damage minus

        health -= damage;
        CheckHealth();
    }

    public void CheckHealth()
    {
        if(health <= 0)
        {
            health = 0;
            Debug.Log("I m D e a d");
            //this.enabled = false;
            animator.SetTrigger("Death");
            Player.Instance.OnDead?.Invoke(true);
            Player.Instance.OnDead(true);
        }
        else if(health <= healthPhase2)
        {
            Debug.Log("I m stronger");
            PhaseUp();
        }
    }

    public void PlayGroundParticle()
    {
        Transform groundSlamPrefab = animator.transform.Find("GroundSlam");
        if (groundSlamPrefab)
        {
            groundSlamPrefab.position = transform.Find("Weapon2Spawner").position;
            RaycastHit2D hit = Physics2D.Raycast(groundSlamPrefab.position, Vector2.down, Mathf.Infinity);
            if (hit) groundSlamPrefab.position = hit.point;
            groundSlamPrefab.GetComponent<ParticleSystem>().Play();
        }
    }

    public void OnDead()
    {
        // UI OnDead

        // Disable Animator
        TweenKill();
        transform.parent.gameObject.SetActive(false);
    }

    public void TweenKill()
    {
        var activeTween = DOTween.KillAll();
        Debug.Log(activeTween);
    }
}
