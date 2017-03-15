using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Represents a water object.
/// </summary>
public class Water : NetworkBehaviour {
	/// <summary>
	/// Prefab of the water object.
	/// </summary>
	public Object waterPrefab;
	/// <summary>
	/// The cool down of water spread.
	/// </summary>
	public static float coolDown = 5f;
	/// <summary>
	/// Can the water spread anymore?
	/// </summary>
	public bool spreadable = true;
	/// <summary>
	/// Whether the water is a supreme leader.
	/// </summary>
	public bool isTheSupremeLeader = false;
	/// <summary>
	/// Collider of this water.
	/// </summary>
	public Collider2D thisCollider;

	/// <summary>
	/// One server tick.
	/// </summary>
	void FixedUpdate() {
		if (isTheSupremeLeader) {
			coolDown -= Time.fixedDeltaTime;
		}
		if (!spreadable)
			return;

		if (isServer && coolDown <= 0) {
			for (int g = Random.Range (0, 2); g >= 0; g--) {
				Spread ();
			}
		}
	}

	/// <summary>
	/// Spreads the water.
	/// </summary>
	void Spread() {
		int side = Random.Range (
			0, 5);

		Vector2 spreadOffset = new Vector2();

		switch (side) {
		case 4:   
			break;
		case (int)BlockSide.NORTH:
			spreadOffset.y = 1;
			break;
		case (int)BlockSide.SOUTH:
			spreadOffset.y = -1;
			break;
		case (int)BlockSide.EAST:
			spreadOffset.x = 1; 
			break;
		case (int)BlockSide.WEST:
			spreadOffset.x = -1;
			break;
		}

		Vector3 position = new Vector3 (this.transform.position.x + spreadOffset.x, this.transform.position.y + spreadOffset.y, this.transform.position.z);
		RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, position - transform.position);
		bool canSpawn = true;
		/*foreach (RaycastHit2D hit in hits) {
			if (hit.collider != thisCollider && hit.transform.tag == "Water" || hit.transform.tag == "Terrain") {
				canSpawn = false;
			}
		}*/

		if(canSpawn && (spreadOffset != Vector2.zero)) {
			GameObject childWater = (GameObject)Instantiate(waterPrefab, new Vector3(this.transform.position.x + spreadOffset.x, this.transform.position.y + spreadOffset.y, this.transform.position.z), Quaternion.identity, this.transform.parent);
			NetworkServer.Spawn (childWater);
		}

		coolDown = 10f;
		spreadable = false;
	}
	void OnTriggerEnter2D(Collider2D coll) {
		if (isServer) {
			if (coll.gameObject.tag == "Player") {
				coll.gameObject.GetComponent<Player> ().Kill ();
			}
		}
	}
}