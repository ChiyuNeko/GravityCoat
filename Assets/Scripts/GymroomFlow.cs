using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GymroomFlow : MonoBehaviour {

	public enum Phase {
		None,
		Introduction,
		PreDumbbell,
		DumbbellExercise,
		PreBattlerope,
		BattleroptExercise,
		PreDeadlift,
		DeadliftExercise,
		PreKettlebell,
		KettlebellExercise,
		Ending
	}
	[SerializeField] private Phase currentPhase = Phase.None;
	[SerializeField] private List<PhaseContent> phaseContents = new List<PhaseContent>();

	private float waitCounter = 0f;

	private void Update() {
		if (currentPhase == Phase.None) {
			return;
		}
		waitCounter -= Time.deltaTime;
		if (waitCounter <= 0f) {
			ChangeToNextPhase();
		}
	}

	[ContextMenu("StartPhase")]
	public void StartPhase() {
		currentPhase = Phase.None;
		ChangeToNextPhase();
	}

	private void ChangeToNextPhase() {
		//Current Phase Procedure
		if (currentPhase != Phase.None) {
			PhaseContent currentPhaseContent = phaseContents.FirstOrDefault(x => x.Phase == currentPhase);
			if (currentPhaseContent == null) {
				Debug.Log(String.Format("Current PhaseContent of {0} does not exist! Reset to Phase-None", currentPhaseContent.ToString()));
				currentPhase = Phase.None;
				return;
			}
			currentPhaseContent.OnPhaseEndEvent?.Invoke();
			Debug.Log(String.Format("Phase-{0}: OnPhaseEndEvent Invoked at {1}.", currentPhase.ToString(), Time.time));
		}

		//Change Phase
		int nextPhase = (int)currentPhase + 1;
		if (nextPhase >= System.Enum.GetValues(typeof(Phase)).Length) {
			Debug.Log(String.Format("All Phase Ended. Reset to Phase-None"));
			currentPhase = Phase.None;
			return;
		}
		currentPhase = (Phase)nextPhase;

		//Next Phase Procedure
		PhaseContent nextPhaseContent = phaseContents.FirstOrDefault(x => x.Phase == currentPhase);
		if (nextPhaseContent == null) {
			Debug.Log(String.Format("Next PhaseContent of {0} does not exist! Reset to Phase-None", nextPhaseContent.ToString()));
			currentPhase = Phase.None;
			return;
		}
		nextPhaseContent.OnPhaseStartEvent?.Invoke();
		Debug.Log(String.Format("Phase-{0}: OnPhaseStartEvent Invoked at {1}.", currentPhase.ToString(), Time.time));

		waitCounter = nextPhaseContent.WaitDuration;
	}
}

