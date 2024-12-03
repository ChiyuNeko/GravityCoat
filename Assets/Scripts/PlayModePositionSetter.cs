using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayModePositionSetter : MonoBehaviour {

	private static Dictionary<GameObject, Vector3> savedPositions = new Dictionary<GameObject, Vector3>();
	public static Vector3 savedPosition;
	public static Vector3 savedEulerAngles;
	public static Vector3 savedScale;
#if UNITY_EDITOR
	private static bool hasSavedTransform = false;

	[ContextMenu("Save All Setter Object Transform")]
	public void SaveCurrentTransform() {
		if (!Application.isPlaying) {
			Debug.LogWarning("This operation is meant to be used during Play Mode.");
			return;
		}

		List<PlayModePositionSetterObject> setterObjects = new List<PlayModePositionSetterObject>(FindObjectsOfType<PlayModePositionSetterObject>());
		for (int i = 0; i < setterObjects.Count; i++) {
			Debug.Log(setterObjects[i].name);
			if (!savedPositions.ContainsKey(setterObjects[i].gameObject)) {
				savedPositions.Add(setterObjects[i].gameObject, setterObjects[i].transform.localPosition);
			}
		}

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
		List<PlayModePositionSetterObject> targets = new List<PlayModePositionSetterObject>(FindObjectsOfType<PlayModePositionSetterObject>());
		List<GameObject> keys = new List<GameObject>(savedPositions.Keys);
		for (int i = 0; i < keys.Count; i++) {
			Debug.Log(keys[i].name);
		}
		for (int i = 0; i < keys.Count; i++) {
			Debug.Log(keys[i].transform.localPosition);
			// if (savedTransforms[i] != null) {
				// Undo.RecordObject(savedTransforms[i].transform, "Apply Saved Transform");
				keys[i].transform.localPosition = savedPositions[keys[i]];
				// targets[i].transform.localPosition = targets[i].savedPosition;
				// targets[i].transform.localEulerAngles = targets[i].savedEulerAngles;
				// targets[i].transform.localScale = targets[i].savedScale;

				// Mark the scene as dirty to persist changes
				Debug.Log("Saved transform applied and scene marked dirty.");
			// }
		EditorSceneManager.MarkSceneDirty(keys[i].gameObject.scene);
		}

		hasSavedTransform = false; // Reset flag after applying

	}
#endif


}
