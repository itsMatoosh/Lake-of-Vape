using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Synchronizes players ingame.
/// </summary>
public class Player : NetworkBehaviour {

    /// <summary>
    /// Client's storage of players.
    /// Idk what for but it might be useful.
    /// </summary>
	public static List<Player> players;

    public override void OnStartAuthority()
    {
        Debug.Log("Spawned the local player.");

        if (players == null)
        {
            players = new List<Player>();
        }
        players.Add(this);

        base.OnStartAuthority();
    }
}
