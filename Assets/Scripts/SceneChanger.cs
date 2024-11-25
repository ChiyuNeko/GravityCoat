using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneChanger : MonoBehaviour {
	public static SceneChanger Instance;
	[SerializeField] private CanvasGroup blackOverlayCanvasGroup;
	[SerializeField] private float fadeInDuration = 1f;
	[SerializeField] private float fadeOutDuration = 1f;
	private bool isSceneChanging = false;

	private void Awake() {
		if (SceneChanger.Instance == null) {
			DontDestroyOnLoad(this.gameObject);
			Instance = this;
		} else {
			Destroy(this.gameObject);
		}
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			ChangeToScene(0);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			ChangeToScene(1);
		}
		if (Input.GetKeyDown(KeyCode.R)) {
			ChangeToScene(0);
		}
	}

	public void ChangeToScene(int sceneIndex) {
		if (isSceneChanging == true) return;
		isSceneChanging = true;
		blackOverlayCanvasGroup.gameObject.SetActive(true);
		blackOverlayCanvasGroup.alpha = 0f;
		blackOverlayCanvasGroup.DOFade(1f, fadeInDuration)
			.OnComplete(() => {
				SceneManager.LoadScene(sceneIndex);
			});
		SceneManager.sceneLoaded += FadeOutBlackOverlay;
	}

	private void FadeOutBlackOverlay(Scene scene, LoadSceneMode mode) {
		isSceneChanging = false;
		blackOverlayCanvasGroup.DOFade(0f, fadeOutDuration)
			.OnComplete(() => {
				blackOverlayCanvasGroup.gameObject.SetActive(false);
			});
		SceneManager.sceneLoaded -= FadeOutBlackOverlay;
	}
}
