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
	public string address;
    public int port;
	public string info;


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
	/// Setup the specified address and info.
	/// </summary>
	/// <param name="address">Address.</param>
	/// <param name="info">Info.</param>
	public void Setup(string address, string info) {
		this.address = address;
		this.info = info;

        string[] matchInfo = info.Split('`');
        matchName.text = matchInfo[0];
        onlinePlayers.text = matchInfo[2];
        port = int.Parse(matchInfo[1]);
	}
    /// <summary>
    /// Connects to the server represented by the button.
    /// </summary>
    public void Connect()
    {
		if (matchInfo != null) {
            MatchMaker.instance.JoinInternetMatch(matchInfo);
		}
		if (address != null) {
            MatchMaker.instance.JoinLanMatch(address, port);
        }
		GetComponent<Button> ().interactable = false;
    }
}
