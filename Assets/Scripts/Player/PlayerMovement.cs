using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Space]
    [Header("Rotation Settings")]
    [SerializeField] [Range(1, 10)] private float rotateIntensity;
    [SerializeField] private float maxAngularVelocity;
    [Space]
    [Header("Movement Settings")]
    [SerializeField] [Range(1, 10)] private float forwardSpeed;
    [Space]
    [Header("Attack Settings")]
    [Tooltip("Value in seconds.")]
    [SerializeField] private float singleFireInterval;
    [SerializeField] private float specialFireInterval;

    [SerializeField] private int attackDamage;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform forwardBulletOrigin;
    [SerializeField] private List<Transform> leftBulletOrigin;
    [SerializeField] private List<Transform> rightBulletOrigin;

    
    private float _rotateValue;
    private float _forwardValue;
    private bool _canShoot = true;
    private bool _canMove = true;
    private Vector2 _forwardDir;
    private Rigidbody2D _playerRigidbody;
    private PlayerManager _playerManager;
    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _playerManager = GetComponent<PlayerManager>();
    }

    private void FixedUpdate()
    {
        var playerRotationDegree = _playerRigidbody.rotation - 90f;
        var forwardX = Mathf.Cos(playerRotationDegree * Mathf.Deg2Rad);
        var forwardY = Mathf.Sin(playerRotationDegree * Mathf.Deg2Rad);
        _forwardDir = new Vector2(forwardX, forwardY);

        if (_canMove)
        {
            _playerRigidbody.AddForce(_forwardDir * _forwardValue);
            _playerRigidbody.AddTorque(_rotateValue,ForceMode2D.Force);
        }
    }

    public void DisableMovement()
    {
        _canMove = false;
        _canShoot = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        _rotateValue = 0;
        _forwardValue = 0;
        
        if (Input.GetKey(KeyCode.A))
        {
            _rotateValue = rotateIntensity;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _rotateValue = -rotateIntensity;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            _forwardValue = forwardSpeed;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && _canShoot && _canMove)
        {
            var newBullet = BulletPool.SharedInstance.GetPooledObject();
            Shoot(newBullet,_forwardDir,forwardBulletOrigin.transform.position,singleFireInterval);
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow) && _canShoot && _canMove)
        {
            var newBulletOne = BulletPool.SharedInstance.GetPooledObject();
            var newBulletTwo  = BulletPool.SharedInstance.GetPooledObject();
            var newBulletThree  = BulletPool.SharedInstance.GetPooledObject();
            var originOne = leftBulletOrigin[0].position;
            var originTwo = leftBulletOrigin[1].position;
            var originThree = leftBulletOrigin[2].position;

            var playerPosition = transform.position;
            Shoot(newBulletOne, originOne - playerPosition,  originOne, specialFireInterval);
            Shoot(newBulletTwo, originOne - playerPosition, originTwo, specialFireInterval);
            Shoot(newBulletThree, originOne - playerPosition, originThree, specialFireInterval);

        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow) && _canShoot && _canMove)
        {
            var newBulletOne = BulletPool.SharedInstance.GetPooledObject();
            var newBulletTwo  = BulletPool.SharedInstance.GetPooledObject();
            var newBulletThree  = BulletPool.SharedInstance.GetPooledObject();
            var originOne = rightBulletOrigin[0].position;
            var originTwo = rightBulletOrigin[1].position;
            var originThree = rightBulletOrigin[2].position;

            var playerPosition = transform.position;
            Shoot(newBulletOne, originOne - playerPosition,  originOne, specialFireInterval);
            Shoot(newBulletTwo, originOne - playerPosition, originTwo, specialFireInterval);
            Shoot(newBulletThree, originOne - playerPosition, originThree, specialFireInterval);
        }

        if (Mathf.Abs(_playerRigidbody.angularVelocity) > maxAngularVelocity)
        {
            _rotateValue = 0;
        }
    }

    void Shoot(GameObject canonball, Vector3 direction, Vector3 origin, float fireInterval)
    {
        if (canonball != null)
        {
            canonball.transform.position = origin;
            canonball.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
            var cannonballBehaviour = canonball.GetComponent<CanonballBehaviour>();
            cannonballBehaviour.SetOriginGameobject(this.gameObject);
            cannonballBehaviour.SetDamage(attackDamage);
            _canShoot = false;
            Invoke(nameof(RechargeCanon),fireInterval);
        }
    }
    
    void RechargeCanon()
    {
        _canShoot = true;
    }
}
