using UnityEngine;
using UnityEditor;

public class ChangeLightColorTool : EditorWindow
{
    private Color originalColor = Color.white; // Lambanýn ilk rengi için
    private Color redColor = Color.red; // Kýrmýzý renk
    private Color blueColor = Color.blue; // Mavi renk
    private float lightIntensity = 1f; // Iþýk þiddeti

    [MenuItem("Tools/Change Light Color")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ChangeLightColorTool), false, "Change Light Color");
    }

    private void OnGUI()
    {
        GUILayout.Label("Light Color Changer", EditorStyles.boldLabel);

        // Iþýk þiddetini ayarlama
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
        Light[] lights = FindObjectsOfType<Light>(); // Sahnedeki tüm ýþýk nesnelerini bul

        foreach (Light light in lights)
        {
            // Lambanýn rengini ayarlama
            light.color = color;
            light.intensity = lightIntensity; // Lambanýn þiddetini ayarlama
        }

        Debug.Log($"Tüm lambalarýn rengi {color} olarak deðiþtirildi!");
    }

    private void ResetAllLightsToOriginalColor()
    {
        Light[] lights = FindObjectsOfType<Light>(); // Sahnedeki tüm ýþýk nesnelerini bul

        foreach (Light light in lights)
        {
            light.color = originalColor; // Orijinal rengi geri yükle
            light.intensity = 1f; // Varsayýlan ýþýk þiddeti
        }

        Debug.Log("Tüm lambalar orijinal renge döndürüldü!");
    }
}
