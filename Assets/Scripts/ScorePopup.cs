using UnityEngine;
using TMPro;

public class ScorePopup : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float lifetime = 1.2f;
    [SerializeField] private Vector3 riseSpeed = new Vector3(0f, 1f, 0f);

    private float timer;

    private void Awake()
    {
        timer = lifetime;
    }

    private void Update()
    {
        transform.position += riseSpeed * Time.deltaTime;
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void SetScore(int points)
    {
        if (scoreText != null)
        {
            scoreText.text = "+" + points.ToString();
        }
    }
}
