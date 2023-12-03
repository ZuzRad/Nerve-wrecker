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

    private void Awake()
    {
        currentCheckpoint = start;
        GameMusicManager.Music randomMusic = (GameMusicManager.Music)UnityEngine.Random.Range(0, 3);
        musicManager.PlayMusic(randomMusic);
        uiController.SetTimeSlider(player.movement.timeToControl);
        loadLevel();
	}
    private void loadLevel() 
    {
        SaveSystem.DeleteProgress(4);
        var x = SceneManager.GetActiveScene().name;
		int level = (int)char.GetNumericValue(x[x.Length - 1]);
		PlayerData playerData = SaveSystem.LoadPlayer(level);
        if (false/*playerData != null*/)
        {
            Debug.Log($"x:{playerData.position[0]};\ny:{playerData.position[1]};\nz:{playerData.position[2]}");
            Vector3 loadedPosition = new Vector3(playerData.position[0], playerData.position[1], playerData.position[2]);
            player.transform.position = loadedPosition;

            if (playerData.lastCheckpoint != -1)// je¿eli przez jakiegoœ przeszed³
                ChangeCurrentCheckpoint(checkpointsList[playerData.lastCheckpoint]);
            

            int decrease = 3 - playerData.health;
            for (int i = 0; i < decrease; i++)
            {
                //Debug.Log("decreased");
                //uiController.hpController.DecreaseHeartsAmount();
                player.healthManager.DecreaseHealth();
            }

        }
        //Debug.Log($"Health manager:{player.healthManager.currentHealth}");
        //player.healthManager.currentHealth = 2;
       
        
        
        //Debug.Log($"Health manager:{player.healthManager.currentHealth} Saved:{playerData.health}");
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
        Debug.Log($"Health manager:{player.healthManager.currentHealth}");

       uiController.SetTimeSlider(time);
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
        //var x = SceneManager.GetActiveScene().name;
        //int level = (int)char.GetNumericValue(x[x.Length - 1]); //usuwamy plik z progresem
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
        var x = SceneManager.GetActiveScene().name;
        int level = (int)char.GetNumericValue(x[x.Length - 1]); //usuwamy plik z progresem
        SaveSystem.DeleteProgress(level);
        SaveSystem.LevelCompleted(level);   
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
        var x = SceneManager.GetActiveScene().name;
        int level = (int)char.GetNumericValue(x[x.Length - 1]); //usuwamy plik z progresem
        SaveSystem.DeleteProgress(level);
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
        Debug.Log($"Health manager:{player.healthManager.currentHealth}");
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
