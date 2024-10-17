using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SocketManager))]
public class SocketInteractionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // SocketManager scriptine referans al
        SocketManager socketManager = (SocketManager)target;

        // Varsayýlan Inspector çizim fonksiyonunu kullan
        DrawDefaultInspector();

        // Kullanýcýya süre sýnýrýný girmesi için alan oluþtur
        socketManager.timeLimit = EditorGUILayout.IntField("Time Limit (seconds)", socketManager.timeLimit);

        // Deðiþiklikler yapýldýðýnda scriptin güncellenmesini saðla
        if (GUI.changed)
        {
            EditorUtility.SetDirty(socketManager);
        }
    }
}
