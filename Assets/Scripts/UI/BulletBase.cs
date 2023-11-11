using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour {
    public virtual void ActiveToDir(Vector2 dir){}
    public virtual void ActiveToTarget(Vector2 target){}
}
