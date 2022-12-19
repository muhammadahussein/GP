using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Character
{
    float[] FHeights = new float[] { 0, 0 };
    float[] SFHeights = new float[] { 0, 0 };

    float[] FinalHeights = new float[] { 0, 0 };
    bool[] flooredFeet = new bool[] { false, false };
    Quaternion SHitRot = new Quaternion();

    AvatarIKGoal[] feet = new AvatarIKGoal[] { AvatarIKGoal.LeftFoot, AvatarIKGoal.RightFoot };

    float bHeight = 0;



    float[] footHeights = new float[2];

    float bodyHeight;


    float ikWeight = 0;

    float leanAngle = 0;

    public int rayLayerMasks;

    public void UpdateFeetIK()
    {
        float rayHeight = 0.7f;
        float rayRange = 1.2f; //+ (-RB.velocity.y/3.5f);
        for (int i = 0; i < 2; i++)
        {
            AvatarIKGoal foot = feet[i];
            float footLength = 0.35f;
            Vector3 footPos = anim.GetIKPosition(foot);
            Quaternion footRot = anim.GetIKRotation(foot);
            Vector3 toeCast = footPos + (footRot * (Vector3.forward * footLength));
            Vector3 rayStart = toeCast + anim.deltaPosition * 2;
            rayStart.y = transform.position.y + rayHeight;
            Vector3 rayStartAnkle = footPos + anim.deltaPosition * 2;
            rayStartAnkle.y = transform.position.y + rayHeight;
            RaycastHit hit, h1, h2 = new RaycastHit();
            
            bool footRay = Physics.Raycast(rayStart, -Vector3.up, out h1, rayRange, rayLayerMasks);
            bool ankleRay = Physics.Raycast(rayStartAnkle, -Vector3.up, out h2, rayRange, rayLayerMasks);



            if (footRay || ankleRay)
            {


                hit = h1.point.y > h2.point.y ? h1 : h2;

                Quaternion hitRot = Quaternion.AngleAxis(Vector3.Angle(Vector3.up, hit.normal),
                    Quaternion.Inverse(transform.rotation) * Vector3.Cross(Vector3.up, hit.normal));
                SHitRot = Quaternion.Slerp(SHitRot, hitRot, 15 * Time.unscaledDeltaTime);

                FHeights[i] = hit.point.y - transform.position.y;


                Utilities.LinearLerp(ref SFHeights[i], FHeights[i], 3f);



                Quaternion rotInv = Quaternion.Inverse(transform.rotation);

                Quaternion IKRot = footRot * SHitRot;

                Vector3 hitPos, IKPos;
                if (h1.point.y - h2.point.y > 0.01f)
                {
                    hitPos = (Vector3.up * (SFHeights[i]));


                    hitPos += (IKRot * (Vector3.forward * -footLength));

                    IKPos = toeCast + hitPos;
                }
                else
                {
                    hitPos = Vector3.up * (SFHeights[i]);
                    IKPos = footPos + hitPos;

                }

                FinalHeights[i] = hitPos.y;



                anim.SetIKPosition(foot, IKPos);
                anim.SetIKPositionWeight(foot, ikWeight);

                if (Vector3.Dot(hit.normal, Vector3.up) > 0.5f)
                {
                    anim.SetIKRotation(foot, IKRot);
                    anim.SetIKRotationWeight(foot, ikWeight);

                }
                flooredFeet[i] = true;
            }
            else
            {
                flooredFeet[i] = false;
                SFHeights[i] = 0;
            }
            if (!flooredFeet[i])
                FinalHeights[i] = Mathf.Infinity;
        }
        if (!(flooredFeet[0] || flooredFeet[1]))
        {
            if (!falling)
            {
                falling = true;
            }
        }
        if(falling)
        { 
            bHeight = 0;
            
            
            RaycastHit h;
            float rayExtend = 1.8f;
            if (Physics.Raycast(transform.position + Vector3.up, -Vector3.up, out h, rayExtend, rayLayerMasks))
            {
                if (h.distance < rayExtend && h.distance > 1f)
                {
                    anim.SetBool("falling", false);
                    if (!jumping)
                        ikWeight = 1;
                    else
                    {
                        if (RB.velocity.y > 0)
                            Utilities.LinearLerp(ref ikWeight, 0, 20);
                        else
                            Utilities.LinearLerp(ref ikWeight, 1, 4);
                    }

                        if (anim.applyRootMotion) anim.applyRootMotion = false;
                }
                else
                {
                    falling = false;
                }
            }
            else
            {
                jumping = false;
                anim.SetBool("falling", true);
                if (anim.applyRootMotion)
                {
                    //RB.velocity = anim.velocity * 1;
                    anim.applyRootMotion = false;
                }
                
                Utilities.LinearLerp(ref ikWeight, 0, 2f);
            }
        }
        else
        {

            Utilities.LinearLerp(ref bHeight, Mathf.Min(FinalHeights), 2f);
            if (!anim.applyRootMotion && !enableRagdoll && RB.velocity.y < 0)
            {
                jumping = false;
                anim.applyRootMotion = true;
                SFHeights[0] = SFHeights[1] = 0;
            }
            
            Utilities.LinearLerp(ref ikWeight, 1, 4);


            if (anim.applyRootMotion)
            {
                /*transform.position = new Vector3(transform.position.x,
                Mathf.Lerp(transform.position.y, transform.position.y + bHeight, (9 * Time.deltaTime)),
                transform.position.z);*/
                RB.velocity = new Vector3(RB.velocity.x, bHeight*10, RB.velocity.z);

                /*
                leanAngle = Mathf.Lerp(leanAngle,
                    Mathf.Abs(
                        FinalHeights[0] < Mathf.Infinity ? FinalHeights[0] : FinalHeights[1] -
                        FinalHeights[1] < Mathf.Infinity ? FinalHeights[1] : FinalHeights[0])
                    * ((isMoving ? 1 : 0) - (Speed / 1.5f)) * 40, 10 * Time.deltaTime);

                anim.bodyRotation *= Quaternion.Euler(leanAngle, 0, 0);*/
            }

        }

    }
}
