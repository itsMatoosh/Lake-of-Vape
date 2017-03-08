using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkOJ : NetworkManager {

	/// <summary>
	/// Called when the server starts.
	/// </summary>
	public override void OnStartServer ()
	{
		base.OnStartServer ();

		//Starting the game.
		GetComponent<GameManager>().StartGame(CreateMatchManager.currentGameSettings);
	}

	public override void OnClientConnect (NetworkConnection conn)
	{
		base.OnClientConnect (conn);

		//Loading the level?

	}
}
