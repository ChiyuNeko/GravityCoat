using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class GymroomInstruction : MonoBehaviour {
	[SerializeField] private List<CanvasGroup> phaseCanvasGroups = new List<CanvasGroup>();
	private int currentPhaseIndex = 0;
	[SerializeField] private float fadeOutDuration = 0.5f;
	[SerializeField] private float fadeInDuration = 0.5f;

	[SerializeField] private TextMeshProUGUI countText;
	public float CountDurationOffset = 1f;

	private void Awake() {
		currentPhaseIndex = phaseCanvasGroups.FindIndex(x => x.gameObject.activeSelf);
		if (currentPhaseIndex < 0) {
			phaseCanvasGroups[0].gameObject.SetActive(true);
			currentPhaseIndex = 0;
		}
	}

	public void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			FadeToNextCanvas(currentPhaseIndex + 1);
		}
	}

	public void FadeToNextCanvas(int index) {
		if (index >= phaseCanvasGroups.Count) {
			Debug.Log(string.Format("Phase Index: {0} is not exist", index));
			return;
		}
		int fadeOutIndex = currentPhaseIndex;
		phaseCanvasGroups[currentPhaseIndex].DOFade(0f, fadeOutDuration)
			.OnComplete(() => {
				phaseCanvasGroups[fadeOutIndex].gameObject.SetActive(false);
				currentPhaseIndex = index;
				phaseCanvasGroups[currentPhaseIndex].gameObject.SetActive(true);
				phaseCanvasGroups[currentPhaseIndex].alpha = 0f;
				phaseCanvasGroups[currentPhaseIndex].DOFade(1f, fadeOutDuration);
			});
	}

	public void CountdownFrom(int maxNumber) {
		if (countText == null) return;
		int countingNumber = maxNumber;
		countText.text = maxNumber.ToString();
		countText.gameObject.SetActive(true);
		for (int i = 0; i < maxNumber; i++) {
			countText.transform.DOScale(1f, CountDurationOffset)
				.SetDelay(CountDurationOffset * i)
				.OnComplete(() => {
					countingNumber--;
					countText.text = countingNumber.ToString();
					if (countingNumber == 0) {
						countText.gameObject.SetActive(false);
					}
				});
		}

	}
}
