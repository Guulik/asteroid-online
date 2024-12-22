using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public float maxHP = 100;
    public float currentHP = 100;

    public RectTransform foreground;

    private float initialWidth;
    [SerializeField] private GameObject Player;

    void Start()
    {
        initialWidth = foreground.sizeDelta.x;
    }

    void Update()
    {
        if(Player != null)
            currentHP = Mathf.Clamp(Player.gameObject.GetComponent<HDSystem>().CurrentHealth, 0, maxHP);
        else
            currentHP = 0;
        float hpPercent = (float)currentHP / (float)maxHP;
        float newWidth = initialWidth * hpPercent;

        Vector2 sizeDelta = foreground.sizeDelta;
        sizeDelta.x = newWidth;
        foreground.sizeDelta = sizeDelta;
    }

    public void SetHP(int newHP)
    {
        currentHP = Mathf.Clamp(newHP, 0, maxHP);
    }
}
