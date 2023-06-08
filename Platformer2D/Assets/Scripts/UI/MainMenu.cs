using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using IJunior.TypedScenes;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.GetInt("levelReached", 1);
    }

    public void PlayCurrentLevel()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached");

        SceneManager.LoadScene(levelReached);

        Destroy(GameObject.Find("Audio Source"));
    }

    public void OpenLevelsList()
    {
        LevelsChoose.Load();
    }

    public void RestartProgress()
    {
        PlayerPrefs.SetInt("levelReached", 1);

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
