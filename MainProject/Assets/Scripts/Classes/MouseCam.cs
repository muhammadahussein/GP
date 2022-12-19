using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCam
{
    public GameObject camPivot;
    Character player;

    public float Height = 1.3f;
    public float Distance = 3;
    public float Smoothness = 15;
    GameObject cam = simpleControl.Instance.gameplayCamera;
    public Quaternion rotation;
    public MouseCam(Character t)
    {
        player = t;
        camPivot = new GameObject();
        camPivot.transform.position = player.transform.position + Vector3.up * Height;
        camPivot.transform.rotation = player.transform.rotation;
        camPivot.name = "camPivot";
        //camPivot.transform.parent = player.transform;

        resetCamera();

        Cursor.lockState = CursorLockMode.Locked;
    }
    public float lookSpeed = 2;
    float x, y;

    float finalDistance;

    public bool canControlCamera = true;
    public void UpdateCam() // Look rotation (UP down is Camera) (Left right is Transform rotation)
    {
        
        camPivot.transform.position = Vector3.Lerp(camPivot.transform.position,
            player.transform.position+Vector3.up*Height, 15*Time.deltaTime);

        if (canControlCamera)
        {
            x += Input.GetAxis("Mouse X") * lookSpeed;
            y += -Input.GetAxis("Mouse Y") * lookSpeed;

            y = Mathf.Clamp(y, -30, 80);
            rotation = camPivot.transform.rotation;
            camPivot.transform.eulerAngles = new Vector3(y, x, 0);
        }

        Vector3 camForward = camPivot.transform.rotation * Vector3.forward;

        RaycastHit hit;
        //if (Physics.Raycast(camPivot.transform.position, -camForward, out hit, Distance, player.rayLayerMasks))
        if (Physics.SphereCast(camPivot.transform.position, 0.5f, -camForward, out hit, Distance+0.5f, player.rayLayerMasks))
            finalDistance = hit.distance;
        else
            finalDistance = Distance;
        
        cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, 
                new Vector3(0,0,-finalDistance), 10 * Time.deltaTime);
    }
    public void UpdateTarget()
    {
        //player.SetTargetRotation(x);
    }
    static public void EnableCursor(bool isEnabled)
    {
        simpleControl.canControlCam = !isEnabled;
        Cursor.lockState = isEnabled ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isEnabled;
    }

    public void resetCamera()
    {
        cam.transform.parent = camPivot.transform;
        cam.transform.localPosition = new Vector3(0, 0, -Distance);
        cam.transform.localRotation = Quaternion.identity;
    }
    public void disableCameraMovement()
    {
        cam.transform.parent = null;
    }
}
