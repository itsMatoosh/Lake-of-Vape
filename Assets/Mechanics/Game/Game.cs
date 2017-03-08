using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Represents a single game.
/// Present on the server only.
/// </summary>
public class Game {
	/// <summary>
	/// Levels played during this game.
	/// </summary>
	public List<Level> playedLevels;
	/// <summary>
	/// Settings of this game.
	/// </summary>
	public GameSettings settings;


	/// <summary>
	/// Initializes a new instance of the <see cref="Game"/> class.
	/// </summary>
	public Game (GameSettings settings) {
		this.settings = settings;

		InitGame ();
	}

	/// <summary>
	/// Inits the game.
	/// </summary>
	private void InitGame() {
        //Loading the first level.
        playedLevels = new List<Level>();
		LoadNextLevel();

		//Starting a new round.
		StartNextRound();
	}
	/// <summary>
	/// Loads and the next level.
	/// </summary>
	public void LoadNextLevel() 
	{
        Level loadedLevel = LevelManager.LoadLevel(false, null);

        if(loadedLevel != null)
        {
            NetworkOJ.singleton.ServerChangeScene(loadedLevel.name);
            playedLevels.Add(loadedLevel);
        }
        else
        {
            Debug.LogError("Counldn't load the next level!");
        }
	}
	/// <summary>
	/// Starts the next round.
	/// </summary>
	public void StartNextRound() {

	}

	/// <summary>
	/// Finishes the current round.
	/// </summary>
	public void FinishRound(string winner) {

	}
}
