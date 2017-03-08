using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class MatchButton : MonoBehaviour {
	public Text matchName;
	public Text onlinePlayers;
    /// <summary>
    /// Info about the match represented by this button.
    /// </summary>
    public MatchInfoSnapshot matchInfo;


	/// <summary>
	/// Setup the specified info.
	/// </summary>
	/// <param name="info">Info.</param>
	public void Setup(MatchInfoSnapshot info) {
        matchInfo = info;

        matchName.text = info.name;
		onlinePlayers.text = info.currentSize + "/" + info.maxSize;
	}
    /// <summary>
    /// Connects to the server represented by the button.
    /// </summary>
    public void Connect()
    {
        NetworkManager.singleton.matchMaker.JoinMatch(matchInfo.networkId, "", "", "", 0, 0, MatchMaker.instance.OnJoinInternetMatch);
    }
}
