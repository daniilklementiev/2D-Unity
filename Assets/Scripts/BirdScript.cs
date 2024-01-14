using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
	private float discreteForceFactor  = 300f; // дискретное управление многократным нажатием
	private float continualForceFactor = 5f; // непрерывное управление нажатием и удержанием
	private float vitalityTime		   = 20f; // скорость потери жизненной силы
	private Rigidbody2D body; // reference to component

    void Start()
    { 
		GameState.gameRunning = true;
		body = this.GetComponent<Rigidbody2D>();
		GameState.pipesPassed = 0;
		GameState.vitality = 0.85f;
	}


    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			body.AddForce(discreteForceFactor * Time.timeScale * Vector2.up);
		}
		if (Input.GetKey(KeyCode.W) && GameState.isWkeyEnabled)
		{
			body.AddForce(continualForceFactor * Time.timeScale * Vector2.up);
		}

		this.transform.eulerAngles = new Vector3(0, 0, body.velocity.y * 5f);

		GameState.vitality -= Time.deltaTime / GameState.vitalityPeriod;
		if(GameState.vitality <= 0)
		{
			GameState.vitality = 1f;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		// Debug.Log("Collision: " + collision.gameObject.name);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Transform transformParent = other.gameObject.transform.parent;
		if(transformParent != null && transformParent.gameObject.CompareTag("Pipe"))
		{
			// Pipe trigger
			GameState.isPipeHitted = true;
			GameState.gameRunning = false;
		}
		if (other.gameObject.CompareTag("Food")) 
		{
			GameState.vitality = 1f;
			GameObject.Destroy(other.gameObject);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Pipe")) {
			GameState.pipesPassed += 1;
		}
	}
}
