using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes camera follow a target smoothly.
/// </summary>
[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour {

	//Camera follow script.
	public float smoothTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public static Camera thisCamera;
    public Transform target;

	// Update is called once per frame
	void Update () 
	{
        Vector3 point = thisCamera.WorldToViewportPoint(target.position);
        Vector3 delta = target.position - thisCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
        Vector3 destination = transform.position + delta;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, smoothTime);
    }
	void Awake() {
		thisCamera = GetComponent<Camera> ();
	}
}
