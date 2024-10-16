using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SocketInteraction))]
public class SocketInteractionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // SocketInteraction scriptine referans al
        SocketInteraction socketInteraction = (SocketInteraction)target;

        // Varsayýlan Inspector çizim fonksiyonunu kullan
        DrawDefaultInspector();

        // Kullanýcýya süre sýnýrýný girmesi için alan oluþtur
        socketInteraction.timeLimit = EditorGUILayout.FloatField("Time Limit (seconds)", socketInteraction.timeLimit);

        // Deðiþiklikler yapýldýðýnda scriptin güncellenmesini saðla
        if (GUI.changed)
        {
            EditorUtility.SetDirty(socketInteraction);
        }
    }
}
