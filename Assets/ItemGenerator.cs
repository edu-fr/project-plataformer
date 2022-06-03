using System.Collections;
using System.Collections.Generic;
using ProjectPlataformer;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    enum ItemType
    {
        Donut = 0,
        Pepper = 1
    }
    
    private GameObject[] SpawnItemLocations;
    private Player PlayerRef;
    [SerializeField] private Transform DonutPrefab;
    [SerializeField] private Transform PepperPrefab;
    private GameObject ItemsParent;

    [SerializeField] [Range(0, 5)] private float spawnCooldownMin;
    [SerializeField] [Range(0, 5)] private float spawnCooldownMax;
    [SerializeField] private float currentSpawnCooldown;

    private void Awake()
    {
        SpawnItemLocations = GameObject.FindGameObjectsWithTag("SpawnItemLocation");
        PlayerRef = GameObject.FindGameObjectWithTag("player").GetComponent<Player>();
        ItemsParent = GameObject.FindGameObjectWithTag("ItemsParent");
    }

    private void Update()
    {
        if (!PlayerRef.isRunning) return;
        
        currentSpawnCooldown -= Time.deltaTime;
        if (currentSpawnCooldown <= 0) // New items can spawn
        {
            // spawn item at certain distance from the player, in one of the spawn locations]
            SpawnItem(new Vector3(PlayerRef.transform.position.x + 30, GetRandomItemSpawnSpotY(), 0), Random.Range(0, 2));
            // print("Spawn item!");
            currentSpawnCooldown = Random.Range(spawnCooldownMin, spawnCooldownMax);
        } 
    }

    private void SpawnItem(Vector3 position, int itemNumber)
    {
        var newItemInstantiated = Instantiate( GetItemPrefabFromNumber(itemNumber), position, Quaternion.identity, ItemsParent.transform);
        Destroy(newItemInstantiated.gameObject, 15f);
    }

    private float GetRandomItemSpawnSpotY()
    {
        return SpawnItemLocations[Random.Range(0, 4)].transform.position.y;
    }

    private Transform GetItemPrefabFromNumber(int number)
    {
        return number switch
        {
            0 => DonutPrefab,
            1 => PepperPrefab,
            _ => DonutPrefab
        };
    }
}
