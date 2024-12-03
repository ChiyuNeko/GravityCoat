using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayModePositionSetter))]
public class PlayModePositionSetterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Add a button below the default UI
        PlayModePositionSetter script = (PlayModePositionSetter)target;
        // Draw the default Inspector UI
        DrawDefaultInspector();

        EditorGUILayout.Space();
        if (GUILayout.Button("Save Current Transform"))
        {
            if (Application.isPlaying)
            {
                // Call the method when the button is clicked
                script.SaveCurrentTransform();
            }
            else
            {
                Debug.LogWarning("This button works only during Play Mode.");
            }
        }
    }
}
