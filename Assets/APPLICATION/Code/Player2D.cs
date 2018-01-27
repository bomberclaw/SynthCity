using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : MonoBehaviour {

	public int playerId;
	public float maxSpeed;
	public float jumpForce;

	private Vector2 dir = Vector2.zero;

	private Rigidbody2D _rigidbody;

	// Use this for initialization
	void Start () {
		_rigidbody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		dir.x = Input.GetAxis ("Horizontal");
		dir.y = Input.GetAxis ("Vertical");

		if (Input.GetAxis ("Horizontal") != 0) {

		}
	}


	void Jump() {
		
	}

	void Shoot() {

	}
}
