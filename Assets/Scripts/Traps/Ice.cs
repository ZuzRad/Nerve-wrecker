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
			player.additionalForce = slideForce;
		}
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
		if (collision.TryGetComponent(out Movement player))
		{
			player.additionalForce = 0;
		}
	}
}
