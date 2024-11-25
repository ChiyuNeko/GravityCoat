using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GymroomGameTrigger : MonoBehaviour {

	public MeshRenderer MeshRenderer;
	public Action OnTriggerEnterAction;

	private void OnTriggerEnter(Collider other) {
		if (other.tag == "GymroomTrigger") {
			OnTriggerEnterAction?.Invoke();
		}
	}
}
