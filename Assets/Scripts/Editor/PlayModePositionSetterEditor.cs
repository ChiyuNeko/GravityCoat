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

        // Display saved variables (if any)
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Saved Transform Values", EditorStyles.boldLabel);
        if (Application.isPlaying) {
            EditorGUILayout.Vector3Field("Saved Position", PlayModePositionSetter.savedPosition);
            EditorGUILayout.Vector3Field("Saved Euler Angle", PlayModePositionSetter.savedEulerAngles);
            EditorGUILayout.Vector3Field("Saved Scale", PlayModePositionSetter.savedScale);
        } else {
            EditorGUILayout.HelpBox("No transform values have been saved yet.", MessageType.Info);
        }
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
