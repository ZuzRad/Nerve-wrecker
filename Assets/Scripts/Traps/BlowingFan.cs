using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowingFan : MonoBehaviour
{
	[SerializeField] private float blowPower = 16f;
	[SerializeField] private float forceInterval = 0.1f;
	private bool isPlayerInside = false;
	private Rigidbody2D rb;
	private float timeSinceLastBlow = 0f;
	private void Update()
	{
		if (isPlayerInside && rb != null) 
		{
			timeSinceLastBlow +=Time.deltaTime;

			if (timeSinceLastBlow >= forceInterval)
			{
				rb.AddForce(Vector2.up * blowPower, ForceMode2D.Impulse);
				timeSinceLastBlow = 0f;
			}
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.name == "Model")
		{
			rb = collision.GetComponent<Rigidbody2D>();	
			isPlayerInside = true;	
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.name == "Model")
		{
			isPlayerInside = false;
			rb = null;
		}
	}
}
