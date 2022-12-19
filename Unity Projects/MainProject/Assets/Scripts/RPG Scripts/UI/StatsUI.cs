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
    float healthFill, shieldFill;

    public static StatsUI Instance;

    private void Start()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
        charStatsInstance = CharacterStats.Instance;
        UpdateStatsUI();

        this.gameObject.SetActive(false);
    }
    private void Update()
    {
        float healthStatValue = (charStatsInstance.GetStat("Health").StatValue) / 100;
        float shieldStatValue = (charStatsInstance.GetStat("Shield").StatValue) / 100;
        if (Mathf.Abs(healthFill - healthStatValue) >0.01f)
        {
            Utilities.LinearLerp(ref healthFill, healthStatValue, 1f);
            hpBar.fillAmount = healthFill;
        }
        if (Mathf.Abs(shieldFill - shieldStatValue) > 0.01f)
        {
            Utilities.LinearLerp(ref shieldFill, shieldStatValue, 1f);
            shieldBar.fillAmount = shieldFill;
        }
    }

    public void UpdateStatsUI()
    {
        damageText.text = charStatsInstance.GetStat("Damage").StatValue.ToString();
        armorText.text = charStatsInstance.GetStat("Armor").StatValue.ToString();
       
    }
}