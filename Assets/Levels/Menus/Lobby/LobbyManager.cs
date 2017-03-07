using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking.Match;

public class LobbyManager : MonoBehaviour {
	/// <summary>
	/// The instance.
	/// </summary>
	public static LobbyManager instance;

	/// <summary>
	/// The match button prefab.
	/// </summary>
	public Object matchButtonPrefab;
	/// <summary>
	/// The match button holder.
	/// </summary>
	public Transform matchButtonHolder;

	void Start() {
		//Listing all the active games.
		//TODO: Game name filtering.
		instance = this;
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
	/// <summary>
	/// Adds the match button.
	/// </summary>
	/// <param name="info">Info.</param>
	public void AddMatchButton(MatchInfo info) {
		Instantiate (matchButtonPrefab, matchButtonHolder);
	}
}
