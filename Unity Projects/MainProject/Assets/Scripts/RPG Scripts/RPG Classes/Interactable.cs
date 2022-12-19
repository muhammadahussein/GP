using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Interactable : MonoBehaviour
{
    AudioSource AS;
    AudioClip clip;
    private void Start()
    {
        AS = gameObject.AddComponent<AudioSource>();
        clip = Resources.Load<AudioClip>("SFX/pop");
        AS.clip = clip;
        AS.volume = 0.7f;
    }
    public virtual void Interact()
    {
        if(AS)
        {
            AS.Play();
        }
    }
}
