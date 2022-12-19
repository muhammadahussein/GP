using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Snow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<VisualEffect>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
