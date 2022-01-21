using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterPool : ObjectPool
{
    public static ShooterPool SharedInstance;
    private void Awake()
    {
        SharedInstance = this;
        Parent = transform;
    }
}
