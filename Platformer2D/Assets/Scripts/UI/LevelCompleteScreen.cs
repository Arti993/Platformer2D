using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteScreen : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Player _player;
    [SerializeField] private CoinBar _coinbar;
    [SerializeField] private Fireworks _fireworks;
    [SerializeField] private AudioSource _victorySound;

    private const string LevelReached = "levelReached";
    private float _waitTimeToNextLevelStart = 4f;

    private void Start()
    {
        _coinbar.AllCoinsCollected += OnAllCoinsCollected;

        _panel.SetActive(false);
    }

    private void OnAllCoinsCollected()
    {
        _victorySound.Play();

        _player.DisableControl();

        _panel.SetActive(true);

        _fireworks.Play();

        StartCoroutine(TurnOnTimer(_waitTimeToNextLevelStart));
    }

    private void GoToNextLevel()
    {
        CheckGameProgress();

        int currentLevelSceneNumber = SceneManager.GetActiveScene().buildIndex;

        int nextLevelSceneNumber = currentLevelSceneNumber + 1;

        SceneManager.LoadScene(nextLevelSceneNumber);
    }

    private void CheckGameProgress()
    {
        int currentLevelSceneNumber = SceneManager.GetActiveScene().buildIndex;

        int levelReached = PlayerPrefs.GetInt(LevelReached);

        if(currentLevelSceneNumber == levelReached)
        {
            levelReached++;

            PlayerPrefs.SetInt(LevelReached, levelReached);
        }
    }

    private IEnumerator TurnOnTimer(float duration)
    {
        float timerTime = 0f;

        while (timerTime < duration)
        {
            timerTime += Time.deltaTime;
            yield return null;
        }

        GoToNextLevel();
    }
}
