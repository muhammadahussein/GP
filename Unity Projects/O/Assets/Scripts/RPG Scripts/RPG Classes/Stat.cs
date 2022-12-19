using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat 
{
    public float StatValue { get; set; }
    public string StatName { get; set; }

    [Newtonsoft.Json.JsonConstructor]
    public Stat(float statValue, string statName)
    {
        this.StatValue = statValue;
        this.StatName = statName;
    }

    public void AddValueToStat(float value)
    {
        StatValue += value;
    }
    public void RemoveValueFromStat(float value)
    {
        StatValue -= value;
    }
}


