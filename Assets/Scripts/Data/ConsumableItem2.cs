using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItem2 : ItemBase
{
    public float duration;
    //    tags
    public float value;
    public virtual void start(){}
    public virtual void update(){}
    public virtual bool IsCanUse(Player player){ return false; }
    public virtual void end(){}
    public virtual void AddBuffOrDebuff(){}
}
