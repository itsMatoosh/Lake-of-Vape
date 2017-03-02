using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
	/// <summary>
	/// List of all the available normal levels.
	/// </summary>
	public static string[] normalLevels;
	/// <summary>
	/// List of all the available tie breaker levels.
	/// </summary>
	public static string[] tieBreakerLevels;

	/// <summary>
	/// Loads a random level.
	/// </summary>
	/// <param name="tieBreaker">If set to <c>true</c> will load a tiebreaker level.</param>
	/// <param name="blackList">Levels not to load.</param>
	public static void LoadLevel(bool tieBreaker, List<string> blackList) {
		string chosenLevel = "";

		while (chosenLevel == "") {
			if (normalLevels.Length == 0 || tieBreakerLevels.Length == 0) {
				Debug.LogError ("Not enough levels to play the game! :(");
				break;
			}

			if (tieBreaker) {
				chosenLevel = tieBreakerLevels[Random.Range(0, tieBreakerLevels.Length - 1)];
			} else {
				chosenLevel = normalLevels[Random.Range(0, normalLevels.Length - 1)];
			}
			if (blackList.Contains (chosenLevel)) {
				chosenLevel = "";
			}
		}

		if (chosenLevel != "") {
			SceneManager.LoadSceneAsync (chosenLevel, LoadSceneMode.Additive);
		}
	}
}
