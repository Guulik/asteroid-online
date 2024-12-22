using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scope : MonoBehaviour
{
    private GameObject objectToPlace;
    [SerializeField] private GameObject Prefab = null;
    [SerializeField] private float raycastDistance = 10f;
    [SerializeField] private LayerMask ignoreLayer;
    [SerializeField] private Transform MissleTarget = null;
    [SerializeField] private Transform CurrentMissleTarget = null;
    private Spacecraft SelectTarget = null;

    private void OnEnable() {
        SelectTarget.Enable();
        SelectTarget.Player.SelectTarget.performed += SetTarget;
    }

    private void OnDisable() {
        SelectTarget.Disable();
        SelectTarget.Player.SelectTarget.performed -= SetTarget;
    }

    private void Awake()
    {
        SelectTarget = new Spacecraft();
        objectToPlace = Instantiate(Prefab, Vector3.zero, Quaternion.Euler(Vector3.zero));
        objectToPlace.SetActive(false);
    }

    public void SetTarget(InputAction.CallbackContext Value)
    {
        if(MissleTarget != null)
            MissleTarget.gameObject.layer = 1;
        if(CurrentMissleTarget != null)
        {
            MissleTarget = CurrentMissleTarget;
            CurrentMissleTarget.gameObject.layer = 7;
        }
        else
            MissleTarget = null;
        gameObject.GetComponent<MissileLauncher>().MissileTarget = MissleTarget;
    }

    void Update()
    {
        Ray ray = new Ray(transform.position + transform.forward * 5, transform.forward);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, raycastDistance,~ignoreLayer))
        {
            objectToPlace.SetActive(true);
            objectToPlace.transform.position = hitInfo.point;
            objectToPlace.transform.rotation = transform.rotation;

            CurrentMissleTarget = hitInfo.transform;
        }
        else
        {
            CurrentMissleTarget = null;
            objectToPlace.SetActive(false);
        }    
    }
}
