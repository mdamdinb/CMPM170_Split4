using UnityEngine;

public class KaushikHud : MonoBehaviour
{
    [Header("Public HUD Values")]
    public int ammoCount = 30;
    public float timerSeconds = 0f;
    public int enemiesDestroyed = 0;

    [Header("Timer")]
    public bool countTimerUp = true;

    [Header("Display")]
    public bool showLabels = true;
    public int fontSize = 32;
    public int screenPadding = 24;

    private GUIStyle leftStyle;
    private GUIStyle rightStyle;

    private void Update()
    {
        if (countTimerUp)
        {
            timerSeconds += Time.deltaTime;
        }
    }

    private void OnGUI()
    {
        EnsureStyles();

        float boxWidth = 260f;
        float boxHeight = 48f;
        float bottomY = Screen.height - screenPadding - boxHeight;

        GUI.Label(
            new Rect(screenPadding, bottomY, boxWidth, boxHeight),
            FormatValue("Ammo", ammoCount.ToString()),
            leftStyle);

        GUI.Label(
            new Rect(Screen.width - screenPadding - boxWidth, screenPadding, boxWidth, boxHeight),
            FormatValue("Timer", Mathf.FloorToInt(timerSeconds).ToString()),
            rightStyle);

        GUI.Label(
            new Rect(Screen.width - screenPadding - boxWidth, bottomY, boxWidth, boxHeight),
            FormatValue("Enemies", enemiesDestroyed.ToString()),
            rightStyle);
    }

    private void EnsureStyles()
    {
        if (leftStyle != null && leftStyle.fontSize == fontSize)
        {
            return;
        }

        leftStyle = CreateStyle(TextAnchor.MiddleLeft);
        rightStyle = CreateStyle(TextAnchor.MiddleRight);
    }

    private GUIStyle CreateStyle(TextAnchor alignment)
    {
        return new GUIStyle(GUI.skin.label)
        {
            alignment = alignment,
            fontSize = fontSize,
            fontStyle = FontStyle.Bold,
            normal = { textColor = Color.white }
        };
    }

    private string FormatValue(string label, string value)
    {
        return showLabels ? $"{label}: {value}" : value;
    }
}
