using TMPro;
using UnityEngine;

public class Scorer : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private EnemySpawner _enemyContactsDetector;

    private int _counter = 0;

    private void OnEnable()
    {
        _enemyContactsDetector.IsDestroy += ShowFragsNumber;
    }

    private void ShowFragsNumber()
    {
        _counter++;

        _text.text = _counter.ToString();
    }

    private void OnDisable()
    {
        _enemyContactsDetector.IsDestroy -= ShowFragsNumber;
    }
}