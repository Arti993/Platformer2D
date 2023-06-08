using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CoinBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Coin[] _levelCoins;
    [SerializeField] private TMP_Text _collectedCoinsCount;

    public event UnityAction AllCoinsCollected;

    private void OnEnable()
    {
        _player.CollectedCoinsCountChanged += OnCollectedCoinsCountChanged;
    }

    private void OnDisable()
    {
        _player.CollectedCoinsCountChanged -= OnCollectedCoinsCountChanged;
    }

    private void OnCollectedCoinsCountChanged(int value)
    {
        _collectedCoinsCount.text = $"{value} / {_levelCoins.Length}";

        if (_levelCoins.Length == value)
        {
            AllCoinsCollected?.Invoke();
        }
    }
}
