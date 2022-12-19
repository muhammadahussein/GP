using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{  
    private LayerMask m_LayerMask;
    private bool collided = false;
    private Vector3 interactionPos;
    float sphereRadius;
    public Button EButton { get; set; }

    private void Awake()
    {
        m_LayerMask = LayerMask.GetMask("Interactable");
        sphereRadius = 1.5f;
    }
    private void Update()
    {
        interactionPos = transform.position + new Vector3(0, 1f, 0);
        Collider[] hitColliders = Physics.OverlapSphere(interactionPos, sphereRadius, m_LayerMask);
        int i = 0;
        while (i < hitColliders.Length)
        {
            //Place E Button
            EButton.transform.position =
                Camera.main.WorldToScreenPoint(hitColliders[i].transform.position + new Vector3(0, hitColliders[i].bounds.size.y, 0));
           
            if (Input.GetButtonDown("Interact"))
                hitColliders[i].GetComponent<Interactable>().Interact();
            i++;
        }
        //Show and hide E button Button
        collided = Physics.CheckSphere(interactionPos, sphereRadius,m_LayerMask);
        EButton.gameObject.SetActive(collided);
    }
}
