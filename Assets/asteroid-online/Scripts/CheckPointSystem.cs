using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingSystem : MonoBehaviour
{
    public List<GameObject> ringList; // список кольцевых чекпоинтов в правильном порядке
    private int currentRingIndex = 0; // индекс текущего кольца, которое игрок должен проходить

    // Функция вызывается, когда объект входит в триггер кольца
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject == ringList[currentRingIndex])
            {
                currentRingIndex++;

                // Проверяем, прошел ли игрок все кольца в правильном порядке
                if (currentRingIndex >= ringList.Count)
                {
                    Debug.Log("You passed all rings");
                    currentRingIndex = 0;
                }
            }
            else
            {
                // Если игрок проходит через кольцо не в том порядке, то он должен вернуться к последнему пройденному кольцу.
                Debug.Log("You missed the ring!");
                currentRingIndex--;
                if (currentRingIndex < 0)
                {
                    currentRingIndex = 0;
                }
            }
        }
    }
}