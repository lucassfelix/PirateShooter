using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonballBehaviour : MonoBehaviour
{
    private int _damage;
    private GameObject _origin;

    public int GetDamage()
    {
        return _damage;
    }
    public void SetDamage(int damage)
    {
        _damage = damage;
    }
    public void SetOriginGameobject(GameObject origin)
    {
        _origin = origin;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_origin.Equals(other.gameObject)) return;

        var playerManager = other.gameObject.GetComponent<PlayerManager>();

        if (playerManager != null)
        {
            playerManager.TakeDamage(_damage);
        }
        
        var enemyBehaviour = other.gameObject.GetComponents<EnemyBehaviour>();

        if (enemyBehaviour.Length != 0)
        {
            enemyBehaviour[0].TakeDamage(_damage);
        }
        
        var explosion = ExplosionPool.SharedInstance.GetPooledObject();
        explosion.GetComponent<ExplosionBehaviour>().Explode(transform.position,1);
        BulletPool.SharedInstance.ReturnPooledObject(gameObject);
    }
    
}
