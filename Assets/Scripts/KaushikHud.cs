using UnityEngine;

public class KaushikHud : MonoBehaviour
{
    [Header("Public HUD Values")]
    public int ammoCount = 30;
    public int maxAmmoCount = 30;
    public float timerSeconds = 60f;
    public float maxTimerSeconds = 60f;
    public int enemiesDestroyed = 0;

    [Header("Timer")]
    public bool countTimerUp = false;

    [Header("Display")]
    public int fontSize = 11;
    public int screenPadding = 6;

    private GUIStyle centerStyle;
    private GUIStyle goldStyle;
    private Texture2D portraitTexture;
    private Texture2D goldCoinTexture;

    private void Update()
    {
        if (countTimerUp)
        {
            timerSeconds += Time.deltaTime;
        }
        else
        {
            timerSeconds = Mathf.Max(0f, timerSeconds - Time.deltaTime);
        }
    }

    private void OnGUI()
    {
        EnsureResources();

        float hudWidth = 390f;
        float hudHeight = 58f;
        float x = Mathf.Max(screenPadding, Screen.width * 0.5f - hudWidth - 36f);
        float y = Screen.height - hudHeight - screenPadding;

        DrawPanel(new Rect(x, y, hudWidth, hudHeight));

        Rect portraitRect = new Rect(x + 9f, y + 8f, 42f, 42f);
        GUI.DrawTexture(portraitRect, portraitTexture, ScaleMode.StretchToFill, true);
        DrawRectBorder(portraitRect, new Color(0.86f, 0.68f, 0.28f), 2f);

        Rect hpRect = new Rect(x + 60f, y + 12f, 230f, 13f);
        Rect manaRect = new Rect(x + 60f, y + 31f, 230f, 13f);
        DrawBar(hpRect, timerSeconds, maxTimerSeconds, new Color(0.07f, 0.78f, 0.12f), new Color(0.02f, 0.36f, 0.05f));
        DrawBar(manaRect, ammoCount, maxAmmoCount, new Color(0.09f, 0.44f, 0.94f), new Color(0.02f, 0.14f, 0.34f));

        GUI.Label(hpRect, $"{Mathf.CeilToInt(timerSeconds)} / {Mathf.CeilToInt(maxTimerSeconds)}", centerStyle);
        GUI.Label(manaRect, $"{ammoCount} / {maxAmmoCount}", centerStyle);

        Rect goldPanelRect = new Rect(x + 305f, y + 17f, 68f, 24f);
        DrawInsetPanel(goldPanelRect);
        GUI.DrawTexture(new Rect(goldPanelRect.x + 7f, goldPanelRect.y + 5f, 14f, 14f), goldCoinTexture);
        GUI.Label(new Rect(goldPanelRect.x + 27f, goldPanelRect.y + 2f, 34f, 20f), enemiesDestroyed.ToString(), goldStyle);
    }

    private void EnsureResources()
    {
        if (centerStyle != null && centerStyle.fontSize == fontSize)
        {
            return;
        }

        centerStyle = CreateStyle(TextAnchor.MiddleCenter, Color.white);
        goldStyle = CreateStyle(TextAnchor.MiddleLeft, new Color(1f, 0.91f, 0.45f));

        portraitTexture = CreateCircleTexture(96, new Color(0.42f, 0.08f, 0.13f), new Color(0.95f, 0.57f, 0.28f));
        goldCoinTexture = CreateCircleTexture(32, new Color(1f, 0.72f, 0.12f), new Color(0.55f, 0.34f, 0.04f));
    }

    private GUIStyle CreateStyle(TextAnchor alignment, Color color)
    {
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.alignment = alignment;
        style.fontSize = fontSize;
        style.fontStyle = FontStyle.Bold;
        style.normal.textColor = color;
        return style;
    }

    private void DrawPanel(Rect rect)
    {
        DrawRect(rect, new Color(0.03f, 0.08f, 0.08f, 0.78f));
        DrawRectBorder(rect, new Color(0.55f, 0.47f, 0.26f), 2f);
        DrawRectBorder(new Rect(rect.x + 4f, rect.y + 4f, rect.width - 8f, rect.height - 8f), new Color(0.08f, 0.2f, 0.18f), 1f);
    }

    private void DrawInsetPanel(Rect rect)
    {
        DrawRect(rect, new Color(0.04f, 0.13f, 0.12f, 0.95f));
        DrawRectBorder(rect, new Color(0.68f, 0.56f, 0.29f), 2f);
    }

    private void DrawBar(Rect rect, float current, float maximum, Color fillColor, Color shadowColor)
    {
        float fillAmount = maximum <= 0f ? 0f : Mathf.Clamp01(current / maximum);

        DrawRect(rect, new Color(0.01f, 0.04f, 0.04f, 1f));
        DrawRect(new Rect(rect.x + 2f, rect.y + 2f, (rect.width - 4f) * fillAmount, rect.height - 4f), fillColor);
        DrawRect(new Rect(rect.x + 2f, rect.y + rect.height * 0.58f, (rect.width - 4f) * fillAmount, rect.height * 0.24f), shadowColor);
        DrawRectBorder(rect, new Color(0.59f, 0.55f, 0.32f), 1f);
    }

    private void DrawRect(Rect rect, Color color)
    {
        Color oldColor = GUI.color;
        GUI.color = color;
        GUI.DrawTexture(rect, Texture2D.whiteTexture);
        GUI.color = oldColor;
    }

    private void DrawRectBorder(Rect rect, Color color, float thickness)
    {
        DrawRect(new Rect(rect.x, rect.y, rect.width, thickness), color);
        DrawRect(new Rect(rect.x, rect.yMax - thickness, rect.width, thickness), color);
        DrawRect(new Rect(rect.x, rect.y, thickness, rect.height), color);
        DrawRect(new Rect(rect.xMax - thickness, rect.y, thickness, rect.height), color);
    }

    private Texture2D CreateCircleTexture(int size, Color centerColor, Color edgeColor)
    {
        Texture2D texture = new Texture2D(size, size);
        texture.wrapMode = TextureWrapMode.Clamp;

        float radius = size * 0.5f;
        Vector2 center = new Vector2(radius, radius);

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float distance = Vector2.Distance(new Vector2(x, y), center);
                float blend = Mathf.Clamp01(distance / radius);
                Color color = Color.Lerp(centerColor, edgeColor, blend);
                color.a = distance <= radius ? 1f : 0f;
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
        return texture;
    }
}
