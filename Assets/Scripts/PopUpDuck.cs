using UnityEngine;

public class PopUpDuck : MonoBehaviour
{
    [Header("Pop-Up Settings")]
    [SerializeField] private float popUpDuration = 0.5f;
    [SerializeField] private float upRotation = 90f;

    [Header("Duck Data")]
    [SerializeField] private DuckData duckData;
    [SerializeField] private GameObject targetObject;

    private bool hasPopped = false;
    private bool isHit = false;
    private Quaternion startRotation;
    private Quaternion upRotation_quat;
    private float popUpTimer = 0f;

    void Start()
    {
        startRotation = transform.localRotation;
        upRotation_quat = startRotation * Quaternion.Euler(0, 0, upRotation);

        if (duckData != null && duckData.image != null)
        {
            ApplyImage();
        }
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

    public void SetDuckData(DuckData data)
    {
        duckData = data;

        if (duckData != null && duckData.image != null)
        {
            ApplyImage();
        }
    }

    private void ApplyImage()
    {
        DuckTarget target = null;

        if (targetObject != null)
        {
            target = targetObject.GetComponent<DuckTarget>();
        }
        else
        {
            target = GetComponentInChildren<DuckTarget>();
        }

        if (target != null && duckData.image != null)
        {
            target.ApplyTexture(duckData.image);
        }
    }

    public int GetPointValue()
    {
        return duckData != null ? duckData.pointValue : 10;
    }

    public DuckData GetDuckData()
    {
        return duckData;
    }
}
