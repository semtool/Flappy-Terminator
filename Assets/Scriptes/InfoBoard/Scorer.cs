using TMPro;
using UnityEngine;

public class Scorer : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private EnemySpawner _enemyContactsDetector;

    private int _fragsCounter = 0;

    private void OnEnable()
    {
        _enemyContactsDetector.IsDestroyed += ShowFragsNumber;
    }

    private void ShowFragsNumber()
    {
        _fragsCounter++;

        _text.text = _fragsCounter.ToString();
    }

    private void OnDisable()
    {
        _enemyContactsDetector.IsDestroyed -= ShowFragsNumber;
    }
}