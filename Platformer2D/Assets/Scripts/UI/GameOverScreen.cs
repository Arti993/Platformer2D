using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using IJunior.TypedScenes;


public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Button _tryAgainButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _panel;
    [SerializeField] private AudioSource _loseSound;

    private void Start()
    {
        _panel.SetActive(false);
        _player.Died += OnDied;
        _tryAgainButton.onClick.AddListener(OnTryAgainButtonClick);
        _mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);
    }

    private void OnDestroy()
    {
        _player.Died -= OnDied;
        _tryAgainButton.onClick.RemoveListener(OnTryAgainButtonClick);
        _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClick);
    }

    private void OnDied()
    {
        _loseSound.Play();
        _panel.SetActive(true);
        _pauseButton.interactable = false;
        _player.DisableControl();
        Time.timeScale = 0;
    }

    private void OnTryAgainButtonClick()
    {
        _panel.SetActive(false);
        Time.timeScale = 1;
        Scene currentLevelScene = SceneManager.GetActiveScene();
        int currentLevelBuildIndex = currentLevelScene.buildIndex;
        SceneManager.LoadScene(currentLevelBuildIndex);
    }

    private void OnMainMenuButtonClick()
    {
        _panel.SetActive(false);
        Time.timeScale = 1;
        MainScreenMenu.Load();
    }
}
