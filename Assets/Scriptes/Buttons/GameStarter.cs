using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;

    private bool _isPlaying = true;

    public event Action  WeaponInUnbloked;

    public void StartLevel()
    {
        if (_isPlaying)
        {
            PrepareComponents();
        }
        else
        {
            ReastartLevel();
        }
    }

    public void ReastartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void PrepareComponents()
    {
        _enemySpawner.LaunchEnemies();
        _isPlaying = false;
        WeaponInUnbloked?.Invoke();
    }
}
