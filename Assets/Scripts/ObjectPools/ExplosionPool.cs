using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPool : ObjectPool
{
    public static ExplosionPool SharedInstance;
    private void Awake()
    {
        SharedInstance = this;
        Parent = transform;
    }
}
