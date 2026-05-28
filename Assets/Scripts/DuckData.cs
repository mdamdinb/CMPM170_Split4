using UnityEngine;

[CreateAssetMenu(fileName = "NewDuckData", menuName = "Duck Hunt/Duck Data")]
public class DuckData : ScriptableObject
{
    [Header("Movement")]
    public float speed = 3f;
    public float travelDistance = 10f;

    [Header("Scoring")]
    public int pointValue = 10;

    [Header("Visual")]
    public Sprite image = null;
}
