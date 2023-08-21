using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructingPlatform : MonoBehaviour
{
	[SerializeField] private float health = 15;
	[SerializeField] private Color startColor;
	[SerializeField] private Color endColor;

	private SpriteRenderer sprite;
	private float maxHealth;

	private void Start()
    {
		sprite = GetComponent<SpriteRenderer>();
		maxHealth = health;
		SetColor();
	}
    private void OnTriggerStay2D(Collider2D collision)
    {
		if (collision.TryGetComponent(out Movement player))
		{
			health -= 0.5f;
			if (health > 0)
			{
				SetColor();
			}
			else
			{
				Destroy(gameObject);
			}
		}
	}

    private void SetColor()
	{
		sprite.color = Color.Lerp(endColor, startColor, (float)(health - 1) / (float)(maxHealth - 1));
	}
}
