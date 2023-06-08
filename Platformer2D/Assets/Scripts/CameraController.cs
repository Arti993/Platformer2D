using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private Vector3 _desiredPosition;
    private float _cameraMoveSpeed = 3f;

    private void Awake()
    {
        if (!_player)
            _player = FindObjectOfType<Player>().transform;
    }

    private void Update()
    {
        _desiredPosition = _player.position;
        _desiredPosition.z = -5f;

        transform.position = Vector3.Lerp(transform.position, _desiredPosition, _cameraMoveSpeed * Time.deltaTime);
    }
}
