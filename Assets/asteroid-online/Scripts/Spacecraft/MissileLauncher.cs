using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MissileLauncher : MonoBehaviour
{
    private Spacecraft MissileLaunch = null;
    [SerializeField] private GameObject prefab;
    [SerializeField] private float force = 100f;
    [SerializeField] private float SpawnOffset = 5f;
    [SerializeField] public Transform MissileTarget = null;
    [SerializeField] private bool isControlledByPlayer = true;

    //Поебень для инпут системы
    private void OnEnable() {
        MissileLaunch.Enable();
        MissileLaunch.Player.Fire.performed += Launch;
    }

    private void OnDisable() {
        MissileLaunch.Disable();
        MissileLaunch.Player.Fire.performed -= Launch;
    }

    void Awake(){
        MissileLaunch = new Spacecraft();
    }
    //Конец поебени

    public void Launch(InputAction.CallbackContext Value)
    {
        if(isControlledByPlayer)
        {
            GameObject newObject = Instantiate(prefab, transform.position + transform.forward * SpawnOffset, transform.rotation);
            Rigidbody rb = newObject.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward.normalized * force, ForceMode.Impulse);

            if(MissileTarget != null)
            {
                newObject.GetComponent<AutoaimMissile>().target = MissileTarget;
            }
        }
    }
    //ПерегрузО4ка
    public void Launch()
    {
        GameObject newObject = Instantiate(prefab, transform.position + transform.forward * SpawnOffset, transform.rotation);
        Rigidbody rb = newObject.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward.normalized * force, ForceMode.Impulse);

        if(MissileTarget != null)
        {
            newObject.GetComponent<AutoaimMissile>().target = MissileTarget;
        }
    }
}
