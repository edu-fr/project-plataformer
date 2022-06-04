using System.Collections;
using System.Collections.Generic;
using ProjectPlataformer;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    private GameObject[] SpawnItemLocations;
    private player PlayerRef;
    [SerializeField] private Transform DonutPrefab;
    [SerializeField] private Transform PepperPrefab;
    private GameObject ItemsParent;

    [SerializeField] [Range(0, 5)] private float spawnCooldownMin;
    [SerializeField] [Range(0, 5)] private float spawnCooldownMax;
    [SerializeField] private float currentSpawnCooldown;

    private void Awake()
    {
        SpawnItemLocations = GameObject.FindGameObjectsWithTag("SpawnItemLocation");
        PlayerRef = GameObject.FindGameObjectWithTag("player").GetComponent<player>();
        ItemsParent = GameObject.FindGameObjectWithTag("ItemsParent");
    }

    private void Update()
    {
        if (!PlayerRef.isRunning) return;
        
        currentSpawnCooldown -= Time.deltaTime;
        if (currentSpawnCooldown <= 0) // New items can spawn
        {
            var itemNumber = 0;
            // spawn item at certain distance from the player, in one of the spawn locations]
            SpawnItem(new Vector3(PlayerRef.transform.position.x + Random.Range(20, 25), GetRandomItemSpawnSpotY(), 0), Random.Range(0, 100) > 80 ? 1 : 0); // 80% donut
            currentSpawnCooldown = Random.Range(spawnCooldownMin, spawnCooldownMax);
        } 
    }

    private void SpawnItem(Vector3 position, int itemNumber)
    {
        var newItemInstantiated = Instantiate( GetItemPrefabFromNumber(itemNumber), position, Quaternion.identity, ItemsParent.transform);
        newItemInstantiated.GetComponent<ItemScript>().itemType = GetItemTypeFromNumber(itemNumber);
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
    
    private ItemScript.ItemType GetItemTypeFromNumber(int number)
    {
        return number switch
        {
            0 => ItemScript.ItemType.Donut,
            1 => ItemScript.ItemType.Pepper,
            _ => ItemScript.ItemType.Donut
        };
    }
}
