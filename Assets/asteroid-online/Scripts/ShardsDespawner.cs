using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardsDespawner : MonoBehaviour
{
    [SerializeField] private int DestroyTimer = 5;
    private void Awake() {
        Destroy(gameObject, DestroyTimer);
    }
}
