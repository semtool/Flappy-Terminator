using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(UniversalContactsDetector))]
public class Enemy : MonoBehaviour
{
    public float GetRandomOffset(float minOffsetOfPosition, float maxOffsetOfPosition)
    {
        return Random.Range(minOffsetOfPosition, maxOffsetOfPosition);
    }
}