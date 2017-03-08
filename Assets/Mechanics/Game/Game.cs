using System.Collections;
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
	public List<string> playedLevels;
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
		//Starting a new round.
		StartNextRound();
	}
	/// <summary>
	/// Loads and the next level.
	/// </summary>
	public void LoadNextLevel() {

	}
	/// <summary>
	/// Starts the next round.
	/// </summary>
	public void StartNextRound() {

	}
}
