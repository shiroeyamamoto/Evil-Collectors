using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingletonMonobehavious<PlayerManager>
{
    public DashCollison DashCollison;
    public ParticleSystem bloodParticle;
    public GameObject canvasDamageShow;
    public ParticleSystem attackStrongParticle;

    private void Start()
    {
        canvasDamageShow.SetActive(false);
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
