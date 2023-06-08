using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Heart _heartTemplate;

    private List<Heart> _hearts = new List<Heart>();

    private void OnEnable()
    {
        _player.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int health)
    {
        if (_hearts.Count < health)
        {
            for (int i = 0; i < health; i++)
            {
                CreateHeart();
            }
        }
        else
        {
            DestroyHeart(_hearts[_hearts.Count - 1]);
        }
    }

    private void DestroyHeart(Heart heart)
    {
        _hearts.Remove(heart);
        heart.Destroy();
    }

    private void CreateHeart()
    {
        Heart newHeart = Instantiate(_heartTemplate, transform);
        _hearts.Add(newHeart.GetComponent<Heart>());
    }
}
