using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyType
{

}
public class EnemyData 
{
    public int enemyId;
    public EnemyType enemyType;
    public string enemyName;
    public string enemyDescription;
    public HealthSystem healthSystem;
    public Item [] itemsDropOnDie;
}
