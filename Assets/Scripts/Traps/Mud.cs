using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mud : MonoBehaviour
{
	[SerializeField] private float slow = 0.5f;
	private float mostRecentSpeed;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("Siema");
			

		if (collision.TryGetComponent(out Movement player))
		{
			mostRecentSpeed = player.speed;
			//Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
			//rb.velocity = new Vector2(rb.velocity.x * slow, rb.velocity.y);
			player.speed = player.speed * slow;
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out Movement player))
		{
			player.speed = mostRecentSpeed;
		}
	}
}
