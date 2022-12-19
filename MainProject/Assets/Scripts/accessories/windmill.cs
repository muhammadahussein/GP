using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windmill : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationSpeed = 30;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right*rotationSpeed*Time.deltaTime);
    }
}
