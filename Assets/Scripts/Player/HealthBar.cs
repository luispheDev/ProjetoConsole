using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBarSprite;

    public void RemoveHealthBar(float maxLife, float currentLife)
    {
        _healthBarSprite.fillAmount = currentLife / maxLife;
    }

    public void AddHealthBar(float currentLife)
    {
        _healthBarSprite.fillAmount += currentLife;
    }
}
