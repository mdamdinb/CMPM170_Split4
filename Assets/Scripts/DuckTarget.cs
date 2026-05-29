using UnityEngine;

public class DuckTarget : MonoBehaviour
{
    [Header("Score Popup")]
    [SerializeField] private GameObject scorePopupPrefab;
    [SerializeField] private Vector3 scorePopupOffset = new Vector3(0f, 1.5f, 0f);

    private bool isHit = false;
    private MeshRenderer meshRenderer;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball") && !isHit)
        {
            isHit = true;

            PopUpDuck popUpDuck = GetComponentInParent<PopUpDuck>();
            if (popUpDuck != null)
            {
                int points = popUpDuck.GetPointValue();
                if (ScoreManager.Instance != null)
                {
                    ScoreManager.Instance.AddScore(points);
                }

                SpawnScorePopup(points);
                popUpDuck.Hit();
            }
        }
    }

    private void SpawnScorePopup(int points)
    {
        if (scorePopupPrefab == null) return;

        GameObject popupInstance = Instantiate(scorePopupPrefab, transform.position + scorePopupOffset, Quaternion.identity);
        ScorePopup scorePopup = popupInstance.GetComponent<ScorePopup>();
        if (scorePopup != null)
        {
            scorePopup.SetScore(points);
            return;
        }

        TMPro.TMP_Text tmpText = popupInstance.GetComponentInChildren<TMPro.TMP_Text>();
        if (tmpText != null)
        {
            tmpText.text = "+" + points.ToString();
        }
    }

    public void ApplyTexture(Sprite sprite)
    {
        if (sprite == null) return;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = sprite;
        }
        else
        {
            if (meshRenderer == null)
            {
                meshRenderer = GetComponent<MeshRenderer>();
            }

            if (meshRenderer != null)
            {
                Material newMaterial = new Material(meshRenderer.material);
                newMaterial.mainTexture = sprite.texture;
                meshRenderer.material = newMaterial;
            }
        }
    }
}
