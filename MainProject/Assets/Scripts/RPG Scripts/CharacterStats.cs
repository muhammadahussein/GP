using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public List<Stat> charStats = new List<Stat>();
    public static CharacterStats Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        charStats = new List<Stat>()
        {
            new Stat(50, 100, "Health"),
            new Stat(50,100, "Shield"),
            new Stat(5,100, "Damage"),
            new Stat(5,100, "Armor")
        };
    }

    //Equipping
    public void AddStatAdditive(List<Stat> statAdditives)
    {
        foreach (Stat statAdditive in statAdditives)
        {
            GetStat(statAdditive.StatName).AddValueToStat(statAdditive.StatValue);
        }
    }

    //Unequipping
    public void RemoveStatAdditive(List<Stat> statAdditives)
    {
        foreach (Stat statAdditive in statAdditives)
        {
            GetStat(statAdditive.StatName).RemoveValueFromStat(statAdditive.StatValue);
        }
    }

    public Stat GetStat(Stat stat)
    {
        return charStats.Find(x => x == stat);
    }

    public Stat GetStat(string statName)
    {
        return charStats.Find(x => x.StatName == statName);
    }
}