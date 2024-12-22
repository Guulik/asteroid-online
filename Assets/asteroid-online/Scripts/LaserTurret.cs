using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif // UNITY_EDITOR

public class LaserTurret : TurretAim
{
    [SerializeField] private float DamagePerSeconf = 5f;
    [SerializeField] private bool isDamager = true;
    [SerializeField] private string damageTag = "Player";
    [SerializeField] private LineRenderer lineRenderer;

    private void Start()
    {
        base.Start();

        lineRenderer.positionCount = 2;
    }

    private void Update()
    {
        base.Update();

        Ray ray = new Ray(lineRenderer.transform.position, lineRenderer.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, attackDistance))
        {
            if(hit.rigidbody.gameObject.CompareTag(damageTag) && isDamager)
            {
                lineRenderer.SetPosition(0, lineRenderer.transform.position);
                lineRenderer.SetPosition(1, hit.point);
                hit.rigidbody.gameObject.GetComponent<HDSystem>().TakeDamage(DamagePerSeconf*Time.deltaTime);
            }
            else
            {
                lineRenderer.SetPosition(0, lineRenderer.transform.position);
                lineRenderer.SetPosition(1, lineRenderer.transform.position);
            }
        }
        else
        {
            lineRenderer.SetPosition(0, lineRenderer.transform.position);
            lineRenderer.SetPosition(1, lineRenderer.transform.position);
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(LaserTurret))]
public class HandlesEditor_LaserTurret : HandlesEditor_one
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