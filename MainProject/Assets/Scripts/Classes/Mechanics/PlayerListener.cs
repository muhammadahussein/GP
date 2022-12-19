using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListener : MonoBehaviour
{
    public Character player;
    private void Update()
    {
        player.Update();
    }
    private void LateUpdate()
    {
        player.LateUpdate();
    }
    private void OnAnimatorIK(int layerIndex)
    {
        player.UpdateIK();
    }
    private void OnCollisionEnter(Collision collision)
    {

    }
    public void jump()
    {
        player.JumpAnim();
    }

    public void sheath()
    {
        player.sheathAnim();
    }

    public void unsheath()
    {
        player.unsheathAnim();
    }
    private void OnAnimatorMove()
    {
        player.UpdateRootMotion();
    }
    public void EnableSwordTriggers()
    {
        player.EnableWeaponTriggers();
    }
    public void DisableSwordTriggers()
    {
        player.DisableWeaponTriggers();
    }

    public void SFX()
    {
        AudioSource AS = GetComponent<AudioSource>();
        AS.clip = Resources.Load<AudioClip>("SFX/footStep" + (int)Random.Range(1.5f, 2.5f));
        GetComponent<AudioSource>().Play();
    }
}
