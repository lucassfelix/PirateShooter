using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserBehaviour : EnemyBehaviour
{
    [SerializeField] private int collisionDamage;
    protected void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerManager>().TakeDamage(collisionDamage);
            Explode();
        }
    }

    void Explode()
    {
        MovementEnabled = false;   
        hullSpriteRenderer.sprite = destroyedHullSprite;
        sailSpriteRenderer.sprite = destroyedSailSprite;
        
        var explosionOne = ExplosionPool.SharedInstance.GetPooledObject();
        explosionOne.SetActive(false);
        
        StartCoroutine(PlayExplosion(explosionOne, Vector3.zero, 4, 0f));
        StartCoroutine(SinkShip(0.31f));
    }
    
    
}
