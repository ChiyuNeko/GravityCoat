using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	private void Update() {
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			SceneManager.LoadScene(0);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			SceneManager.LoadScene(1);
		}
		if (Input.GetKeyDown(KeyCode.R)) {
			SceneManager.LoadScene(0);
		}
	}
}
