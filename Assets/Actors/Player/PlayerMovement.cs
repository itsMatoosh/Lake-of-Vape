using System.Collections;
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
    /// Calculating the movement.
    /// </summary>
    private void FixedUpdate()
    {
        if (!localPlayerAuthority) return;

        body.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * speed, 0.8f),
                                                Mathf.Lerp(0, Input.GetAxis("Vertical") * speed, 0.8f));
    }
}
