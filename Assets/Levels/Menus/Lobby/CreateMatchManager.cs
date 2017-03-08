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
    /// <summary>
    /// Sets the name of the game.
    /// </summary>
    /// <param name="name"></param>
    public void SetGameName(string name)
    {
        currentGameSettings.gameName = name;
    }
    /// <summary>
    /// Starts the game.
    /// </summary>
    public void StartGame()
    {
        MatchMaker.instance.CreateInternetMatch(currentGameSettings);
    }
}
