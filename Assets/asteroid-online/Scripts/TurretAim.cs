using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif // UNITY_EDITOR

public class TurretAim : MonoBehaviour
{
    [SerializeField] private Transform baseBone;
    [SerializeField] private Transform yHingeBone;
    [SerializeField] private Transform xHingeBone;
    [SerializeField] private Transform aimPoint;
    [SerializeField] protected Transform target;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float maxRotationAngle = 10f;
    [SerializeField] protected float attackDistance = 10;
    protected float distanceToTarget = 0;
    private Quaternion baseRotationOffset;
    private Quaternion yHingeRotationOffset;
    private Quaternion xHingeRotationOffset;

    protected void Start()
    {
        baseRotationOffset = baseBone.localRotation;
        yHingeRotationOffset = yHingeBone.localRotation;
        xHingeRotationOffset = xHingeBone.localRotation;
    }

    protected void Update()
    {
        if(target == null)
            return;

        distanceToTarget = Vector3.Magnitude(target.position - transform.position);

        if(distanceToTarget <= attackDistance)
        {
            Vector3 targetDirection;
            if(target != null)
                targetDirection = target.position - aimPoint.position;
            else
                targetDirection = new Vector3(0,0,0);

            Quaternion turretRotation = Quaternion.Euler(transform.eulerAngles);
            Quaternion targetRotation = Quaternion.Inverse(turretRotation) * Quaternion.LookRotation(targetDirection);

            Quaternion yHingeRotation = Quaternion.Euler(0f, 0f, targetRotation.eulerAngles.y);
            Quaternion xHingeRotation = Quaternion.Euler(targetRotation.eulerAngles.x * -1, 0f, 0f);

            float dot = Vector3.Dot(targetDirection.normalized, transform.forward);
            if (Mathf.Acos(dot) * Mathf.Rad2Deg < maxRotationAngle)
            {
                if(rotationSpeed == 0)
                {
                    baseBone.localRotation = baseRotationOffset;
                    yHingeBone.localRotation = yHingeRotationOffset * yHingeRotation;
                    xHingeBone.localRotation = xHingeRotationOffset * xHingeRotation;
                }
                else
                {
                    baseBone.localRotation = baseRotationOffset;
                    yHingeBone.localRotation = Quaternion.RotateTowards(yHingeBone.localRotation, yHingeRotationOffset * yHingeRotation, rotationSpeed * Time.deltaTime);
                    xHingeBone.localRotation = Quaternion.RotateTowards(xHingeBone.localRotation, xHingeRotationOffset * xHingeRotation, rotationSpeed * Time.deltaTime);
                }
            }
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(TurretAim))]
public class HandlesEditor_one : Editor
{
    SerializedProperty attackDistance;

    protected void OnEnable()
    {
        attackDistance = serializedObject.FindProperty("attackDistance");
    }
    public void OnSceneGUI()
    {
        var linkedObject = target as TurretAim;
        EditorGUI.BeginChangeCheck();

        Handles.color = Color.red;
        float newAttackDistance = Handles.RadiusHandle(Quaternion.identity, linkedObject.transform.position, attackDistance.floatValue, false);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Update params");
            attackDistance.floatValue = newAttackDistance;
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif // UNITY_EDITOR