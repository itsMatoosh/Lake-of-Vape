using System.Collections;
using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class MatchButton : MonoBehaviour {
	public UnityEngine.UI.Text matchName;
	public UnityEngine.UI.Text onlinePlayers;


	/// <summary>
	/// Setup the specified info.
	/// </summary>
	/// <param name="info">Info.</param>
	public void setup(string matchName, int online) {
		this.matchName.text = matchName;
		onlinePlayers.text = online + "/10";
	}
}
