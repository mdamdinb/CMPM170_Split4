using UnityEngine;

public class DuckSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject duckPrefab;
    [SerializeField] private DuckData[] duckTypes;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private int maxDucks = 10;

    private float nextSpawnTime;
    private int ducksSpawned;

    void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
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

    private void SpawnDuck()
    {
        Debug.Log("SpawnDuck called");
        GameObject duck = Instantiate(duckPrefab, transform.position, transform.rotation);

        Debug.Log($"DuckTypes array length: {(duckTypes != null ? duckTypes.Length : 0)}");

        if (duckTypes != null && duckTypes.Length > 0)
        {
            DuckData randomType = duckTypes[Random.Range(0, duckTypes.Length)];
            Debug.Log($"Selected random type: {(randomType != null ? randomType.name : "null")}");

            DuckMovement movement = duck.GetComponent<DuckMovement>();
            Debug.Log($"DuckMovement found: {movement != null}");

            if (movement != null)
            {
                movement.SetDuckData(randomType);
            }
        }
        else
        {
            Debug.LogWarning("No duck types assigned to spawner!");
        }
    }
}
