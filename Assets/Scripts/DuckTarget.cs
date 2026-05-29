using UnityEngine;

public class DuckTarget : MonoBehaviour
{
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
                popUpDuck.Hit();
            }
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
