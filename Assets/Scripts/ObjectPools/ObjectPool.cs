using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using UnityEngine;

public abstract class ObjectPool : MonoBehaviour
{
    protected Queue<GameObject> PooledObjects;
    [SerializeField] protected GameObject objectToPool;
    [SerializeField] protected int amountToPool;
    protected Transform Parent;



    private void Start()
    {
        PooledObjects = new Queue<GameObject>();
        GameObject aux;
        for (int i = 0; i < amountToPool; i++)
        {
            aux = Instantiate(objectToPool,Parent);
            aux.SetActive(false);
            PooledObjects.Enqueue(aux);
        }
    }

    public GameObject GetPooledObject()
    {
        if (PooledObjects.Count == 0) return null;
        var obj = PooledObjects.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    public void ReturnPooledObject(GameObject obj)
    {
        obj.SetActive(false);
        PooledObjects.Enqueue(obj);
    }
    
    
}
