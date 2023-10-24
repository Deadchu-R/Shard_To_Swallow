using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;

    private void Awake()
    {
        _healthText.text = "0";
    }

    public void UpdateHealthText(int health)
    {
        _healthText.text = health.ToString();
    }
}
