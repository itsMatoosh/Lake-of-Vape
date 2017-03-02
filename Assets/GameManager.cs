using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	/// <summary>
	/// Currently running game.
	/// </summary>
	public static Game currentGame;

	/// <summary>
	/// Starts the game.
	/// </summary>
	public void StartGame() {
		//Creating the game instance.
		currentGame = new Game();
	}
}
