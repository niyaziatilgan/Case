using UnityEngine;
using UnityEditor;

public class ChangeLightColorTool : EditorWindow
{
    private Color originalColor = Color.white; // Lamban�n ilk rengi i�in
    private Color redColor = Color.red; // K�rm�z� renk
    private Color blueColor = Color.blue; // Mavi renk
    private float lightIntensity = 1f; // I��k �iddeti

    [MenuItem("Tools/Change Light Color")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ChangeLightColorTool), false, "Change Light Color");
    }

    private void OnGUI()
    {
        GUILayout.Label("Light Color Changer", EditorStyles.boldLabel);

        // I��k �iddetini ayarlama
        lightIntensity = EditorGUILayout.Slider("Light Intensity", lightIntensity, 0f, 10f);

        if (GUILayout.Button("Change All Lights to Red"))
        {
            ChangeAllLightsToColor(redColor);
        }

        if (GUILayout.Button("Change All Lights to Blue"))
        {
            ChangeAllLightsToColor(blueColor);
        }

        if (GUILayout.Button("Reset All Lights to Original Color"))
        {
            ResetAllLightsToOriginalColor();
        }
    }

    private void ChangeAllLightsToColor(Color color)
    {
        Light[] lights = FindObjectsOfType<Light>(); // Sahnedeki t�m ���k nesnelerini bul

        foreach (Light light in lights)
        {
            // Lamban�n rengini ayarlama
            light.color = color;
            light.intensity = lightIntensity; // Lamban�n �iddetini ayarlama
        }

        Debug.Log($"T�m lambalar�n rengi {color} olarak de�i�tirildi!");
    }

    private void ResetAllLightsToOriginalColor()
    {
        Light[] lights = FindObjectsOfType<Light>(); // Sahnedeki t�m ���k nesnelerini bul

        foreach (Light light in lights)
        {
            light.color = originalColor; // Orijinal rengi geri y�kle
            light.intensity = 1f; // Varsay�lan ���k �iddeti
        }

        Debug.Log("T�m lambalar orijinal renge d�nd�r�ld�!");
    }
}
