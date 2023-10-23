using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    public Action onBackToMenu;
    public Action onResetLevel;
    public Action onResumeLevel;
    public Action onNextLevelButtonClicked;

    [Header("Top info")]
    [SerializeField] public HPUIController hpController;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Slider slider;

    [Header("Panels")]
    [SerializeField] private LevelCompleteUI levelCompleteDisplayer;
    [SerializeField] private PauseGameUI pausePanelDisplayer;

    private void Start()
    {
        SetActiveCompleteLevelPanel(false);
        SetActivePausePanel(false);
        pausePanelDisplayer.onResetLevel += () => { onResetLevel?.Invoke(); };
        pausePanelDisplayer.onBackButtonClicked += () => { onBackToMenu?.Invoke(); };
        pausePanelDisplayer.onResumeLevel += () => { onResumeLevel?.Invoke(); };

        levelCompleteDisplayer.onNextLevel += () => { onNextLevelButtonClicked?.Invoke(); };
        levelCompleteDisplayer.onBackButtonClicked += () => { onBackToMenu?.Invoke(); };
        levelCompleteDisplayer.onResetLevel += () => { onResetLevel?.Invoke(); };
    }

    public void SetTimeSlider(float time)
    {
        slider.value = time;
    }

    public void SetActivePausePanel(bool isActive)
    {
        pausePanelDisplayer.gameObject.SetActive(isActive);
    }

    public void SetActiveCompleteLevelPanel(bool isActive)
    {
        levelCompleteDisplayer.gameObject.SetActive(isActive);
    }

    public void SetLevelNumber(int levelNumber)
    {
        levelText.text = "Level " + levelNumber;
    }
}
