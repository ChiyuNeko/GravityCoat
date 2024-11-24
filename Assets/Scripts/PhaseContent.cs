using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhaseContent : MonoBehaviour {
	public GymroomFlow.Phase Phase;
	public GymroomFlow.Phase NextPhase;
	public UnityEvent OnPhaseStartEvent;
	public float WaitDuration = 5f;
	public UnityEvent OnPhaseEndEvent;
}
