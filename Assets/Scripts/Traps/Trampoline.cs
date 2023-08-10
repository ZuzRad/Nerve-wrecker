using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
	[SerializeField] private float forcePower = 16f;

	private Rigidbody2D rb;
	private float timeSinceLastBlow = 0f;
	private Animator animator;

    private void Start()
    {
		animator = GetComponent<Animator>();
    }
    private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out Movement player))
		{
			rb = player.GetComponent<Rigidbody2D>();
			rb.AddForce(Vector2.up * forcePower, ForceMode2D.Impulse);
			animator.SetTrigger("standing");
		}
	}
}
