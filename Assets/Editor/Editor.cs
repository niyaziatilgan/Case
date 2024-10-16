using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SocketInteraction))]
public class SocketInteractionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // SocketInteraction scriptine referans al
        SocketInteraction socketInteraction = (SocketInteraction)target;

        // Varsay�lan Inspector �izim fonksiyonunu kullan
        DrawDefaultInspector();

        // Kullan�c�ya s�re s�n�r�n� girmesi i�in alan olu�tur
        socketInteraction.timeLimit = EditorGUILayout.FloatField("Time Limit (seconds)", socketInteraction.timeLimit);

        // De�i�iklikler yap�ld���nda scriptin g�ncellenmesini sa�la
        if (GUI.changed)
        {
            EditorUtility.SetDirty(socketInteraction);
        }
    }
}
