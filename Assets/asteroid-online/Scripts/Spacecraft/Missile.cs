using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float destroyDelay = 10f;
    private bool isExploded = false;
    private bool isQuitting = false;

    private void Awake() {
        Destroy(gameObject, destroyDelay);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Explosion explosionComponent = explosion.GetComponent<Explosion>();
        explosionComponent.Activate();
        isExploded = true;

        Destroy(gameObject);
    }

    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    private void OnDestroy() {
        if(isExploded == false && !isQuitting)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Explosion explosionComponent = explosion.GetComponent<Explosion>();
            explosionComponent.Activate(); 
        }
    }
}
