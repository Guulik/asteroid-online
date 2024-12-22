using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSystem : MonoBehaviour
{
    [SerializeField] private List<GameObject> ringList;
    private int currentRingIndex = 0;

    private void Start() 
    {
        if (ringList[0] != null)
            ringList[0].GetComponent<CheckPointIndication>().ChangeMaterial();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CheckPoint")
        {
            if (other.gameObject == ringList[currentRingIndex])
            {
                currentRingIndex++;
                Debug.Log("You passed ring " + currentRingIndex);

                if (currentRingIndex >= ringList.Count)
                {
                    ringList[currentRingIndex - 1].gameObject.GetComponent<CheckPointIndication>().ChangeMaterial();
                    Debug.Log("You passed all rings");
                    currentRingIndex = 0;
                    ringList[currentRingIndex].gameObject.GetComponent<CheckPointIndication>().ChangeMaterial();
                }
                else
                {
                    ringList[currentRingIndex].gameObject.GetComponent<CheckPointIndication>().ChangeMaterial();
                    ringList[currentRingIndex - 1].gameObject.GetComponent<CheckPointIndication>().ChangeMaterial();
                }
            }
            else
            {
                Debug.Log("You missed the ring!");
                // currentRingIndex--;
                // if (currentRingIndex < 0)
                // {
                //     currentRingIndex = 0;
                // }
                ringList[currentRingIndex].gameObject.GetComponent<CheckPointIndication>().ChangeMaterial();
                if(currentRingIndex > 0)
                {
                    currentRingIndex--;
                    ringList[currentRingIndex].gameObject.GetComponent<CheckPointIndication>().ChangeMaterial();
                }
            }
        }
    }
}