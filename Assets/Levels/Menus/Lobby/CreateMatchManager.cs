using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMatchManager : MonoBehaviour {
	/// <summary>
	/// The current game settings.
	/// </summary>
	public static GameSettings currentGameSettings;

	void Start() {
		currentGameSettings = new GameSettings ();
	}

	/// <summary>
	/// Sets the max players amount.
	/// </summary>
	/// <param name="maxPlayers">Max players.</param>
	public void SetMaxPlayers(int maxPlayers) {
		currentGameSettings.maxPlayerCount = maxPlayers;
	}
}
