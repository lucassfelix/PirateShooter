using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : ObjectPool
{
    public static BulletPool SharedInstance;
    private void Awake()
    {
        SharedInstance = this;
        Parent = transform;
    }
    
}
