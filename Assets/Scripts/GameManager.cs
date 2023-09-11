using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameUIController uiController;

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

        uiController.SetTimeText(player.movement.timeToControl.ToString());
    }
    private void BindToEvents()
    {
        player.healthManager.onPlayerDeath += BackToCheckpoint;
        player.healthManager.onPlayerLost += RestartLevel;
        player.healthManager.onIncreaseHealth += AddHeart;

        player.movement.onPauseGame += HandlePauseGame;
        player.movement.onSlowGame += HandleSlowGame;
        end.onEndLevel += HandleEndLevel;

		foreach (Checkpoint checkpoint in checkpointsList)
        {
            checkpoint.onCheckpointTrigger += ChangeCurrentCheckpoint;
        }

        uiController.onBackToMenu += HandleBackToMenu;
        uiController.onNextLevelButtonClicked += HandleNextLevelButtonClicked;
        uiController.onResetLevel += HandleResetLevel;
        uiController.onResumeLevel += HandleResume;
    }

    private void HandleSlowGame(float time)
    {
        string timeText = time.ToString("F1");
        uiController.SetTimeText(timeText);
    }
    private void HandleNextLevelButtonClicked()
    {
        //dataManager.CurrentLvl++;
        //int levelIndex = dataManager.CurrentLvl - 1;

        //if (levelIndex >= 0)
        //{
        //    SceneManager.LoadScene(playerGameProgress.LevelsData[levelIndex].PathToScene);
        //}
        Time.timeScale = 1;
        Debug.Log("LOAD NEXT LEVEL");
    }

    private void HandleResetLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void HandleBackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    private void HandleResume()
    {
        uiController.SetActivePausePanel(false);
        Time.timeScale = 1;
    }

    private void HandlePauseGame()
    {
        uiController.SetActivePausePanel(true);
        Time.timeScale = 0;
    }

    private void UnbindFromEvents()
    {
        player.healthManager.onPlayerDeath -= BackToCheckpoint;
        player.healthManager.onPlayerLost -= RestartLevel;
        player.healthManager.onIncreaseHealth -= AddHeart;

        player.movement.onPauseGame -= HandlePauseGame;
        player.movement.onSlowGame -= HandleSlowGame;

        end.onEndLevel -= HandleEndLevel;
		foreach (Checkpoint checkpoint in checkpointsList)
        {
            checkpoint.onCheckpointTrigger -= ChangeCurrentCheckpoint;
        }

        uiController.onBackToMenu -= HandleBackToMenu;
        uiController.onNextLevelButtonClicked -= HandleNextLevelButtonClicked;
        uiController.onResetLevel -= HandleResetLevel;
        uiController.onResumeLevel -= HandleResume;
    }


    private void HandleEndLevel()
    {
        uiController.SetActiveCompleteLevelPanel(true);
        Time.timeScale = 0;
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

    private void AddHeart()
    {
        uiController.hpController.IncreaseHeartsAmount();
    }

    private void BackToCheckpoint()
    {
        uiController.hpController.DecreaseHeartsAmount();
        player.model.transform.position = currentCheckpoint.transform.position;
    }
}
