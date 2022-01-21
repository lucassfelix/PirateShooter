using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterBehaviour : EnemyBehaviour
{
    [Space]
    [Header("Fire Settings")]
    [SerializeField] private Transform bulletOrigin;
    [SerializeField] private float distanceToShoot;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int bulletDamage;
    [SerializeField] private float fireInterval;

    private bool _canShoot = true;
    
    protected override void FixedEnemyMovement()
    {
        if (Vector3.Distance(transform.position, playerReference.transform.position) <= distanceToShoot)
        {
            EnemyRigidbody2D.AddTorque(RotationDir * rotationSpeed,ForceMode2D.Force);
            return;
        }
        base.FixedEnemyMovement();
    }

    protected override void EnemyAttack()
    {
        if (Vector3.Distance(transform.position, playerReference.transform.position) > distanceToShoot || !_canShoot)
        {
            return;
        }
        var newBullet = BulletPool.SharedInstance.GetPooledObject();
        if (newBullet != null)
        {
            newBullet.transform.position = bulletOrigin.position;
            newBullet.GetComponent<Rigidbody2D>().velocity = ForwardDir * bulletSpeed;
            var cannonballBehaviour = newBullet.GetComponent<CanonballBehaviour>();
            cannonballBehaviour.SetOriginGameobject(this.gameObject);
            cannonballBehaviour.SetDamage(bulletDamage);
            _canShoot = false;
            Invoke(nameof(RechargeCanon),fireInterval);
        }
        base.EnemyAttack();
    }
    
    void RechargeCanon()
    {
        _canShoot = true;
    }
}
