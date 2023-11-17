using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static List<PooledObjectInfo> ObjectPools = new List<PooledObjectInfo>();

    public static GameObject SpawnObject(GameObject objectToSpawn , Vector3 spawnPosition , Quaternion spawnRotation)
    {
        PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == objectToSpawn.name);

        
        if(pool == null)
        {
            // if pool does not exist , create it
            pool = new PooledObjectInfo() { LookupString = objectToSpawn.name };
            ObjectPools.Add(pool);
        }
        GameObject spawnableObject = pool.InactiveObjects.FirstOrDefault();
        if (spawnableObject == null)
        {
            // if no inactive object , create new one
            spawnableObject = Instantiate(objectToSpawn, spawnPosition, spawnRotation);
        } else
        {
            // if htere is an inactive object , reactive it
            spawnableObject.transform.position = spawnPosition;
            spawnableObject.transform.rotation = spawnRotation;
            pool.InactiveObjects.Remove(spawnableObject);
            spawnableObject.SetActive(true);

            GameObject parent = GameObject.Find("ObjectPoolManager");
            if (parent == null)
            {
                parent = new GameObject("ObjectPoolManager");
                parent.transform.parent = null;
            }
            spawnableObject.transform.parent = parent.transform;


        }
        return spawnableObject;
    }

    public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation,Transform spawnParent)
    {
        PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == objectToSpawn.name);


        if (pool == null)
        {
            // if pool does not exist , create it
            pool = new PooledObjectInfo() { LookupString = objectToSpawn.name };
            ObjectPools.Add(pool);
        }
        GameObject spawnableObject = pool.InactiveObjects.FirstOrDefault();
        if (spawnableObject == null)
        {
            // if no inactive object , create new one
            spawnableObject = Instantiate(objectToSpawn, spawnPosition, spawnRotation);

        }
        else
        {
            // if htere is an inactive object , reactive it
            spawnableObject.transform.position = spawnPosition;
            spawnableObject.transform.rotation = spawnRotation;

            
            pool.InactiveObjects.Remove(spawnableObject);
            spawnableObject.SetActive(true);

        }
        return spawnableObject;
    }

    public static void ReturnObjectToPool(GameObject obj)
    {
        string goName = obj.name.Substring(0, obj.name.Length - 7);// remove "(Clone) string from the name obj spawned object"
        PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == goName);
        if (pool == null)
        {
            Debug.Log("Not object named :" + obj.name);
        } else
        {
            obj.SetActive(false);
            pool.InactiveObjects.Add(obj);
        }
    }
}

public class PooledObjectInfo
{
    public string LookupString;
    public List<GameObject> InactiveObjects = new List<GameObject>();
}
