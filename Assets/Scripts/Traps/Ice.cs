using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
	[SerializeField] private float slideForce = 1f;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out Movement player))
		{
			Rigidbody2D playerRB = player.GetComponent<Rigidbody2D>();
			playerRB.velocity *= slideForce;
			//         if (player.isFacingRight)
			//         {

			//         }
			//playerRB.AddForce(new Vector2(horizontal * slideForce, 0f));
		}
	}
}
