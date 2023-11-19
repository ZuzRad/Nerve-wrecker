using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelector : MonoBehaviour
{
    public int level;
	public TextMeshProUGUI levelText;

    void Awake()
    {
        levelText.text = level.ToString();
        bool isCompleted = SaveSystem.IsLevelCompleted(level);
        if (isCompleted)
            levelText.text += "\nCompleted!";
        
    }
    public void OpenScene() 
    {
        SceneManager.LoadScene($"Level {level}");
    }
}
