using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteUI : MonoBehaviour
{
    public Action onBackButtonClicked;
    public Action onResetLevel;
    public Action onNextLevel;

    [SerializeField] private UnityEngine.UI.Button backButton;
    [SerializeField] private UnityEngine.UI.Button resetLevelButton;
    [SerializeField] private UnityEngine.UI.Button nextLevelButton;

    private void Start()
    {
        backButton.onClick.AddListener(() =>
        {
            onBackButtonClicked?.Invoke();
        });

        resetLevelButton.onClick.AddListener(() =>
        {
            onResetLevel?.Invoke();
        });

        nextLevelButton.onClick.AddListener(() =>
        {
            onNextLevel?.Invoke();
        });
    }
}
