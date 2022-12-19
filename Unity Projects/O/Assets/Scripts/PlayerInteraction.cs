﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{  
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Create an instance of this class in simple control 
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///
    ///
    ///
    ///
    ///
    ///
    ///
    ///
    ///
    ///
    ///
    ///
    ///
    ///
    ///
    ///
    ///
    ///
    ///
    ///
    ///
    ///
    ///
    ///
    ///
    ///
    ///
    /// 
    public LayerMask m_LayerMask;
    [SerializeField] private Transform interactionArea;
    [SerializeField] private Vector3 interactionSize;
    [SerializeField] private bool debug;
    [SerializeField] private Button eButton;
    private bool collided = false;
    private void FixedUpdate()
    {
        Collider[] hitColliders = Physics.OverlapBox(interactionArea.position, interactionSize / 2, Quaternion.identity, m_LayerMask);
        int i = 0;
        while (i < hitColliders.Length)
        {
            //Place E Button
            eButton.transform.position =
                Camera.main.WorldToScreenPoint(hitColliders[i].transform.position + new Vector3(0, hitColliders[i].GetComponent<Collider>().bounds.max.y, 0));
           
            if (Input.GetKeyDown(KeyCode.E))
                hitColliders[i].GetComponent<Interactable>().Interact();
            i++;
        }
        //Show and hide E button Button
        collided = Physics.CheckBox(interactionArea.position, interactionSize / 2, Quaternion.identity, m_LayerMask);
        eButton.gameObject.SetActive(collided);
    }
    
    //Debug
    void OnDrawGizmos()
    {
        
        Gizmos.color = Color.yellow;
        if(debug)
            Gizmos.DrawWireCube(interactionArea.position, interactionSize);
    }
    
}
