using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : Spawnable {

	public float speed;
	public float lifeTime;
	public LayerMask destroyerLayers;

	private float t = 0;

	void Update()
	{
		if (Active) {
			if(Spawner.transform.parent.localScale.x > 0)
				transform.Translate (Vector3.right * speed * Time.deltaTime);
			else
				transform.Translate (Vector3.left * speed * Time.deltaTime);

			t += Time.deltaTime;
			if (t >= lifeTime) {
				Spawner.RemoveObject(gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if ((other.gameObject.layer & destroyerLayers.value) == 0)
		{
			Spawner.RemoveObject(gameObject);
		}
	}

	public override void SetActiveState()
	{
		Active = true;
	}

	public override void SetInactiveState()
	{
		Active = false;
		t = 0;
	}
}
