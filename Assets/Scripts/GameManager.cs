using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameUIController uiController;

    [Header("Checkpoints")]
    [SerializeField] private List<Checkpoint> checkpointsList = new(); // do tego siê odnieœ i da siê za³adowaæ checkpoint
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
        GameMusicManager.Music randomMusic = (GameMusicManager.Music)UnityEngine.Random.Range(0, 3);
        musicManager.PlayMusic(randomMusic);
        uiController.SetTimeText(player.movement.timeToControl.ToString());
        loadLevel();
	}
    private void loadLevel() 
    {
		var x = SceneManager.GetActiveScene().name;
		char level = x[x.Length - 1];
		PlayerData playerData = SaveSystem.LoadPlayer((int)char.GetNumericValue(level));
		Debug.Log($"{playerData.position[0]}, {playerData.position[1]}, {playerData.position[2]}");
		if (playerData != null)
		{
			Vector3 loadedPosition = new Vector3(playerData.position[0], playerData.position[1], playerData.position[2]);
			player.transform.position = loadedPosition;
            
            if(playerData.lastCheckpoint!=-1)// je¿eli przez jakiegoœ przeszed³
                currentCheckpoint = checkpointsList[playerData.lastCheckpoint];
            
            player.healthManager.currentHealth = playerData.health;
		}
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
        SavePlayerInformation();
		SceneManager.LoadScene("Menu");           
    }

    private void HandleResume()
    {
        player.movement.EnableInputs();
        uiController.SetActivePausePanel(false);
        Time.timeScale = 1;
    }

    private void HandlePauseGame()
    {
        player.movement.DisableInputs();
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
        player.movement.DisableInputs();
        uiController.SetActiveCompleteLevelPanel(true);
        Time.timeScale = 0;
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
    private void SavePlayerInformation() 
    {
        var x = SceneManager.GetActiveScene().name;
        char level = x[x.Length - 1];
        Debug.Log("Poziom: " + (int)char.GetNumericValue(level));
        Debug.Log("Zapis danych "+ player.model.transform.position.x + "," + player.model.transform.position.y);
        Debug.Log(checkpointsList.IndexOf(currentCheckpoint));
        SaveSystem.SavePlayer(player, checkpointsList.IndexOf(currentCheckpoint), (int)char.GetNumericValue(level));
    }
}
