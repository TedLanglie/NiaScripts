using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    public void SetHealth(float health, float maxHealth)
    {
        _slider.value = health;
        _slider.maxValue = maxHealth;
    }
}
