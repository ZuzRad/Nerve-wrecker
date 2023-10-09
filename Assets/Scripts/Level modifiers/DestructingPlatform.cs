using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructingPlatform : MonoBehaviour
{
	[SerializeField] private float time = 15;
	[SerializeField] private Color startColor;
	[SerializeField] private Color endColor;

    [SerializeField] private SpriteRenderer sprite;
	[SerializeField] private BoxCollider2D boxCollider;
	private HealthManager healthManager;
	private float maxTime;

	private void Start()
    {
		sprite = GetComponent<SpriteRenderer>();
        maxTime = time;
		SetColor();
	}
    private void OnDisable()
    {
		if (healthManager != null)
		{
			healthManager.onPlayerDeath -= ResetPlatform;
		}
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
		if (collision.TryGetComponent(out HealthManager hpManager))
		{
            if (healthManager == null) 
			{
                healthManager = hpManager;
				healthManager.onPlayerDeath += ResetPlatform;
            }


			time -= 0.5f;
			if (time > 0)
			{
				SetColor();
			}
			else
			{
				StartCoroutine(WaitAndResetPlatform());
			}
		}
	}
	private void ResetPlatform()
	{
        sprite.enabled = true;
        boxCollider.enabled = true;
        sprite.color = startColor;
        time = maxTime;
    }

	private IEnumerator WaitAndResetPlatform()
	{
		sprite.enabled = false;
		boxCollider.enabled = false;
		yield return new WaitForSeconds(2f);
		ResetPlatform();
    }

    private void SetColor()
	{
		sprite.color = Color.Lerp(endColor, startColor, (float)(time - 1) / (float)(maxTime - 1));
	}
}
