using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public static List<Player> players;
		
	// Use this for initialization
	void Start () {
		if (players == null) {
			players = new List<Player> ();
		}
		players.Add (this);


	}
}
