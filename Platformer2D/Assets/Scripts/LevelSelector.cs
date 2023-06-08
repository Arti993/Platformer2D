using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Button[] Levels;

    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached");

        for (int i = 0; i < Levels.Length; i++)
        {
            if (i + 1 > levelReached)
                Levels[i].interactable = false;
        }
    }

    public void Select(int numberInBuild)
    {
        SceneManager.LoadScene(numberInBuild);
        Destroy(GameObject.Find("Audio Source"));
    }
}
