using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public float StatValue { get; set; }
    public float StatMaxValue { get; set; }
    public string StatName { get; set; }

    [Newtonsoft.Json.JsonConstructor]
    public Stat(float statValue, float statMaxValue, string statName)
    {
        this.StatValue = statValue;
        this.StatMaxValue = statMaxValue;
        this.StatName = statName;
    }

    public void AddValueToStat(float value)
    {
        StatValue += value;
        StatValue = Mathf.Clamp(StatValue, 0, StatMaxValue);
        Debug.Log(StatValue);
    }
    public void RemoveValueFromStat(float value)
    {
        StatValue -= value;
        StatValue = Mathf.Clamp(StatValue, 0, StatMaxValue);
    }
}


