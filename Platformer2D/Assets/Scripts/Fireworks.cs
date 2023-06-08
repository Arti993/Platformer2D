using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireworks : MonoBehaviour
{
    [SerializeField] private GameObject _effect;
    [SerializeField] private Player _player;

    public void Play()
    {
        Instantiate(_effect, _player.transform.position, Quaternion.identity);
    }
}
