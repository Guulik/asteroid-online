using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class ObjectDestroyer : MonoBehaviour
{
    [SerializeField] int points;
    public static event Action<int> objectDestroyed;

    private void OnDestroy()
    {
        objectDestroyed?.Invoke(points);
    }
}