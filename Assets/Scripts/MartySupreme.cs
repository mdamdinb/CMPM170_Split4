using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MartySupreme : MonoBehaviour
{
    [Header("Lifetime")]
    [SerializeField] private float maxLifetime = 4f;      // failsafe if it never hits anything
    [SerializeField] private float returnDelay = 1.2f;    // seconds after hitting before returning to pool

    [Header("Bounce Reaction")]
    [SerializeField] private float hitTorqueStrength = 8f; // spin on impact

    private Rigidbody rb;
    private Coroutine returnCoroutine;
    private bool hasHit = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Called by ShootController each time this ball is fired
    public void Launch(Vector3 origin, Vector3 direction, float force)
    {
        // Reset state for reuse from pool
        hasHit = false;
        transform.position = origin;
        transform.rotation = Random.rotation; // random start spin looks natural
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.AddForce(direction * force, ForceMode.Impulse);

        // Failsafe: return to pool after maxLifetime even if it never hits anything
        if (returnCoroutine != null) StopCoroutine(returnCoroutine);
        returnCoroutine = StartCoroutine(ReturnAfterDelay(maxLifetime));
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return; // only react to the first hit
        hasHit = true;

        // Add a random spin so it tumbles naturally after bouncing
        rb.AddTorque(Random.insideUnitSphere * hitTorqueStrength, ForceMode.Impulse);

        // Cancel the lifetime failsafe and start the shorter post-hit timer
        if (returnCoroutine != null) StopCoroutine(returnCoroutine);
        returnCoroutine = StartCoroutine(ReturnAfterDelay(returnDelay));
    }

    private IEnumerator ReturnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        MartyPool.Instance.Release(gameObject);
    }
}