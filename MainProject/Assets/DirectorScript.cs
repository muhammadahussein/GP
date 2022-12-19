using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DirectorScript : MonoBehaviour
{
    public static PlayableDirector director;
    void Start()
    {
        director = this.GetComponent<PlayableDirector>();
        simpleControl.canControlCam = false;
    }

    // Update is called once per frame
    bool playOnce = false;

    void Update()
    {
        if((director.state != PlayState.Playing) && !playOnce)
        {
            simpleControl.canControlCam = true;
            playOnce = true;
        }
    }
}
