using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class EnemyBehaviour : MonoBehaviour
{

    [Space] [Header("References")] [SerializeField]
    protected GameObject playerReference;
    
    [Space]
    [Header("Movement Settings")]
    [SerializeField] protected float enemySpeed;
    [SerializeField] protected float rotationSpeed;

    [Space]
    [Header("Health Settings")]
    [SerializeField] protected int enemyMaxHealth;

    [Space] [Header("Sprite Settings")] [Header("Hull")] 
    [SerializeField] protected Sprite hullSprite;
    [SerializeField] protected Sprite mediumDamageHullSprite;
    [SerializeField] protected Sprite heavyDamageHullSprite;
    [SerializeField] protected Sprite destroyedHullSprite;
    [Header("Flag")]
    [SerializeField] protected Sprite sailSprite;
    [SerializeField] protected Sprite mediumDamageSailSprite;
    [SerializeField] protected Sprite heavyDamageSailSprite;
    [SerializeField] protected Sprite destroyedSailSprite;
    [Header("Renderers")]
    [SerializeField] protected SpriteRenderer hullSpriteRenderer;
    [SerializeField] protected SpriteRenderer sailSpriteRenderer;

    protected int CurrentEnemyHealth;
    protected Rigidbody2D EnemyRigidbody2D;
    protected float RotationDir;
    protected Vector2 ForwardDir;
    protected bool MovementEnabled;
    protected ObjectPool EnemyTypePoolReference;
    protected ScoreManager ScoreManagerReference;

    protected IEnumerator PlayExplosion(GameObject explosion, Vector3 offset, int scale, float time)
    {
        yield return new WaitForSeconds(time);
        explosion.SetActive(true);
        explosion.GetComponent<ExplosionBehaviour>().Explode(transform.position + offset,scale);
    }

    protected IEnumerator SinkShip(float time)
    {
        yield return new WaitForSeconds(time);
        EnemyTypePoolReference.ReturnPooledObject(gameObject);
    }

    
    
    void Defeat()
    {
        MovementEnabled = false;   
        var explosionOne = ExplosionPool.SharedInstance.GetPooledObject();
        var explosionTwo = ExplosionPool.SharedInstance.GetPooledObject();
        var explosionThree = ExplosionPool.SharedInstance.GetPooledObject();
        
        explosionOne.SetActive(false);
        explosionTwo.SetActive(false);
        explosionThree.SetActive(false);
        
        ScoreManagerReference.AddScore();

        StartCoroutine(PlayExplosion(explosionOne, Vector3.up/10, 2, 0.3f));
        StartCoroutine(PlayExplosion(explosionTwo, Vector3.left/10, 1, 0.6f));
        StartCoroutine(PlayExplosion(explosionThree, Vector3.down/10, 2, 0.9f));
        StartCoroutine(SinkShip(3));

    }
    public void SetPlayerReference(GameObject player)
    {
        playerReference = player;
    }

    public void SetScoreManagerReference(ScoreManager scoreManager)
    {
        ScoreManagerReference = scoreManager;
    }

    public void SetPoolReference(ObjectPool pool)
    {
        EnemyTypePoolReference = pool;
    }
    public void Init()
    {
        hullSpriteRenderer.sprite = hullSprite;
        sailSpriteRenderer.sprite = sailSprite;
        CurrentEnemyHealth = enemyMaxHealth;
        MovementEnabled = true;
    }
    void Start()
    {
        Init();
        EnemyRigidbody2D = GetComponent<Rigidbody2D>();
    }

    protected virtual void EnemyMovement()
    {
        var enemyRotationDegree = EnemyRigidbody2D.rotation - 90f;
        var forwardX = Mathf.Cos(enemyRotationDegree * Mathf.Deg2Rad);
        var forwardY = Mathf.Sin(enemyRotationDegree * Mathf.Deg2Rad);
        ForwardDir = new Vector2(forwardX, forwardY);
        var playerPos = playerReference.transform.position;
        var towardPlayer = playerPos - transform.position;
        
        var angle = Vector2.SignedAngle(ForwardDir, towardPlayer.normalized);
        RotationDir = Mathf.Sign(angle);
    }

    protected virtual void FixedEnemyMovement()
    {
        EnemyRigidbody2D.AddTorque(RotationDir * rotationSpeed,ForceMode2D.Force);
        EnemyRigidbody2D.AddForce(ForwardDir * enemySpeed);
    }
    protected virtual void EnemyAttack(){}
    protected virtual void FixedAttack(){}

    public void TakeDamage(int damageTaken)
    {
        CurrentEnemyHealth -= damageTaken;
        if (CurrentEnemyHealth <= 0)
        {
            hullSpriteRenderer.sprite = destroyedHullSprite;
            sailSpriteRenderer.sprite = destroyedSailSprite;
            if (MovementEnabled)
            {
                Defeat();
            }
        }
        else if (CurrentEnemyHealth == 1)
        {
            hullSpriteRenderer.sprite = heavyDamageHullSprite;
            sailSpriteRenderer.sprite = heavyDamageSailSprite;
        }
        else if (CurrentEnemyHealth <= enemyMaxHealth / 2)
        {
            hullSpriteRenderer.sprite = mediumDamageHullSprite;
            sailSpriteRenderer.sprite = mediumDamageSailSprite;
        }
    }

    void FixedUpdate()
    {
        if (!MovementEnabled) return;
        FixedEnemyMovement();
        FixedAttack();
    }

    void Update()
    {
        if (!MovementEnabled) return;
        EnemyMovement();
        EnemyAttack();
    }
}
