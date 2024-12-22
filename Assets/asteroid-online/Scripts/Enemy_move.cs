using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif // UNITY_EDITOR


public class Enemy_move : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float attackDistance = 1f;
    [SerializeField] private float approachDistance = 1f; 
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private float maxReflectionAngle = 45f;
    [SerializeField] private float turnSpeed = 10f;
    [SerializeField] private GameObject explosionPrefab;

    public float brakeForce = 10f;
    private bool isQuitting = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (target == null)
            return;

        Vector3 targetDir = target.position - transform.position;
        float targetDistance = targetDir.magnitude;

        if (targetDistance < attackDistance && targetDistance > approachDistance)
        {
            Vector3 moveDir = targetDir.normalized;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, moveDir, out hit, targetDistance, obstacleMask))
            {
                // Calculate the angle between the target and the reflected ray
                Vector3 reflectDir = Vector3.Reflect(moveDir, hit.normal);
                float angle = Vector3.SignedAngle(targetDir, reflectDir, Vector3.up);

                if (Mathf.Abs(angle) < maxReflectionAngle)
                {
                    // Add a random deviation to the move direction
                    moveDir += Random.insideUnitSphere * 0.3f;
                    moveDir = moveDir.normalized;
                }
                else
                {
                    moveDir = reflectDir.normalized;
                }
            }

            Vector3 rotation = Vector3.RotateTowards(transform.forward, moveDir, turnSpeed * Time.deltaTime, 0f);
            rb.MoveRotation(Quaternion.LookRotation(rotation));
            rb.AddForce(transform.forward*speed);
        }
        else if(targetDistance < approachDistance)
        {
            Vector3 moveDir = targetDir.normalized;
            Vector3 rotation = Vector3.RotateTowards(transform.forward, moveDir, turnSpeed * Time.deltaTime, 0f);
            rb.MoveRotation(Quaternion.LookRotation(rotation));
            
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, brakeForce * Time.fixedDeltaTime);
        }
    }

    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    private void OnDestroy() {
        if(!isQuitting)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Explosion explosionComponent = explosion.GetComponent<Explosion>();
            explosionComponent.Activate(); 
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Enemy_move))]
public class HandlesEditor : Editor
{
    SerializedProperty attackDistance;
    SerializedProperty approachDistance;

    void OnEnable()
    {
        attackDistance = serializedObject.FindProperty("attackDistance");
        approachDistance = serializedObject.FindProperty("approachDistance");
    }
    public void OnSceneGUI()
    {
        var linkedObject = target as Enemy_move;
        EditorGUI.BeginChangeCheck();

        Handles.color = Color.red;
        float newAttackDistance = Handles.RadiusHandle(Quaternion.identity, linkedObject.transform.position, attackDistance.floatValue, false);
        Handles.color = Color.blue;
        float newApproachDistance = Handles.RadiusHandle(Quaternion.identity, linkedObject.transform.position, approachDistance.floatValue, false);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Update params");
            attackDistance.floatValue = newAttackDistance;
            approachDistance.floatValue = newApproachDistance;
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif // UNITY_EDITOR