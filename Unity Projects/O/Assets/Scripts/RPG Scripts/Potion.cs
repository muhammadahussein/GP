using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour , IConsumable
{
    public void Consume()
    {
        Debug.Log("Drink the potion");
    }
}
