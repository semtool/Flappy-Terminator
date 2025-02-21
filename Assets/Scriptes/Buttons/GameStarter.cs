using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Player _player;

    private bool _isPlaying = true;
    private int _counter = 0;
    private int _maxTime = 1;

    public event Action WeaponInUnbloked;

    private void OnEnable()
    {
        _player.Disappeared += RestartLevel;
    }

    public void StartLevel()
    {
        if (_isPlaying)
        {
            ActivateComponents();
        }
        else
        {
            ReloadScene();
        }
    }

    public void RestartLevel()
    {
        StartCoroutine(UseDelay());
    }

    private IEnumerator UseDelay()
    {
        while (_counter < _maxTime)
        {
            _counter++;

            var wait = new WaitForSeconds(1);

            yield return wait;
        }

        ReloadScene();
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ActivateComponents()
    {
        _enemySpawner.LaunchEnemies();
        _isPlaying = false;
        WeaponInUnbloked?.Invoke();
    }

    private void OnDisable()
    {
        _player.Disappeared -= RestartLevel;
    }
}