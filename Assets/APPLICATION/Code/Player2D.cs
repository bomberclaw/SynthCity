using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : MonoBehaviour {

	public int playerId;
	public float speed;
	public float jumpForce;
	public LayerMask whatIsGround;
	public LayerMask whatIsWall;
	public Spawner _spawner;
	public int maxPapers = 5;
	public int currentPapers = 5;

	public Transform groundCheck;
	public Transform wallCheck;
	private bool isFacingRight;

	private bool isGrounded;
	private bool isWalled;

	private Rigidbody2D _rigidbody;
	private Collider2D _collider;
	private Animator _anim;

	private Transform oneWayPlatform;
	private Transform prevOneWayPlatform;
	private Vector3 initialPosition;

	// Use this for initialization
	void Start () {
		_rigidbody = GetComponent<Rigidbody2D> ();
		_collider = GetComponent<Collider2D> ();
		_anim = GetComponentInChildren<Animator> ();
		isFacingRight = transform.localScale.x > 0;
		initialPosition = transform.position + Vector3.up;
	}

	void FixedUpdate() {
		
		float moveHorizontal = Input.GetAxis ("Horizontal" + playerId.ToString());

		Vector3 startPoint = groundCheck.position - new Vector3 (_collider.bounds.size.x / 2, 0, 0);
		Vector3 endPoint = groundCheck.position + new Vector3 (_collider.bounds.size.x / 2, 0, 0);

		Debug.DrawLine (startPoint, endPoint, Color.green);
		isGrounded = Physics2D.Linecast (startPoint, endPoint, whatIsGround);

		startPoint = wallCheck.position - new Vector3 (0, _collider.bounds.size.y / 2, 0);
		endPoint = wallCheck.position + new Vector3 (0, _collider.bounds.size.y / 2, 0);

		Debug.DrawLine (startPoint, endPoint, Color.blue);
		isWalled = Physics2D.Linecast (startPoint, endPoint, whatIsWall);

		Vector3 move = new Vector3 (moveHorizontal * speed * Time.deltaTime, _rigidbody.velocity.y, 0f);

		if (moveHorizontal < 0 && isFacingRight) {
			Flip ();
		} else if (moveHorizontal > 0 && !isFacingRight) {
			Flip ();
		}

		if (!isWalled)
			_rigidbody.velocity = move;
		else
			_rigidbody.velocity = new Vector3 (0, _rigidbody.velocity.y, 0f);

		_anim.SetBool ("grounded", isGrounded);
		_anim.SetFloat ("hSpeed", Mathf.Abs(_rigidbody.velocity.x));
		_anim.SetFloat ("vSpeed", _rigidbody.velocity.y);

	}

	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown ("Jump" + playerId.ToString())) {
			Jump ();
		}

		if (Input.GetButtonDown ("Shoot" + playerId.ToString())) {
			Shoot ();
		}
	}

	void Flip() {
		isFacingRight = !isFacingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void Jump() {
		if (isGrounded) {
			if (Input.GetAxis ("Vertical" + playerId.ToString()) < 0 && oneWayPlatform != null) {
				prevOneWayPlatform = oneWayPlatform;
				Physics2D.IgnoreCollision (_collider, oneWayPlatform.GetComponent<Collider2D> ());
				StartCoroutine (ReverseIgnoreCollision ());
			} else {
				_rigidbody.velocity = Vector3.zero;
				_rigidbody.AddForce (new Vector2 (0, jumpForce));
				_anim.SetFloat ("vSpeed", _rigidbody.velocity.y);
			}
		}
	}

	void Bounce(float force) {
		_rigidbody.velocity = Vector3.zero;
		_rigidbody.AddForce (new Vector2 (0, force));
		_anim.SetFloat ("vSpeed", _rigidbody.velocity.y);
	}

	void Shoot() {
		_rigidbody.velocity = new Vector3 (0, _rigidbody.velocity.y, 0f);
		if (currentPapers > 0) {
			_anim.SetTrigger ("shoot");
			_spawner.SpawnObject ();
			currentPapers -= 1;
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		// Si colisiona con una OneWayPlatform
		if (col.gameObject.layer == 11) {
			oneWayPlatform = col.gameObject.transform;
		}
	}

	void OnCollisionExit2D(Collision2D col) {
		// Si colisiona con una OneWayPlatform
		if (col.gameObject.layer == 11) {
			oneWayPlatform = null;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		
		if (other.gameObject.tag == "ReloaderJ" + playerId.ToString ()) {
			currentPapers = maxPapers;
		} else if (other.gameObject.name.Contains ("Death")) {
			currentPapers = 0;
			transform.position = initialPosition;
		}
	}

	private IEnumerator ReverseIgnoreCollision() {
		yield return new WaitForSeconds(0.3f);
		Physics2D.IgnoreCollision (_collider, prevOneWayPlatform.GetComponent<Collider2D> (), false);
		prevOneWayPlatform = null;
	}
}
