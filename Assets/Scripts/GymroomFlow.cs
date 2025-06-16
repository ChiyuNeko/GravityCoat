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

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			StartPhase();
		}
		if (currentPhase == Phase.None)
		{
			return;
		}
		waitCounter -= Time.deltaTime;
		if (waitCounter <= 0f)
		{
			ChangeToNextPhase();
		}
		if (Input.GetKeyDown(KeyCode.Q))
		{
			currentPhase = Phase.BattleroptExercise;

		}
	}

	[ContextMenu("StartPhase")]
	public void StartPhase() {
		currentPhase = Phase.None;
		ChangeToNextPhase();
	}

	private void ChangeToNextPhase() {
		Debug.Log("Change");
		//Current Phase Procedure
		PhaseContent currentPhaseContent = phaseContents.FirstOrDefault(x => x.Phase == currentPhase);
		if (currentPhase != Phase.None) {
			if (currentPhaseContent == null) {
				Debug.Log(String.Format("Current PhaseContent of {0} does not exist! Reset to Phase-None", currentPhaseContent.ToString()));
				currentPhase = Phase.None;
				return;
			}
			currentPhaseContent.OnPhaseEndEvent?.Invoke();
			Debug.Log(String.Format("Phase-{0}: OnPhaseEndEvent Invoked at {1}.", currentPhase.ToString(), Time.time));
		}

		//Change Phase
		if (currentPhase == Phase.None) {
			currentPhase = Phase.Introduction;
		} else {
			if (currentPhaseContent.NextPhase != Phase.None) {
				currentPhase = currentPhaseContent.NextPhase;
			} else {
				int nextPhase = (int)currentPhase + 1;
				if (nextPhase >= System.Enum.GetValues(typeof(Phase)).Length) {
					Debug.Log(String.Format("All Phase Ended. Reset to Phase-None"));
					currentPhase = Phase.None;
					return;
				}
				currentPhase = (Phase)nextPhase;
			}
		}
		Debug.Log(currentPhase);

		//Next Phase Procedure
		PhaseContent nextPhaseContent = phaseContents.FirstOrDefault(x => x.Phase == currentPhase);
		Debug.Log(nextPhaseContent);
		if (nextPhaseContent == null) {
			Debug.Log(String.Format("Next PhaseContent of {0} does not exist! Reset to Phase-None", nextPhaseContent.ToString()));
			currentPhase = Phase.None;
			return;
		}
		waitCounter = nextPhaseContent.WaitDuration;
		Debug.Log("hihi");
		nextPhaseContent.OnPhaseStartEvent?.Invoke();
		Debug.Log(String.Format("Phase-{0}: OnPhaseStartEvent Invoked at {1}.", currentPhase.ToString(), Time.time));

		Debug.Log(waitCounter);
	}
}

