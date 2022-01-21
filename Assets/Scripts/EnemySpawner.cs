using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject playerReference;
    [SerializeField] private ScoreManager scoreManagerReference;
    
    
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private List<ObjectPool> enemyTypePools;

    private float _spawnTime;
    
    void SpawnEnemy()
    {
        var randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        var randomSpawnEnemy =  enemyTypePools[Random.Range(0, enemyTypePools.Count)];
        
        var newEnemy = randomSpawnEnemy.GetPooledObject();
        
        if (newEnemy == null) return;
        
        newEnemy.transform.position = randomSpawnPoint.position;
        var newEnemyBehaviour = newEnemy.GetComponents<EnemyBehaviour>()[0];
        newEnemyBehaviour.Init();
        newEnemyBehaviour.SetPlayerReference(playerReference);
        newEnemyBehaviour.SetScoreManagerReference(scoreManagerReference);
        newEnemyBehaviour.SetPoolReference(randomSpawnEnemy);

        Invoke(nameof(SpawnEnemy),_spawnTime);

    }
    void Start()
    {
        _spawnTime = PlayerPrefs.GetFloat("SpawnTime");
        Invoke(nameof(SpawnEnemy),_spawnTime);
    }
}
