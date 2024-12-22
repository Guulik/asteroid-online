using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHDSystem : HDSystem
{
    protected void Start() 
    {
        base.Start();
    }
    private void OnEnable()
    {
        HDSystem.objectDestroyed += DoStuff;
    }

    private void OnDisable()
    {
        HDSystem.objectDestroyed -= DoStuff;
    }

    private void DoStuff(int add)
    {
        CurrentHealth += add;
        if(CurrentHealth > MaxHealth)
            CurrentHealth = MaxHealth;
    }
}