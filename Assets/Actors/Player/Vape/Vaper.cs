using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Manages the vaping mechanic ingame.
/// </summary>
public class Vaper : NetworkBehaviour {
	[SyncVar (hook="OnVapingStatusChanged")]
	public bool vaping = false;

	/// <summary>
	/// The vape effect.
	/// </summary>
	public ParticleSystem iJustS;
	private float coolDown = 0.25f;

	void Update() {
		if (!isLocalPlayer)
			return;
		coolDown -= Time.deltaTime;

		if (Input.GetButton ("Vape") && !vaping) {
			if (coolDown <= 0) {
				CmdVape (true);
				coolDown = 0.25f;
			}
		} else {
			if (coolDown <= 0) {
				CmdVape (false);
				coolDown = 0.25f;
			}
		}
	}

	[Command]
	public void CmdVape(bool enabled) {
		vaping = enabled;

		if (enabled) {
			iJustS.Play ();
		} else {
			iJustS.Stop ();
		}
	}

	/// <summary>
	/// Rpcs that adjusts the vape effect.
	/// </summary>
	/// <param name="enabled">If set to <c>true</c> enabled.</param>
	public void OnVapingStatusChanged(bool enabled) {
		if (enabled) {
			iJustS.Play ();
		} else {
			iJustS.Stop ();
		}
	}
}
