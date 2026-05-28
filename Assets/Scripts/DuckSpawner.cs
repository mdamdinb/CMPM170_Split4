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
        GameObject duck = Instantiate(duckPrefab, transform.position, transform.rotation);

        if (duckTypes != null && duckTypes.Length > 0)
        {
            DuckData randomType = duckTypes[Random.Range(0, duckTypes.Length)];
            DuckMovement movement = duck.GetComponent<DuckMovement>();
            if (movement != null)
            {
                movement.SetDuckData(randomType);
            }
        }
    }
}
