using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System.Collections.Generic;

/// <summary>
/// Matchmaker
/// </summary>
public class MatchMaker : MonoBehaviour
{
	/// <summary>
	/// The instance.
	/// </summary>
	public static MatchMaker instance;

	/// <summary>
	/// The lan discovery.
	/// </summary>
	public LANDiscovery lanDiscovery;

	/// <summary>
	/// Starts the MatchMaker.
	/// </summary>
	void Start()
	{
		lanDiscovery.Initialize ();
		instance = this;
		NetworkManager.singleton.StartMatchMaker();
	}

	/// <summary>
	/// Creates a new match.
	/// </summary>
	/// <param name="matchName">Match name.</param>
	public void CreateInternetMatch(GameSettings settings)
	{
		if (true || Application.internetReachability == NetworkReachability.NotReachable) {
			//NetworkServer.Listen (42069);
			if (lanDiscovery.isClient || lanDiscovery.isServer) {
				lanDiscovery.StopBroadcast ();
			}

            NetworkManager.singleton.StartHost();
            //Setting the ip on landiscovery.
            lanDiscovery.broadcastData = settings.gameName + "`" + NetworkServer.listenPort + "`" + "0/" + settings.maxPlayerCount;
            lanDiscovery.StartAsServer();
            Debug.Log("Server connection config: ");
            foreach (QosType channel in NetworkManager.singleton.channels)
            {
                Debug.Log(channel);
            }

        } else {
			NetworkManager.singleton.matchMaker.CreateMatch(settings.gameName, (uint)settings.maxPlayerCount, true, "", "", "", 0, 0, OnInternetMatchCreate);
		}   
	}

    /// <summary>
    /// Event called when the match is created.
    /// </summary>
    /// <param name="success">If set to <c>true</c> success.</param>
    /// <param name="extendedInfo">Extended info.</param>
    /// <param name="matchInfo">Match info.</param>
    private void OnInternetMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
	{
		if (success)
		{
			Debug.Log("Created match successfully! xd");

			MatchInfo hostInfo = matchInfo;
			//NetworkServer.Listen(hostInfo, 42069);

			if (lanDiscovery.isClient || lanDiscovery.isServer) {
				lanDiscovery.StopBroadcast ();
			}
			lanDiscovery.StartAsServer ();
            NetworkManager.singleton.StartHost(hostInfo);
            //Setting the ip on landiscovery.
            lanDiscovery.broadcastData = NetworkServer.listenPort + "";
            Debug.LogError(lanDiscovery.broadcastData);
        }
		else
		{
			Debug.LogError("Create match failed! " + extendedInfo);
		}
	}

	/// <summary>
	/// Searches for internet matches with name.
	/// </summary>
	/// <param name="matchName">Match name.</param>
	public void FindInternetMatch(string matchName)
	{
		LobbyManager.instance.ClearMatchList();
		NetworkManager.singleton.matchMaker.ListMatches(0, 10, matchName, true, 0, 0, OnInternetMatchList);
	}
	public void FindLanMatches() {
		LobbyManager.instance.ClearLanMatchList ();

		if (!lanDiscovery.isClient) {
			lanDiscovery.StartAsClient ();
		}
	}

	/// <summary>
	/// Event called when the matches have been found.
	/// </summary>
	/// <param name="success">If set to <c>true</c> success.</param>
	/// <param name="extendedInfo">Extended info.</param>
	/// <param name="matches">Matches.</param>
	private void OnInternetMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
	{
		if (success)
		{
			if (matches.Count != 0)
			{
				foreach(MatchInfoSnapshot match in matches)
                {
                    LobbyManager.instance.AddMatchButton(match);
                }
			}
			else
			{
				Debug.Log("No matches in requested room!");
			}
		}
		else
		{
			Debug.LogError("Couldn't connect to match maker! " + extendedInfo);
		}
	}

	/// <summary>
	/// Called when the match has been joined.
	/// </summary>
	/// <param name="success">If set to <c>true</c> success.</param>
	/// <param name="extendedInfo">Extended info.</param>
	/// <param name="matchInfo">Match info.</param>
	internal void OnJoinInternetMatch(bool success, string extendedInfo, MatchInfo matchInfo)
	{
		if (success)
		{
			//Debug.Log("Able to join a match");

			MatchInfo hostInfo = matchInfo;
			NetworkManager.singleton.StartClient(hostInfo);
			lanDiscovery.StopBroadcast ();
		}
		else
		{
			Debug.LogError("Join match failed! " + extendedInfo);
		}
	}
	/// <summary>
	/// Called when a lan match has been discovered.
	/// </summary>
	/// <param name="address">Address.</param>
	/// <param name="info">Info.</param>
	internal void OnLANMatchDiscovered(string address, string info) {
		LobbyManager.instance.AddLanMatchButton(address, info);
	}
    /// <summary>
    /// Joins a lan match.
    /// </summary>
    /// <param name="address"></param>
    /// <param name="port"></param>
    internal void JoinLanMatch(string address, int port) {
        Debug.Log("Connecting to a LAN match: " + address + ":" + port);
        NetworkManager.singleton.networkPort = port;
        NetworkManager.singleton.networkAddress = address;

        NetworkManager.singleton.StartClient();
        lanDiscovery.StopBroadcast();
    }
    /// <summary>
    /// Joins an Intenet match.
    /// </summary>
    /// <param name="matchInfo"></param>
    internal void JoinInternetMatch(MatchInfoSnapshot matchInfo)
    {
        Debug.Log("Connecting to an Internet match: " + matchInfo.name);
        NetworkManager.singleton.matchMaker.JoinMatch(matchInfo.networkId, "", "", "", 0, 0, instance.OnJoinInternetMatch);
    }
}