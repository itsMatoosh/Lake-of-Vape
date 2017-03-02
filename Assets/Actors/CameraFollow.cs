using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour {

	//Camera follow script.
	public float smoothTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	private Camera thisCamera;

	// Update is called once per frame
	/*void Update () 
	{
		//if(GameManager.
		Vector3 point = thisCamera.WorldToViewportPoint(target.position);
		Vector3 delta = target.position - thisCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
		Vector3 destination = transform.position + delta;
		transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, smoothTime);
	}*/
	void Start() {
		thisCamera = GetComponent<Camera> ();
	}
}
