using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    private CharacterStats charStatsInstance;
    [SerializeField] private TextMeshProUGUI damageText, armorText;
    [SerializeField] private Image hpBar, shieldBar;

    public static StatsUI Instance;

    private void Start()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
        charStatsInstance = CharacterStats.Instance;
        UpdateStatsUI();
    }

    public void UpdateStatsUI()
    {
        damageText.text = charStatsInstance.GetStat("Damage").StatValue.ToString();
        armorText.text = charStatsInstance.GetStat("Armor").StatValue.ToString();
        hpBar.fillAmount = (charStatsInstance.GetStat("Health").StatValue) / 100;
        shieldBar.fillAmount = (charStatsInstance.GetStat("Shield").StatValue) / 100;
    }
}