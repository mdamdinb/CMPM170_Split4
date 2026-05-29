using UnityEngine;

[CreateAssetMenu(fileName = "NewDuckData", menuName = "Duck Hunt/Duck Data")]
public class DuckData : ScriptableObject
{
    [Header("Movement")]
    public float speed = 1f;
    public float travelDistance = 15f;

    [Header("Scoring")]
    public int pointValue = 10;

    [Header("Spawning")]
    public int spawnWeight = 10;

    [Header("Visual")]
    public Sprite image = null;
}
