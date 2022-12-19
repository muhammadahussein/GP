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
        //Time.timeScale = 0.4f; 
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Charpos = character.transform.position;
        lights.transform.position = Vector3.Lerp(lights.transform.position ,Charpos,20*Time.deltaTime);
        lights.transform.rotation = Quaternion.Slerp(lights.transform.rotation, character.transform.rotation, 0.2f * Time.deltaTime);
    }
    private void LateUpdate()
    {
        Vector3 Charpos = character.transform.position;
        float x = Mathf.Round(Charpos.x/2)*2;
        float z = Mathf.Round(Charpos.z/2)*2;
        plane.transform.position = new Vector3(x, plane.transform.position.y, z);
    }
}
