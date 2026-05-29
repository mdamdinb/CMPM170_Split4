using UnityEngine;

public class DuckMovement : MonoBehaviour
{
    private Vector3 startPosition;
    private PopUpDuck popUpDuck;
    private bool moveBackward = false;

    void Start()
    {
        startPosition = transform.position;
        popUpDuck = GetComponent<PopUpDuck>();
    }

    void Update()
    {
        DuckData duckData = popUpDuck != null ? popUpDuck.GetDuckData() : null;

        float currentSpeed = duckData != null ? duckData.speed : 3f;
        Vector3 direction = moveBackward ? Vector3.back : Vector3.forward;
        transform.position += direction * currentSpeed * Time.deltaTime;

        float maxDistance = duckData != null ? duckData.travelDistance : 15f;
        float distanceTraveled = Vector3.Distance(startPosition, transform.position);
        if (distanceTraveled >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    public void SetMoveBackward(bool backward)
    {
        moveBackward = backward;
    }
}
