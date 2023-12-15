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
        transform.GetComponent<EchoEffect>().enabled = false;
        damagedSound = gameObject.GetComponent<AudioSource>();
        //SpawnDamagableObject2();
    }

    public void PlaySound(AudioClip clip)
    {
        SoundManager.PlaySound(clip);
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

        transform.GetComponent<EchoEffect>().enabled = true;
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
    public AudioClip damagableObject2Sound;

    public void SpawnDamagableObject2()
    {
        Vector3 startPosition = transform.Find("Weapon2Spawner").position;
        float offset = n % 2 == 1 ? 0f : distanceToOther / 2;
        int directionInt = (Player.Instance.transform.position.x < transform.position.x) ? -1 : 1;
        for (int i = 0; i < n; i++)
        {
            float x = transform.position.x + (distanceToBoss + offset + distanceToOther * i) * directionInt;
            float y = transform.position.y - Mathf.Abs(transform.lossyScale.y)/2;
            Debug.Log("y"+y);
            //Debug.Log($"tranform boss = {transform.position.x} ,distanceToBoss = {distanceToBoss} ,offset = {offset}, distanceToOther = {distanceToOther}, i = {i} , x = {x}");

            Vector3 point = new Vector3(x, y, 0);
            GameObject o = Instantiate(prefab, startPosition, Quaternion.identity).gameObject;
            o.transform.DOJump(point, Random.Range(8, 15), 1, Random.Range(1f, 1.2f)).SetEase(Ease.Linear).OnComplete(() =>
            {
                PlaySound(damagableObject2Sound);
                o.transform.DOKill();
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

        Player.Instance.UseMana(-damage);
            /*if (GameController.Instance.Player.CurrentInfo.mana < GameController.Instance.Player.InfoDefaultSO.mana)
        {
            GameController.Instance.Player.CurrentInfo.mana += damage;
            if (GameController.Instance.Player.CurrentInfo.mana > GameController.Instance.Player.InfoDefaultSO.mana)
                GameController.Instance.Player.CurrentInfo.mana = GameController.Instance.Player.InfoDefaultSO.mana;
            Player.Instance.OnUpdateMana?.Invoke(Player.Instance.CurrentInfo.mana);
        }*/
    }
    public float scaleDefault;
    public AudioSource damagedSound;
    public void OnDamaged(float damage)
    {
        
        Player.Instance.ShowDamage(damage, gameObject);
        damagedSound.clip = Player.Instance.gameObject.GetComponent<PlayerSound>().PlayerAttackBoss;
        damagedSound.volume = Settings.sound-0.5f;
        damagedSound.Play();

        //damage = 10;
        //damage animation
        Debug.Log(scaleDefault);

        transform.Find("Body").Find("Eyes").DOScaleY(eyeScale, scaleDuration / 2).SetEase(Ease.Linear).OnComplete(() =>
        {
            transform.Find("Body").Find("Eyes").DOScaleY(scaleDefault, scaleDuration / 2).SetEase(Ease.Linear).OnComplete(() =>
            {
                //transform.DOKill();
            });
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

            // ro code
            
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

    public BossStatus_SO bsso_current;
    public BossStatus_SO bsso_unlock;

    public void OnDead()
    {
        // UI OnDead

        // Disable Animator
        //TweenKill();
        bsso_current.defeated = true;
        Debug.Log(bsso_current.defeated);
        bsso_unlock.unlocked = true;
        transform.parent.gameObject.SetActive(false);
        Player.Instance.OnDead?.Invoke(true);
        Player.Instance.OnDead(true);
    }

    public void TweenKill()
    {
        var activeTween = DOTween.KillAll();// transform.DOKill();
        //var tweenChild = transform.GetComponent<Animator>().DOKill();
        //Debug.Log(activeTween);
    }
}
