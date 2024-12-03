using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.Events;
using fofulab;

public class SceneChanger : MonoBehaviour {
	public static SceneChanger Instance;
	[SerializeField] public CanvasGroup BlackOverlayCanvasGroup;
	[SerializeField] private float fadeInDuration = 1f;
	[SerializeField] private float fadeOutDuration = 1f;
	private bool isSceneChanging = false;
    [SerializeField] private HapticMaker hm;
	public UnityEvent OnPPressed;
	public UnityEvent On0Pressed;
	

	private void Awake() {
		if (SceneChanger.Instance == null) {
			DontDestroyOnLoad(this.gameObject);
			Instance = this;
		} else {
			SceneChanger.Instance.BlackOverlayCanvasGroup.GetComponent<Canvas>().worldCamera = Camera.main;
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

		if (Input.GetKeyDown(KeyCode.P)) {
			hm.SetPWM(OutputPin.P7, 0);
            hm.SetPWM(OutputPin.P9, 0);
			hm.SetPWM(OutputPin.P8, 0);
            hm.SetPWM(OutputPin.P11, 0);
			OnPPressed?.Invoke();
		}

		if (Input.GetKeyDown(KeyCode.Alpha0)) {
			On0Pressed?.Invoke();
			hm.SetPWM(OutputPin.P8, 150);
            hm.SetPWM(OutputPin.P11, 150);
		}
	}

	public void ChangeToScene(int sceneIndex) {
		if (isSceneChanging == true) return;
		isSceneChanging = true;
		BlackOverlayCanvasGroup.gameObject.SetActive(true);
		BlackOverlayCanvasGroup.alpha = 0f;
		BlackOverlayCanvasGroup.DOFade(1f, fadeInDuration)
			.OnComplete(() => {
				SceneManager.LoadScene(sceneIndex);
			});
		SceneManager.sceneLoaded += FadeOutBlackOverlay;
	}

	private void FadeOutBlackOverlay(Scene scene, LoadSceneMode mode) {
		isSceneChanging = false;
		BlackOverlayCanvasGroup.DOFade(0f, fadeOutDuration)
			.OnComplete(() => {
				BlackOverlayCanvasGroup.gameObject.SetActive(false);
			});
		SceneManager.sceneLoaded -= FadeOutBlackOverlay;
	}
}
