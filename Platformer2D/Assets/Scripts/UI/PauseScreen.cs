using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using IJunior.TypedScenes;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _mainMenuButton;

    private GameObject _panel;

    private void OnEnable()
    {
        _continueButton.onClick.AddListener(OnContinueButtonClick);
        _mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);
    }

    private void OnDisable()
    {
        _continueButton.onClick.RemoveListener(OnContinueButtonClick);
        _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClick);
    }

    public void OpenPanel(GameObject panel)
    {
        _panel = panel;
        _panel.SetActive(true);
        _player.DisableControl();
        Time.timeScale = 0;
    }

    private void ClosePanel()
    {
        _panel.SetActive(false);
        _player.EnableControl();
        Time.timeScale = 1;
    }

    private void OnContinueButtonClick()
    {
        ClosePanel();
    }

    private void OnMainMenuButtonClick()
    {
        ClosePanel();
        MainScreenMenu.Load();
    }
}
