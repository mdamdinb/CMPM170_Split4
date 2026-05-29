using UnityEngine;

public class DuckMovement : MonoBehaviour
{
    private Vector3 startPosition;
    private PopUpDuck popUpDuck;

    void Start()
    {
        startPosition = transform.position;
        popUpDuck = GetComponent<PopUpDuck>();
    }

    void Update()
    {
        DuckData duckData = popUpDuck != null ? popUpDuck.GetDuckData() : null;

        float currentSpeed = duckData != null ? duckData.speed : 3f;
        transform.position += Vector3.forward * currentSpeed * Time.deltaTime;

        float maxDistance = duckData != null ? duckData.travelDistance : 15f;
        float distanceTraveled = Vector3.Distance(startPosition, transform.position);
        if (distanceTraveled >= maxDistance)
        {
            Destroy(gameObject);
        }
    }
}
