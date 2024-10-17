using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SocketManager))]
public class SocketInteractionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SocketManager socketManager = (SocketManager)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Activate Sockets"))
        {
            socketManager.ActivateSockets();
        }

        if (GUILayout.Button("Deactivate Sockets"))
        {
            socketManager.DeactivateSockets();
        }

        // Deðiþiklikler yapýldýðýnda scriptin güncellenmesini saðla
        if (GUI.changed)
        {
            EditorUtility.SetDirty(socketManager);
        }
    }
}
