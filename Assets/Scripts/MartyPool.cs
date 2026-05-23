using UnityEngine;
using UnityEngine.Pool;

public class MartyPool : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private int defaultCapacity = 10;
    [SerializeField] private int maxSize = 20;

    private ObjectPool<GameObject> pool;

    // Other scripts grab the pool reference via MartyPool.Instance
    public static MartyPool Instance { get; private set; }

    void Awake()
    {
        Instance = this;

        pool = new ObjectPool<GameObject>(
            createFunc:    () => Instantiate(ballPrefab),
            actionOnGet:   ball => ball.SetActive(true),
            actionOnRelease: ball =>
            {
                ball.SetActive(false);
                ball.transform.SetParent(transform); // tidy: keep pool objects under this GO
            },
            actionOnDestroy: ball => Destroy(ball),
            collectionCheck: false,
            defaultCapacity: defaultCapacity,
            maxSize: maxSize
        );
    }

    public GameObject Get() => pool.Get();
    public void Release(GameObject ball) => pool.Release(ball);
}