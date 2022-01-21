using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserPool : ObjectPool
{
    public static ChaserPool SharedInstance;
    private void Awake()
    {
        SharedInstance = this;
        Parent = transform;
    }
}
