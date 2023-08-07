using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Transform checkpoint;

    private void OnEnable()
    {
        BindToEvents();
    }
    private void OnDisable()
    {
        UnbindFromEvents();
    }
    private void BindToEvents()
    {
        player.healthManager.onPlayerDeath += BackToCheckpoint;
        player.healthManager.onPlayerLost += RestartLevel;
    }

    private void UnbindFromEvents()
    {
        player.healthManager.onPlayerDeath -= BackToCheckpoint;
        player.healthManager.onPlayerLost -= RestartLevel;
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void BackToCheckpoint()
    {
        player.model.transform.position = checkpoint.position;
    }
}
