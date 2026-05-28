using UnityEngine;

public class DuckMovement : MonoBehaviour
{
    [Header("Duck Data")]
    [SerializeField] private DuckData duckData;
    [SerializeField] private GameObject targetObject;

    private Vector3 startPosition;
    private MeshRenderer meshRenderer;

    void Start()
    {
        startPosition = transform.position;

        if (targetObject != null)
        {
            meshRenderer = targetObject.GetComponent<MeshRenderer>();
        }

        if (duckData != null && meshRenderer != null && duckData.image != null)
        {
            Material newMaterial = new Material(meshRenderer.material);
            newMaterial.mainTexture = duckData.image.texture;
            meshRenderer.material = newMaterial;
        }
    }

    void Update()
    {
        float currentSpeed = duckData != null ? duckData.speed : 3f;
        transform.position += Vector3.forward * currentSpeed * Time.deltaTime;

        float maxDistance = duckData != null ? duckData.travelDistance : 10f;
        float distanceTraveled = Vector3.Distance(startPosition, transform.position);
        if (distanceTraveled >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    public int GetPointValue()
    {
        return duckData != null ? duckData.pointValue : 10;
    }

    public void SetDuckData(DuckData data)
    {
        duckData = data;

        if (targetObject != null && duckData != null && duckData.image != null)
        {
            DuckTarget target = targetObject.GetComponent<DuckTarget>();
            if (target != null)
            {
                target.ApplyTexture(duckData.image);
            }
        }
    }
}
