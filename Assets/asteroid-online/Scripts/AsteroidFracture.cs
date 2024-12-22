using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidFracture : MonoBehaviour
{
    [SerializeField] GameObject ShardsPrefab = null;
    private bool isQuitting = false;

    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    private void OnDestroy() {
        if (!isQuitting)
        {
            GameObject Shards = Instantiate(ShardsPrefab, transform.position, transform.rotation);
            Shards.transform.localScale = transform.localScale;
        }
    }
}
