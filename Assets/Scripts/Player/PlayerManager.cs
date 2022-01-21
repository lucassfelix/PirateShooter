using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [Space] [Header("Game Manager")] [SerializeField]
    private GameManager gameManager;
    
    [Space]
    [Header("Player Health")]
    [SerializeField] private int maxHealth;
    
    [Space]
    [Header("Sprite Renderers")]
    [SerializeField] private SpriteRenderer hullRenderer;
    [SerializeField] private SpriteRenderer sailRenderer;

    [Space]
    [Header("Hull Sprites")]
    [SerializeField] private Sprite mediumDamagedHull;
    [SerializeField] private Sprite heavyDamagedHull;
    [SerializeField] private Sprite destroyedHull;
    [Space]
    [Header("Sail Sprites")]
    [SerializeField] private Sprite mediumDamagedSail;
    [SerializeField] private Sprite heavyDamagedSail;
    [SerializeField] private Sprite destroyedSail;

    
    private int _currentHealth;

    private void Start()
    {
        _currentHealth = maxHealth;
    }

    void Defeat()
    {
        GetComponent<PlayerMovement>().DisableMovement();
        gameManager.EndGame();
    }

    public void TakeDamage(int damageTaken)
    {
        _currentHealth -= damageTaken;
        if (_currentHealth <= 0)
        {
            Defeat();
            hullRenderer.sprite = destroyedHull;
            sailRenderer.sprite = destroyedSail;
        }
        else if (_currentHealth == 1)
        {
            hullRenderer.sprite = heavyDamagedHull;
            sailRenderer.sprite = heavyDamagedSail;
        }
        else if (_currentHealth <= maxHealth / 2)
        {
            hullRenderer.sprite = mediumDamagedHull;
            sailRenderer.sprite = mediumDamagedSail;
        }
    }
    
    
    
}
