using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private float shipSpeed;
    [SerializeField] [Range(.1f, 1)] private float rotateIntensity;
    [SerializeField] private float maxAngularVelocity;
    [SerializeField] private float attackInterval;
    
    private float _rotateValue;

    private bool _changeRotation;
    // Start is called before the first frame update
    void Start()
    {
        _changeRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        _rotateValue = 0;
        
        if (Input.GetKey(KeyCode.A))
        {
            _changeRotation = true;
            _rotateValue = -rotateIntensity;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _changeRotation = true;
            _rotateValue = rotateIntensity;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
        }

        if (Mathf.Abs(playerRigidbody.angularVelocity) > maxAngularVelocity)
        {
            _rotateValue = 0;
        }
        
        if (_changeRotation)
        {
            playerRigidbody.AddTorque(_rotateValue,ForceMode2D.Force);
            _changeRotation = false;
        }
        
    }
}
