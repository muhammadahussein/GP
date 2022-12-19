using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities 
{
    // Start is called before the first frame update
    public static void LinearLerp(ref float value, float target, float Speed)
    {
        float s = Mathf.Sign(target - value);
        value += (value == target) ? 0 : s* Mathf.Min(Time.deltaTime * Speed, s*(target - value));
    }
}
