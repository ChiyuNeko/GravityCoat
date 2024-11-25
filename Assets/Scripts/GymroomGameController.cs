using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GymroomGameController : MonoBehaviour {

	[SerializeField] private TextMeshProUGUI scoreText;
	[SerializeField] private int score = 0;
	public int Score {
		get { return score; }
		set {
			score = value;
			if (scoreText != null) {
				scoreText.text = score.ToString();
			}
		}
	}
	public List<GymroomGameTriggerSet> triggerSets = new List<GymroomGameTriggerSet>();


	public void ResetGame() {
		Score = 0;
		for (int i = 0; i < triggerSets.Count; i++) {
			triggerSets[i].Reset();
		}
	}

	public void StartGame() {
		for (int i = 0; i < triggerSets.Count; i++) {
			triggerSets[i].UpdateTriggerSet();
			triggerSets[i].OnScoredAction += AddScore;
		}
	}

	public void EndGame() {
		for (int i = 0; i < triggerSets.Count; i++) {
			triggerSets[i].TurnOff();
			triggerSets[i].OnScoredAction = null;
		}
	}

	public void AddScore() {
		Score++;
	}
}
