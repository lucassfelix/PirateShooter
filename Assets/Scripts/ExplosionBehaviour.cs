using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{

    private Animator _explosionAnimator;

    public void Explode(Vector3 position, int scale)
    {
        transform.localScale = Vector3.one * scale;
        _explosionAnimator = GetComponent<Animator>();
        transform.position = position;
        _explosionAnimator.Play("explosion");
    }

    public void Deactivate()
    {
        ExplosionPool.SharedInstance.ReturnPooledObject(this.gameObject);
    }
}
