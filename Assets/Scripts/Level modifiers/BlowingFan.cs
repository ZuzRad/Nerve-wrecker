using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowingFan : MonoBehaviour
{
	[SerializeField] private float blowPower = 16f;
	[SerializeField] private float forceInterval = 0.1f;

	private Rigidbody2D rb;
	private float timeSinceLastBlow = 0f;

	public Action onTrigger;
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out Movement player))
		{
			timeSinceLastBlow += Time.deltaTime;

			if (timeSinceLastBlow >= forceInterval)
			{
				rb.AddForce(Vector2.up * blowPower, ForceMode2D.Impulse);
				timeSinceLastBlow = 0f;
			}
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out Movement player))
		{
			rb = collision.GetComponent<Rigidbody2D>();
			onTrigger?.Invoke();
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out Movement player))
		{
			rb = null;
		}
	}
}
