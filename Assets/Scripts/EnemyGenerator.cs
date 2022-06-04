using System.Collections;
using System.Collections.Generic;
using ProjectPlataformer;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    enum EnemyType
    {
        Jumpable = 0,
        Large = 1
    }
    
    private GameObject[] SpawnEnemyLocations;
    private player PlayerRef;
    [SerializeField] private Transform JumpablePrefab;
    [SerializeField] private Transform LargePrefab;
    private GameObject EnemiesParent;

    [SerializeField] [Range(0, 5)] private float spawnCooldownMin;
    [SerializeField] [Range(0, 5)] private float spawnCooldownMax;
    [SerializeField] private float currentSpawnCooldown;

    private void Awake()
    {
        SpawnEnemyLocations = GameObject.FindGameObjectsWithTag("SpawnItemLocation");
        PlayerRef = GameObject.FindGameObjectWithTag("player").GetComponent<player>();
        EnemiesParent = GameObject.FindGameObjectWithTag("EnemiesParent");
    }

    private void Update()
    {
        if (!PlayerRef.isRunning) return;
        
        currentSpawnCooldown -= Time.deltaTime;
        if (currentSpawnCooldown <= 0) // New enemies can spawn
        {
            // spawn enemies at certain distance from the player, in one of the spawn locations]
            SpawnEnemy(new Vector3(PlayerRef.transform.position.x + 23, GetRandomEnemySpawnSpotY(), 0), Random.Range(0, 2));
            // print("Spawn enemie!");
            currentSpawnCooldown = Random.Range(spawnCooldownMin, spawnCooldownMax);
        } 
    }

    private void SpawnEnemy(Vector3 position, int enemyNumber)
    {
        var newEnemyInstantiated = Instantiate( GetEnemyPrefabFromNumber(enemyNumber), position, Quaternion.identity, EnemiesParent.transform);
        Destroy(newEnemyInstantiated.gameObject, 15f);
    }

    private float GetRandomEnemySpawnSpotY()
    {
        return SpawnEnemyLocations[Random.Range(0, 4)].transform.position.y;
    }

    private Transform GetEnemyPrefabFromNumber(int number)
    {
        return number switch
        {
            0 => JumpablePrefab,
            1 => LargePrefab,
            _ => JumpablePrefab
        };
    }
}
