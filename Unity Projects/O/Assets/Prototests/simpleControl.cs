using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleControl : MonoBehaviour
{
    Animator anim;

    Character player;
    void Start()
    {
        anim = GetComponent<Animator>();
        player = new Character(anim, true);
    }

    float rotationSpeed = 120;
    private void Update()
    {

        if (Input.GetKey(KeyCode.A))
        {
            player.RotateTarget(Time.deltaTime * -rotationSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            player.RotateTarget(Time.deltaTime * rotationSpeed);
        }
        if (Input.GetKey(KeyCode.W))
        {
            player.Move(true);
        }
        else
        {
            player.Move(false);
        }
        
        player.Update();
    }
    private void OnAnimatorIK(int layerIndex)
    {
        player.UpdateIK();
    }
}