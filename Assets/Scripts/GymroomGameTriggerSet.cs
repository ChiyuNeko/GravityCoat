using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GymroomGameTriggerSet : MonoBehaviour {
	[SerializeField] private Material onMaterial;
	[SerializeField] private Material offMaterial;
	private int index = 0;
	public List<GymroomGameTrigger> triggers = new List<GymroomGameTrigger>();

	public Action OnScoredAction;

	public void Reset() {
		index = 0;
	}

	public void TurnOff() {
		index = 0;
		for (int i = 0; i < triggers.Count; i++) {
			triggers[i].gameObject.SetActive(false);
		}
	}

	public void UpdateTriggerSet() {
		for (int i = 0; i < triggers.Count; i++) {
			triggers[i].gameObject.SetActive(true);
			if (i == index) {
				triggers[i].MeshRenderer.sharedMaterial = onMaterial;
				triggers[i].OnTriggerEnterAction += TriggerHit;
			} else {
				triggers[i].MeshRenderer.sharedMaterial = offMaterial;
			}

		}
	}

	private void TriggerHit() {
		index++;
		if (index >= triggers.Count) {
			index = 0;
			OnScoredAction?.Invoke();
		}
		for (int i = 0; i < triggers.Count; i++) {
			triggers[i].OnTriggerEnterAction = null;
		}
		UpdateTriggerSet();
	}
}
