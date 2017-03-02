using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Settings of the game.
/// </summary>
public class GameSettings {
	/// <summary>
	/// Number of players.
	/// </summary>
	public int playerCount;

	/// <summary>
	/// Initializes a new instance of the <see cref="GameSettings"/> class.
	/// </summary>
	/// <param name="playerCount">Player count.</param>
	public GameSettings(int playerCount) {
		this.playerCount = playerCount;
	}
}
