using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[NetworkSettings(channel=1,sendInterval=0.02f)]
public class PlayerMovement : NetworkBehaviour {

	/// <summary>
	/// Rigidbody of this player.
	/// </summary>
	public Rigidbody2D body;

	/// <summary>
	/// The speed of the player.
	/// </summary>
	public float speed;

	/// <summary>
	/// Input of the player.
	/// </summary>
	public struct PlayerInput
	{
		/// <summary>
		/// Cache of X input.
		/// </summary>
		public float inputXCache;
		/// <summary>
		/// Cache of Y input.
		/// </summary>
		public float inputYCache;
		/// <summary>
		/// Player's rotation.
		/// </summary>
		public float rotation;

		/// <summary>
		/// Timestamp of the input.
		/// </summary>
		public double timeStamp;
	}
	/// <summary>
	/// Used to verify the position predicted by client.
	/// </summary>
	public struct Result {
		/// <summary>
		/// Server side player x position.
		/// </summary>
		public float posX;
		/// <summary>
		/// Server side player y position.
		/// </summary>
		public float posY;
		/// <summary>
		/// Server side rotation of the player.
		/// </summary>
		public float rotation;

		/// <summary>
		/// Timestamp of the results.
		/// </summary>
		public double timeStamp;
	}
	/// <summary>
	/// Cache of the input before fixed update.
	/// Client side
	/// </summary>
	public PlayerInput clientInputCache = new PlayerInput();
	/// <summary>
	/// The last client input.
	/// </summary>
	public PlayerInput lastClientInput = new PlayerInput ();
	/// <summary>
	/// Cache of the input before simulation.
	/// Server side.
	/// </summary>
	public PlayerInput serverInputCache = new PlayerInput();
    /// <summary>
    /// Cache of the server result.
    /// </summary>
    public Result resultCache;
    [SyncVar(hook = "OnResultReceive")]
    public Result currentResult;
	/// <summary>
	/// Whether the player can move.
	/// </summary>
	public bool canMove = true;
	/// <summary>
	/// Result cache of the client.
	/// </summary>
	public List<Result> clientResults = new List<Result>();

	public override void OnStartClient ()
	{
		if (!isLocalPlayer && !isServer) {
			body.isKinematic = false;
		}
	}

	/// <summary>
	/// Calculating the movement.
	/// </summary>
	private void FixedUpdate()
	{
		if (isServer) {
			//Sending predictions for the last fixedupodate simulations.
			resultCache.posX = transform.position.x;
			resultCache.posY = transform.position.y;
            currentResult = resultCache;


			//Simulating on...
			if (canMove) {
				body.velocity = new Vector2 (serverInputCache.inputXCache * speed,
					serverInputCache.inputYCache * speed);
				transform.rotation = Quaternion.AngleAxis (serverInputCache.rotation, Vector3.forward); 
			}

			//Resetting the input and waiting for input from player.
			resultCache = new Result
			{
				timeStamp = serverInputCache.timeStamp,
				rotation = serverInputCache.rotation
			};
			//serverInputCache = new PlayerInput();
		}
		if (isLocalPlayer)
		{
			//Adding client results to the cache.
			clientResults.Add(new Result
				{
					posX = transform.position.x,
					posY = transform.position.y,
					rotation = lastClientInput.rotation,
					timeStamp = lastClientInput.timeStamp
				});
			if(clientResults.Count > 15)
			{
				clientResults.RemoveAt(0);
			}

			//Sending input update.
			clientInputCache.timeStamp = Time.timeSinceLevelLoad;
			CmdSendInput(clientInputCache);
			lastClientInput.timeStamp = clientInputCache.timeStamp;
			lastClientInput.rotation = clientInputCache.rotation;

			//Simulating locally.
			if (canMove) {
				body.velocity = new Vector2(clientInputCache.inputXCache * speed,
					clientInputCache.inputYCache * speed);
			}

			//Resetting the input.
			clientInputCache = new PlayerInput();
		}
	}

	/// <summary>
	/// Getting input.
	/// </summary>
	private void Update()
	{
		if (!isLocalPlayer) return;
		clientInputCache.inputXCache = Input.GetAxis("Horizontal");
		clientInputCache.inputYCache = Input.GetAxis("Vertical");

		AimAtCamera ();
	}
	/// <summary>
	/// Aims at the camera.
	/// </summary>
	private void AimAtCamera() {
		if (!canMove)
			return;
		var pos = CameraFollow.thisCamera.WorldToScreenPoint(transform.position);
		var dir = Input.mousePosition - pos;
		clientInputCache.rotation = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(clientInputCache.rotation, Vector3.forward); 
	}

	/// <summary>
	/// Sends input info to the server.
	/// </summary>
	/// <param name="input"></param>
	[Command]
	public void CmdSendInput(PlayerInput input)
	{
		//Called on the server.
		serverInputCache = input;
	}
	/// <summary>
	/// Called when the server sends the simulation results.
	/// </summary>
	/// <param name="result"></param>
	public void OnResultReceive(Result result)
	{
		if (!isLocalPlayer) {
			if (isServer) {
				return;
			}
			body.MovePosition (new Vector2(result.posX, result.posY));
			body.MoveRotation(result.rotation); 
			return;
		}

		if (!isServer) {
            //Checking if the results match between client and the server.
            Result matchingClientResult = new Result();
			for (int i = 0; i < clientResults.Count; i++) {
                Debug.Log("Client result # " + i);
                Debug.Log(" x: " + clientResults[i].posX);
                Debug.Log(" time: " + clientResults[i].timeStamp);
				if(clientResults[i].posX == result.posX)
				{
                    Debug.Log("[CHOSEN]");
					matchingClientResult = clientResults[i - 3];
				}
			}

			/*if(matchingClientResult == null)
			{
				Debug.LogError("No matching client simulation samples! ");
				return;
			}*/

			if(result.posX != matchingClientResult.posX || result.posY != matchingClientResult.posY)
			{
				Debug.LogError("SERVER-CLIENT MISMATCH!!!");
				Debug.Log("Server: " + result.posX + "/ " + result.timeStamp + " Client: " + matchingClientResult.posX + "/" + matchingClientResult.timeStamp);
				transform.position.Set (result.posX, result.posY, 1);
				body.MovePosition (new Vector2(result.posX, result.posY));
			}
		}
	}
}
