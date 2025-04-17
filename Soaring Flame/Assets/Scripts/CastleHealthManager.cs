using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleHealthManager : MonoBehaviour
{
    public Slider slider;

    public float maxHealth = 100;
    public float currentHealth;

    void Start()
    {
        V.BaseHealth = maxHealth;
        currentHealth = V.BaseHealth;
        SetMaxHealth(maxHealth);
    }

    void FixedUpdate()
    {
        currentHealth = V.BaseHealth;
        SetHealth(currentHealth);
    }

    public void SetHealth(float health)
    {
        slider.value = maxHealth - currentHealth;
    }

    public void SetMaxHealth(float health) 
    {
        slider.maxValue = health;
        slider.value = 0;
    }
} 
