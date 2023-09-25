using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPanelController : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button startGameButton;
    [SerializeField] private UnityEngine.UI.Button chooseLevelButton;
    [SerializeField] private UnityEngine.UI.Button settingsButton;
    [SerializeField] private UnityEngine.UI.Button creditsButton;
    [SerializeField] private UnityEngine.UI.Button exitGameButton;

   // [Space]

    //[SerializeField] private DataManager dataManager;
    public event Action MenuMusic;

    private void Start()
    {
        MenuMusic?.Invoke();

        startGameButton.onClick.AddListener(() =>
        {
            LoadLastLevel();
            
        });

        chooseLevelButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("LevelSelection");
        });

        settingsButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Settings");
        });

        creditsButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Credits");
        });

        exitGameButton.onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif

        });
    }

    private void LoadLastLevel()
    {
		PlayerData playerData = SaveSystem.LoadPlayer();
        SceneManager.LoadScene($"Level {playerData.lastLevel}");
        //SceneManager.LoadScene($"Level 1");
		
	}
}
