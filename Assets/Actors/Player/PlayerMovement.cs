using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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
    public class PlayerInput
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
        /// Timestamp of the input.
        /// </summary>
        public double timeStamp;
    }
    /// <summary>
    /// Used to verify the position predicted by client.
    /// </summary>
    public class Result {
        /// <summary>
        /// Server side player x position.
        /// </summary>
        public float posX;
        /// <summary>
        /// Server side player y position.
        /// </summary>
        public float posY;

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
    public Result resultCache = null;
    /// <summary>
    /// Result cache of the client.
    /// </summary>
    public List<Result> clientResults = new List<Result>();

    /// <summary>
    /// Calculating the movement.
    /// </summary>
    private void FixedUpdate()
    {
        if (isServer) {
            //Sending predictions for the last fixedupodate simulations.
            if(resultCache != null)
            {
                resultCache.posX = transform.position.x;
                resultCache.posY = transform.position.y;
                RpcOnResultsReceived(resultCache);
            }

            //Simulating on...
			body.velocity = new Vector2(serverInputCache.inputXCache * speed,
				serverInputCache.inputYCache * speed);

            //Resetting the input and waiting for input from player.
            resultCache = new Result
            {
				timeStamp = serverInputCache.timeStamp
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

            //Simulating locally.
			body.velocity = new Vector2(clientInputCache.inputXCache * speed,
				clientInputCache.inputYCache * speed);

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
        if(Input.GetAxis("Horizontal") != 0)
        {
			clientInputCache.inputXCache = Input.GetAxis("Horizontal");
        }

        if(Input.GetAxis("Vertical") != 0)
        {
			clientInputCache.inputYCache = Input.GetAxis("Vertical");
        }
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
    [ClientRpc]
    public void RpcOnResultsReceived(Result result)
    {
		if (!isLocalPlayer) {
			GetComponent<Transform>().position.Set (result.posX, result.posY, 1);
			return;
		}

        //Checking if the results match between client and the server.
        Result matchingClientResult = null;
		for (int i = 0; i < clientResults.Count; i++) {
			if(clientResults[i].timeStamp == result.timeStamp)
			{
				matchingClientResult = clientResults[i+1];
			}
		}
	
        if(matchingClientResult == null)
        {
			Debug.LogError("No matching client simulation samples! ");
            return;
        }

        if(result.posX != matchingClientResult.posX || result.posY != matchingClientResult.posY)
        {
            Debug.LogError("SERVER-CLIENT MISMATCH!!!");
            Debug.Log("Server: " + result.posX + "/ " + result.timeStamp + " Client: " + matchingClientResult.posX + "/" + matchingClientResult.timeStamp);
			GetComponent<Transform>().position.Set (result.posX, result.posY, 1);
        }
    }
}
