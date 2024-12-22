using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointIndication : MonoBehaviour
{
    [SerializeField] private Material Material_1;
    [SerializeField] private Material Material_2;
    private bool isChanged = false;
    private Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.material = Material_1;
    }

    public void ChangeMaterial()
    {
        if(isChanged)
        {
            renderer.material = Material_1;
            isChanged = false;
        }
        else
        {
            renderer.material = Material_2;
            isChanged = true;    
        }
    }
}
