using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SocketManager))]
public class SocketInteractionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // SocketManager scriptine referans al
        SocketManager socketManager = (SocketManager)target;

        // Varsay�lan Inspector �izim fonksiyonunu kullan
        DrawDefaultInspector();

        // Kullan�c�ya s�re s�n�r�n� girmesi i�in alan olu�tur
        socketManager.timeLimit = EditorGUILayout.IntField("Time Limit (seconds)", socketManager.timeLimit);

        // De�i�iklikler yap�ld���nda scriptin g�ncellenmesini sa�la
        if (GUI.changed)
        {
            EditorUtility.SetDirty(socketManager);
        }
    }
}
