using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;

    [Header("Checkpoints")]
    [SerializeField] private List<Checkpoint> checkpointsList = new();
    [SerializeField] private Checkpoint start;
    [SerializeField] private EndLevel end;

    [Header("Sound Managers")]
    [SerializeField] private GameMusicManager musicManager;
    [SerializeField] private SoundManager soundManager;
    private Checkpoint currentCheckpoint;
    private void OnEnable()
    {
        BindToEvents();
    }
    private void OnDisable()
    {
        UnbindFromEvents();
    }

    private void Start()
    {
        currentCheckpoint = start;
        GameMusicManager.Music randomMusic = (GameMusicManager.Music)Random.Range(0, 3);
        musicManager.PlayMusic(randomMusic);
    }
    private void BindToEvents()
    {
        player.healthManager.onPlayerDeath += BackToCheckpoint;
        player.healthManager.onPlayerLost += RestartLevel;
        end.onEndLevel += HandleEndLevel;

		foreach (Checkpoint checkpoint in checkpointsList)
        {
            checkpoint.onCheckpointTrigger += ChangeCurrentCheckpoint;
        }
    }
    private void UnbindFromEvents()
    {
        player.healthManager.onPlayerDeath -= BackToCheckpoint;
        player.healthManager.onPlayerLost -= RestartLevel;
        end.onEndLevel -= HandleEndLevel;
		foreach (Checkpoint checkpoint in checkpointsList)
        {
            checkpoint.onCheckpointTrigger -= ChangeCurrentCheckpoint;
        }
    }


    private void HandleEndLevel()
    {
        Debug.Log("You finished the level");
    }
    private void ChangeCurrentCheckpoint(Checkpoint newChechpoint)
    {
        if(newChechpoint != currentCheckpoint)
        {
            currentCheckpoint = newChechpoint;
            currentCheckpoint.animator.SetTrigger("playerTeleport");
        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void BackToCheckpoint()
    {
        player.model.transform.position = currentCheckpoint.transform.position;
    }
}
