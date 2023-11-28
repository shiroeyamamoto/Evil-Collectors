using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractObject 
{ 
    void OnDamaged(float damage);

    void OnDamaged(float damage, bool value);
}
