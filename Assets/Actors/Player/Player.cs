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

    /// <summary>
    /// Prefab of the following camera.
    /// </summary>
    [Tooltip("Prefab of the following camera.")]
    public Object cameraFollowPrefab;

    /// <summary>
    /// Name of the owner of this player.
    /// </summary>
    [SyncVar]
    public string username;

    /// <summary>
    /// Called when the player is spawned on the client controling it.
    /// </summary>
    public override void OnStartLocalPlayer()
    {
        Debug.Log("Spawned the local player.");

        //Initializing.
        InitPlayer();

        //Spawning camera.
        SpawnCamera();

        base.OnStartLocalPlayer();
    }

    /// <summary>
    /// Initializes the player.
    /// </summary>
    private void InitPlayer()
    {
        //Caching players.
        if (players == null)
        {
            players = new List<Player>();
        }
        players.Add(this);

        //Setting the username.
        CmdSetUsername(UserIdentity.name);
    }

    /// <summary>
    /// Sets the username of the player.
    /// </summary>
    /// <param name="username"></param>
    [Command]
    public void CmdSetUsername(string username)
    {
        this.username = username;
    }
    /// <summary>
    /// Spawns the camera to follow the player in the game.
    /// </summary>
    private void SpawnCamera()
    {
        ((GameObject)Instantiate(cameraFollowPrefab, new Vector3(0,0,-1), Quaternion.identity)).GetComponent<CameraFollow>().target = this.transform;
    }
}
