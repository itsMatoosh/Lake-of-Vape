using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	/// <summary>
	/// Gos to lobby.
	/// </summary>
	public void GoToLobby() {
		SceneManager.LoadSceneAsync ("Lobby");
	}
}
