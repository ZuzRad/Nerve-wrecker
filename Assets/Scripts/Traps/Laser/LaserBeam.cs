using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] private float radius = 0.5f;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    [SerializeField] private GameObject sight;
    [SerializeField] private float time = 1f;
    [HideInInspector] public GameObject target;
    [HideInInspector] public Transform startPosition;

    private LineRenderer laser;
    private CircleCollider2D circleCollider;
    private HealthManager player;
    private bool playerInsideLaser = false;
    private bool followPlayer = true;

    public Action onTrigger;
    private void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        laser = gameObject.GetComponent<LineRenderer>();

        laser.startWidth = 0.1f;
        laser.endWidth = 0.1f;
        laser.startColor = new Color32(245, 207, 130, 255);
        laser.endColor = startColor;

        laser.SetPosition(0, startPosition.position);
        laser.SetPosition(1, target.transform.position);
    }


    public void Update()
    {
        if (followPlayer)
        {
            laser.SetPosition(1, target.transform.position);
            circleCollider.offset = target.transform.position;
            sight.transform.position = target.transform.position;
        }
    }

    IEnumerator StartDamageTimer()
    {
        float timer = 0f;
        SpriteRenderer sprite = sight.GetComponent<SpriteRenderer>();
        Color endSightColor = endColor;

        while (timer < time)
        {
            Color lerpedColor = Color.Lerp(startColor, endSightColor, timer / time);
            sprite.color = lerpedColor;

            timer += Time.deltaTime;
            yield return null;
        }

        if (playerInsideLaser)
        {
            player.DecreaseHealth();
        }
        followPlayer = true;
        sprite.color = startColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HealthManager healthManager))
        {
            onTrigger?.Invoke();
            followPlayer = false;
            player = healthManager;
            playerInsideLaser = true;
            StartCoroutine(StartDamageTimer());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HealthManager healthManager))
        {
            playerInsideLaser = false;
        }
    }
}

