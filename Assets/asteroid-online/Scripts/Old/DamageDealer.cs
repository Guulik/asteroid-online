using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damageAmount = 35;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ebanarot!");
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damageAmount);
        }
    }
}
