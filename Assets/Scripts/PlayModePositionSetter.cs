using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayModePositionSetter : MonoBehaviour {

	public static Vector3 savedPosition;
	public static Vector3 savedEulerAngles;
	public static Vector3 savedScale;
#if UNITY_EDITOR
	private static bool hasSavedTransform = false;

	[ContextMenu("Save Current Transform")]
	public void SaveCurrentTransform() {
		if (!Application.isPlaying) {
			Debug.LogWarning("This operation is meant to be used during Play Mode.");
			return;
		}

		// Save the current transform values
		savedPosition = transform.localPosition;
		savedEulerAngles = transform.localEulerAngles;
		savedScale = transform.localScale;
		hasSavedTransform = true;

		Debug.Log($"Transform saved: Position {savedPosition}, EulerAngles {savedEulerAngles}, Scale {savedScale}");
	}

	// Automatically apply saved values when exiting Play Mode
	[InitializeOnLoadMethod]
	private static void OnLoad() {
		EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
	}

	private static void OnPlayModeStateChanged(PlayModeStateChange state) {
		// Apply saved transform values when exiting Play Mode
		if (state == PlayModeStateChange.ExitingPlayMode && hasSavedTransform) {
			// Delay the application of transform changes to after Play Mode
			EditorApplication.delayCall += ApplySavedTransform;
		}
	}

	private static void ApplySavedTransform() {
		var target = FindObjectOfType<PlayModePositionSetter>();
		if (target != null) {
			Undo.RecordObject(target.transform, "Apply Saved Transform");
			target.transform.localPosition = savedPosition;
			target.transform.localEulerAngles = savedEulerAngles;
			target.transform.localScale = savedScale;

			// Mark the scene as dirty to persist changes
			EditorSceneManager.MarkSceneDirty(target.gameObject.scene);
			Debug.Log("Saved transform applied and scene marked dirty.");
		}

		hasSavedTransform = false; // Reset flag after applying

	}
#endif
}
