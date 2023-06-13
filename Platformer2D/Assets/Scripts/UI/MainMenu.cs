using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using IJunior.TypedScenes;

public class MainMenu : MonoBehaviour
{
    private const string LevelReached = "levelReached";

    private void Start()
    {
        PlayerPrefs.GetInt(LevelReached, 1);
    }

    public void PlayCurrentLevel()
    {
        int levelReached = PlayerPrefs.GetInt(LevelReached);

        SceneManager.LoadScene(levelReached);

        Destroy(MenuMusicController.Instance.gameObject);
    }

    public void OpenLevelsList()
    {
        LevelsChoose.Load();
    }

    public void RestartProgress()
    {
        PlayerPrefs.SetInt(LevelReached, 1);

        PlayCurrentLevel();
    }

    public void ShowAuthors()
    {
        Authors.Load();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
