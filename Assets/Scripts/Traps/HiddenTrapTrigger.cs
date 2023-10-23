using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenTrapTrigger : MonoBehaviour
{
    [SerializeField] private GameObject trapPrefab;
    [SerializeField] private Transform trapPosition;
    private bool isTrigger = false;
    private List<GameObject> trapList = new();
    private HealthManager healthManager;

    private void OnDisable()
    {
        if(healthManager != null)
        {
            healthManager.onPlayerDeath -= ResetTraps;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out HealthManager hpManager) && !isTrigger)
        {
            if(healthManager == null)
            {
                healthManager = hpManager;
                healthManager.onPlayerDeath += ResetTraps;
            }
            GameObject trap = Instantiate(trapPrefab, trapPosition);
            trapList.Add(trap);
            isTrigger = true;
        }
    }

    private void ResetTraps()
    {
        foreach(GameObject trap in trapList)
        {
            isTrigger = false;
            Destroy(trap);        
        }
    }
}
