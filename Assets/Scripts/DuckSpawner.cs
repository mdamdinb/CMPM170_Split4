using UnityEngine;
using System.Collections.Generic;

public class DuckSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject duckPrefab;
    [SerializeField] private DuckData[] duckTypes;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private int maxDucks = 10;

    private float nextSpawnTime;
    private int ducksSpawned;
    private List<DuckData> spawnPool;

    void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
        BuildSpawnPool();
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime && ducksSpawned < maxDucks)
        {
            SpawnDuck();
            nextSpawnTime = Time.time + spawnInterval;
            ducksSpawned++;
        }
    }

    private void BuildSpawnPool()
    {
        spawnPool = new List<DuckData>();

        if (duckTypes == null || duckTypes.Length == 0) return;

        int totalWeight = 0;
        foreach (DuckData duckType in duckTypes)
        {
            if (duckType != null)
            {
                totalWeight += duckType.spawnWeight;
            }
        }

        if (totalWeight == 0) return;

        foreach (DuckData duckType in duckTypes)
        {
            if (duckType != null)
            {
                float proportion = (float)duckType.spawnWeight / totalWeight;
                int count = Mathf.RoundToInt(proportion * maxDucks);

                for (int i = 0; i < count; i++)
                {
                    spawnPool.Add(duckType);
                }
            }
        }
    }

    private void SpawnDuck()
    {
        GameObject duck = Instantiate(duckPrefab, transform.position, transform.rotation);

        if (spawnPool != null && spawnPool.Count > 0)
        {
            int randomIndex = Random.Range(0, spawnPool.Count);
            DuckData selectedType = spawnPool[randomIndex];
            spawnPool.RemoveAt(randomIndex);

            PopUpDuck popUpDuck = duck.GetComponent<PopUpDuck>();
            if (popUpDuck != null)
            {
                popUpDuck.SetDuckData(selectedType);
            }
        }
    }
}
