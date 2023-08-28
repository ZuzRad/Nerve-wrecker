using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenTrapTrigger : MonoBehaviour
{
    [SerializeField] private GameObject trapPrefab;
    [SerializeField] private Transform trapPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Movement player))
        {
            Instantiate(trapPrefab, trapPosition);
        }
    }
}
