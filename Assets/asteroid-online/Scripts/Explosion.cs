using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float duration = 2f;
    public float scale = 1f;
    public float speed = 1f;

    private bool isExploding = false;

    public void Activate()
    {
        if (!isExploding)
        {
            isExploding = true;
            transform.rotation = Random.rotation;
            float randomScale = Random.Range(0.5f, 1.5f);
            transform.localScale = new Vector3(randomScale, randomScale, randomScale);
            StartCoroutine(Expand());
        }
    }

    IEnumerator Expand()
    {
        float elapsedTime = 0f;
        Vector3 startScale = transform.localScale;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.localScale = Vector3.Lerp(startScale, startScale * scale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
