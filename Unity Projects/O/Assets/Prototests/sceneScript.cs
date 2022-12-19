using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject plane;
    public GameObject character;
    public GameObject lights;
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Charpos = character.transform.position;
        float x = Mathf.Round(Charpos.x);
        float z = Mathf.Round(Charpos.z);
        plane.transform.position = new Vector3(x, plane.transform.position.y, z);
        lights.transform.position = Vector3.Lerp(lights.transform.position ,Charpos,3*Time.deltaTime);
        lights.transform.rotation = Quaternion.Slerp(lights.transform.rotation, character.transform.rotation, 0.2f * Time.deltaTime);
    }
}
