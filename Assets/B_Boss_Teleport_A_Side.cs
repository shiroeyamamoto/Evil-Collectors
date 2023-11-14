
using UnityEngine;
using DG.Tweening;
using UnityEditor.Timeline.Actions;
using Unity.VisualScripting;

public class B_Boss_Teleport_A_Side : StateMachineBehaviour
{
    [Header("Choose Teleport Position")]
    [HideInInspector] public int directionInt;
    public float wallOffset;
    public LayerMask wallLayer;
    //public ParticleSystem particleSystem;
    //public Transform particlePrefab;

    public float distanceToPlayer;
    public LayerMask playerLayer;
    public Coroutines coroutines;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //particleSystem = animator.transform.parent.GetComponentInChildren<ParticleSystem>();
        /*Transform particlePrefab = Instantiate(this.particlePrefab,animator.transform.position,Quaternion.identity);
        particleSystem = particlePrefab.GetComponent<ParticleSystem>();
        if (particleSystem)
        {
            //particleSystem.Play();
        }*/
        
        //float totalDuration = particleSystem.main.duration;
       // Debug.Log(totalDuration);
        //Destroy(particleSystem.gameObject, totalDuration);

        // Get coroutines
        coroutines = animator.GetComponent<Coroutines>();
        
        /*coroutines.WaitForSeconds(totalDuration, () =>
        {
            Teleport(animator);
        });*/
        animator.transform.GetComponent<SpriteRenderer>().DOFade(0, 0.25f)
        .OnStart(() =>
        {
            animator.transform.GetComponent<Collider2D>().isTrigger = true;
            animator.transform.GetComponent<Rigidbody2D>().gravityScale = 0;
        })
        .OnComplete(() =>
        {
            Teleport(animator);
            animator.transform.GetComponent<SpriteRenderer>().DOFade(1, 0.25f).OnComplete(() =>
            {
                animator.transform.GetComponent<Collider2D>().isTrigger = false;
                animator.transform.GetComponent<Rigidbody2D>().gravityScale = 0;
            });
        });
        //Teleport(animator);
    }

    void Teleport(Animator animator)
    {
        

        Label:
        {
            directionInt = ChooseSide(directionInt);
        }
        int directionShoot = directionInt == -1 ? 1 : -1;
        RaycastHit2D hit = GetHit(animator.transform.position, directionInt);
        Vector3 endPoint;
        endPoint.x = hit.point.x - directionInt * wallOffset;
        endPoint.y = hit.point.y;
        endPoint.z = 0;
        if (!HavePlayerOnRadius(endPoint, distanceToPlayer))
        {
            animator.transform.localScale = new Vector3(Mathf.Abs(animator.transform.lossyScale.x) * directionShoot, animator.transform.lossyScale.y, animator.transform.lossyScale.z);
            animator.transform.position = endPoint;
        }
        else
        {
            goto Label;
        }
    }
    public bool HavePlayerOnRadius(Vector3 position,float radius)
    {
        bool findPlayer = Physics2D.OverlapCircle(position, radius, playerLayer);
        return findPlayer;
    }
    public ParticleSystem GetParticleSystem(Transform particleSystem,Animator animator)
    {
        particleSystem = animator.transform.parent.Find("Particles").Find("TeleportParticle");
        if(particleSystem)
        {
            return particleSystem.GetComponent<ParticleSystem>();
        } else
        {
            return null;
        }
    }
    public int ChooseSide(int side)
    {
        side = Random.Range(-1, 2);
        if (side == 0)
        {
            side = ChooseSide(side);
        }
        return side;
    }
    public int GetDirectionInt(Animator animator)
    {
        int directionInt = (Player.Instance.transform.position.x - animator.transform.position.x) < 0 ? -1:1;
        return directionInt;
    } 
    public RaycastHit2D GetHit(Vector3 position , int directionInt)
    {
        return Physics2D.Raycast(position, Vector2.right * directionInt, Mathf.Infinity, wallLayer);
    }

    public void WaitForSecond(float time)
    {
        while(time> 0)
        {
            time -= Time.deltaTime;
        }
        Debug.Log("1");
    }
}
