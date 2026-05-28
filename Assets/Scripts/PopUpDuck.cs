using UnityEngine;

public class PopUpDuck : MonoBehaviour
{
    [Header("Pop-Up Settings")]
    [SerializeField] private float popUpDuration = 0.5f;
    [SerializeField] private float upRotation = 90f;

    private bool hasPopped = false;
    private bool isHit = false;
    private Quaternion startRotation;
    private Quaternion upRotation_quat;
    private float popUpTimer = 0f;

    void Start()
    {
        startRotation = transform.localRotation;
        upRotation_quat = startRotation * Quaternion.Euler(0, 0, upRotation);
    }

    void Update()
    {
        if (!hasPopped && !isHit)
        {
            popUpTimer += Time.deltaTime;

            float progress = Mathf.Clamp01(popUpTimer / popUpDuration);
            transform.localRotation = Quaternion.Lerp(startRotation, upRotation_quat, progress);

            if (progress >= 1f)
            {
                hasPopped = true;
            }
        }
    }

    public void Hit()
    {
        if (!isHit)
        {
            isHit = true;
            StartCoroutine(FoldForward());
        }
    }

    private System.Collections.IEnumerator FoldForward()
    {
        Quaternion currentRotation = transform.localRotation;
        Quaternion foldRotation = currentRotation * Quaternion.Euler(0, 0, -90f);
        float elapsed = 0f;

        while (elapsed < 0.5f)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / 0.5f;
            transform.localRotation = Quaternion.Lerp(currentRotation, foldRotation, t);
            yield return null;
        }

        transform.localRotation = foldRotation;
    }
}
