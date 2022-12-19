using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    Animator anim;
    Quaternion target;
    bool visualise = false;
    float Ydelta;
    bool move = false;
    AvatarIKGoal[] feet = new AvatarIKGoal[] { AvatarIKGoal.LeftFoot, AvatarIKGoal.RightFoot };

    bool falling = false;
    float fallSpeed = 0;
    public Character(Animator _anim)
    {
        anim = _anim;
        target = anim.transform.rotation;
    }
    public Character(Animator _anim, bool _visualise)
    {
        anim = _anim;
        target = anim.transform.rotation;
        visualise = _visualise;
    }
    public void Update()
    {
        updateAngles();
        visualiseTarget();
    }
    void visualiseTarget()
    {
        if (visualise)
        {
            Vector3 pos = anim.transform.position + Vector3.up * 0.0f;
            Debug.DrawLine(pos, pos + target * Vector3.forward, Color.red);
            Debug.DrawLine(pos, pos + anim.transform.forward, Color.green);
        }
    }
    void updateAngles()
    {
        float angle = Vector3.SignedAngle(target * Vector3.forward, anim.transform.forward, Vector3.up);
        Ydelta = (angle / 180) + 0.5f;
        anim.SetFloat("Turn", Ydelta);
        anim.SetBool("moving", move);
    }


    public void RotateTarget(float angle)
    {
        target *= Quaternion.AngleAxis(angle, Vector3.up);
    }
    public void Move(bool _move)
    {
        move = _move;
    }
    float[] FHeights = new float[] { 0, 0 };
    float[] SFHeights = new float[] { 0, 0 };
    Quaternion SHitRot = new Quaternion();
    public void UpdateIK()
    {
        for (int i = 0; i < 2; i++)
        {
            AvatarIKGoal foot = feet[i];
            float footLength = 0.35f;
            Vector3 footPos = anim.GetIKPosition(foot);
            Quaternion footRot = anim.GetIKRotation(foot);
            Vector3 toeCast = footPos + (footRot * (Vector3.forward * footLength));
            //Vector3 toeCast = footPos;
            Vector3 rayStart = toeCast + (Vector3.up * 0.5f);
            Vector3 rayStartAnkle = footPos + (Vector3.up * 0.5f);
            RaycastHit hit, h1, h2 = new RaycastHit();

            float rayRange = 1.2f;

            if (Physics.Raycast(rayStart, -Vector3.up, out h1, rayRange)
                && Physics.Raycast(rayStartAnkle, -Vector3.up, out h2, rayRange)
                )
            {
                falling = false;
                if (h1.point.y > h2.point.y)
                    hit = h1;
                else
                    hit = h2;

                Quaternion hitRot = Quaternion.AngleAxis(Vector3.Angle(Vector3.up, hit.normal),
                    Quaternion.Inverse(anim.transform.rotation) * Vector3.Cross(Vector3.up, hit.normal));
                SHitRot = Quaternion.Slerp(SHitRot, hitRot, 15 * Time.deltaTime);

                FHeights[i] = hit.point.y - anim.transform.position.y;
                SFHeights[i] = Mathf.Lerp(SFHeights[i], FHeights[i], 15 * Time.deltaTime);

                Vector3 hitPos = toeCast + (Vector3.up * (SFHeights[i]));

                Quaternion rotInv = Quaternion.Inverse(anim.transform.rotation);

                Quaternion IKRot = footRot * SHitRot;
                Vector3 IKPos = hitPos + (IKRot * Vector3.forward * -footLength);

                anim.SetIKPosition(foot, IKPos);
                anim.SetIKRotation(foot, IKRot);
                //anim.SetIKPositionWeight(foot, Mathf.Lerp(anim.GetIKPositionWeight(foot), 1, 0.5f)) ;
                anim.SetIKPositionWeight(foot, 1);
                anim.SetIKRotationWeight(foot, 1);

                if (visualise)
                {
                    Debug.DrawLine(rayStart, h1.point, Color.white);
                    Debug.DrawLine(rayStart, h2.point, Color.white);
                    Debug.DrawLine(h1.point, rayStart - (Vector3.up * rayRange), Color.green);
                    Debug.DrawLine(h2.point, rayStartAnkle - (Vector3.up * rayRange), Color.green);
                }

            }
            else
            {
                falling = true;
            }
        }
        if (falling)
        {
            fallSpeed += -9.8f * Time.deltaTime;
            anim.transform.position += Vector3.up * fallSpeed * Time.deltaTime;
        }
        else
        {
            anim.transform.position =
                Vector3.Lerp(anim.transform.position,
                anim.transform.position + Vector3.up * (Mathf.Min(FHeights)),
                8 * Time.deltaTime);
            fallSpeed = 0;
        }
    }
}