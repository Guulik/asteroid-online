using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif // UNITY_EDITOR

public class RocketLaunchTurret : TurretAim
{
    [SerializeField] private float callInterval = 10f;
    private float timer = 0f;

    private void Start() 
    {
        base.Start();  
    }

    void Update()
    {
        base.Update();

        timer += Time.deltaTime;
        if (timer >= callInterval && distanceToTarget <= attackDistance)
        {
            this.gameObject.GetComponentInChildren<MissileLauncher>().MissileTarget = target;
            this.gameObject.GetComponentInChildren<MissileLauncher>().Launch();
            timer = 0f;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(RocketLaunchTurret))]
public class HandlesEditor_RocketLaunchTurret : HandlesEditor_one
{
    void OnEnable()
    {
        base.OnEnable();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
#endif // UNITY_EDITOR