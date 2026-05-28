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
        Debug.Log("SetDuckData called");
        duckData = data;

        if (duckData == null)
        {
            Debug.LogWarning("duckData is null");
            return;
        }

        Debug.Log($"DuckData: {duckData.name}, Image: {(duckData.image != null ? duckData.image.name : "null")}");

        if (duckData.image != null)
        {
            DuckTarget target = null;

            if (targetObject != null)
            {
                Debug.Log("Using assigned targetObject");
                target = targetObject.GetComponent<DuckTarget>();
            }
            else
            {
                Debug.Log("Searching for DuckTarget in children");
                target = GetComponentInChildren<DuckTarget>();
            }

            if (target != null)
            {
                Debug.Log("Found DuckTarget, calling ApplyTexture");
                target.ApplyTexture(duckData.image);
            }
            else
            {
                Debug.LogWarning("DuckTarget not found!");
            }
        }
        else
        {
            Debug.LogWarning("duckData.image is null");
        }
    }
}
