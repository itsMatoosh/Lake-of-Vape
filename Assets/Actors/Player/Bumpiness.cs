using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Makes players bump off of each other.
/// </summary>
public class Bumpiness : NetworkBehaviour {
	/// <summary>
	/// Rigidbody of this player.
	/// </summary>
	public Rigidbody2D body;

	/// <summary>
	/// Called when the player collides with an object.
	/// </summary>
	/// <param name="coll">Coll.</param>
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			Rigidbody2D other = coll.gameObject.GetComponent<Rigidbody2D> ();
			Vector2 bumpForce = other.position - this.GetComponent<Rigidbody2D> ().position;

			if (isServer) {
				other.AddForce (bumpForce, ForceMode2D.Impulse);
			}
			body.AddForce (-bumpForce, ForceMode2D.Impulse);
		}
	}
}
