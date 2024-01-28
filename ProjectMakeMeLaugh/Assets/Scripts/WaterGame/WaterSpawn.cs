using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpawn : MonoBehaviour
{
    public GameObject waterUnitPrefab; // Water Prefab
    public int spawnCount = 180; // Spawn amount

    private List<GameObject> waterUnits = new List<GameObject>(); // Store all spawned prefab

    void Start()
    {
        SpawnWaterUnits();
    }

    void SpawnWaterUnits()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject droplet = Instantiate(waterUnitPrefab, this.transform.position+ new Vector3(Random.Range(-1,1), Random.Range(-1, 1), 0), Quaternion.identity);
            waterUnits.Add(droplet);
        }
    }

    public void DestroyAllDroplets()
    {
        foreach (GameObject droplet in waterUnits)
        {
            Destroy(droplet);
        }
        waterUnits.Clear();
    }
}
