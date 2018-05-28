using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float speed = 1;
	private float moveHorizontal, moveVertical, moveYaw, movePitch;

	// Use this for initialization
	void Start () {
		moveHorizontal = 0;
		moveVertical = 0;
		moveYaw = 0;
		movePitch = 0;
	}
	
	// Update is called once per frame
	void Update () {
		moveHorizontal = Input.GetAxis("Horizontal") * speed;
		moveVertical = Input.GetAxis("Vertical") * speed;
		moveYaw += Input.GetAxis("Mouse X") * speed;
		movePitch -= Input.GetAxis("Mouse Y") * speed;
		transform.Translate(new Vector3(moveHorizontal, 0, moveVertical));
		transform.eulerAngles = new Vector3(movePitch, moveYaw, 0);
	}
}
