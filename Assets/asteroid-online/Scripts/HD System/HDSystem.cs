using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HDSystem : MonoBehaviour
{
    [SerializeField] private bool isMaser = false;
    [SerializeField] protected float MaxHealth = 100;
    public float CurrentHealth;
    [SerializeField] private float Damage = 35;
    [SerializeField] private int points = 100;
    public static event Action<int> objectDestroyed;

    protected void Start()
    {
        CurrentHealth = MaxHealth;
    }
    
    public void TakeDamage(float Damage)
    {
        CurrentHealth -= Damage;

        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Untagged")) //Ну это тоже фигня так-то
            return;
        if(other.gameObject.GetComponent<HDSystem>().isMaser == false && isMaser == false)
            return;
        other.gameObject.GetComponent<HDSystem>().TakeDamage(Damage);
    }

    protected void OnDestroy()
    {
        objectDestroyed?.Invoke(points);
    }
}
