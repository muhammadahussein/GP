using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventManager : MonoBehaviour
{
    //https://youtu.be/gx0Lt4tCDE0
    //http://www.unitygeek.com/delegates-events-unity/
    public delegate void ItemEvent(Item item);
    public static event ItemEvent OnItemAdded;

    public static void ItemAdded(Item item)
    {
        OnItemAdded(item);
    }
}
