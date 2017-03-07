using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour {
	void Start() {
		//Listing all the active games.
		//TODO: Game name filtering.
		RefreshMatches();
	}
	/// <summary>
	/// Refreshs the matches.
	/// </summary>
	public void RefreshMatches() {
		MatchMaker.instance.FindInternetMatch("");
	}
	/// <summary>
	/// Changes to the create match menu.
	/// </summary>
	public void CreateMatch() {
		SceneManager.LoadScene ("CreateMatch");
	}
}
