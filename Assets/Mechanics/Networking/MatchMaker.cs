using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System.Collections.Generic;

public class MatchMaker : MonoBehaviour
{
	/// <summary>
	/// The instance.
	/// </summary>
	public static MatchMaker instance;

	/// <summary>
	/// Starts the MatchMaker.
	/// </summary>
	void Start()
	{
		instance = this;
		NetworkManager.singleton.StartMatchMaker();
	}

	/// <summary>
	/// Creates a new match.
	/// </summary>
	/// <param name="matchName">Match name.</param>
	public void CreateInternetMatch(GameSettings settings)
	{
        NetworkManager.singleton.matchMaker.CreateMatch(settings.gameName, (uint)settings.maxPlayerCount, true, "", "", "", 0, 0, OnInternetMatchCreate);
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
			NetworkServer.Listen(hostInfo, 42069);

            NetworkManager.singleton.StartHost(hostInfo);
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
		NetworkManager.singleton.matchMaker.ListMatches(0, 10, matchName, true, 0, 0, OnInternetMatchList);
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
				//Debug.Log("A list of matches was returned");

				//join the last server (just in case there are two...)
				NetworkManager.singleton.matchMaker.JoinMatch(matches[matches.Count - 1].networkId, "", "", "", 0, 0, OnJoinInternetMatch);
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
	private void OnJoinInternetMatch(bool success, string extendedInfo, MatchInfo matchInfo)
	{
		if (success)
		{
			//Debug.Log("Able to join a match");

			MatchInfo hostInfo = matchInfo;
			NetworkManager.singleton.StartClient(hostInfo);
		}
		else
		{
			Debug.LogError("Join match failed! " + extendedInfo);
		}
	}
}