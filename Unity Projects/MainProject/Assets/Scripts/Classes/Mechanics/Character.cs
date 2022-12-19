using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Character
{
    float Responsiveness = 6f; //higher = more responsive = more "robotic"
    float jumpForce = 5;

    Vector2 axis = new Vector2(0,1);

    public bool isAttacking;
    public bool isDefending;

    Animator anim;
    Rigidbody RB;
    Quaternion target;

    public bool Agent = false;

    CapsuleCollider characterCollider;


    public bool enableRagdoll = false;


    bool falling = true;
    bool jumping = false;

    public Transform transform;


    Transform[] Bones;
    Transform[] RagdollBones;
    ConfigurableJoint[] joints;
    Quaternion[] initialRotations;

    public GameObject rightHandWeapon;

    public float ragdollStrength = 2;

    public Character(Animator _anim)
    {
        anim = _anim;
        RB = anim.GetComponent<Rigidbody>();

        target = anim.transform.rotation;
        transform = anim.transform;

        characterCollider = anim.GetComponent<CapsuleCollider>();

        PlayerListener pl = anim.gameObject.AddComponent<PlayerListener>();
        pl.player = this;

        bodyHeight = transform.position.y;


        CreateRagdoll();
        int characterLayer = 1 << LayerMask.GetMask("Character");
        int bonesLayer = 1 << LayerMask.GetMask("StaticBones");
        int rigidbodiesLayer = 1 << LayerMask.GetMask("Rigidbodies");
        rayLayerMasks = characterLayer | bonesLayer | rigidbodiesLayer;
    }
    Transform root;
    void CreateRagdoll()
    {
        List<Transform> bones = new List<Transform>();
        foreach (CapsuleCollider bone in transform.GetComponentsInChildren<CapsuleCollider>())
        {
            if (bone.transform != transform)
            {
                bones.Add(bone.transform);
                bone.transform.gameObject.layer = 11;
                bone.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
        Bones = bones.ToArray();
        RagdollBones = new Transform[Bones.Length];
        initialRotations = new Quaternion[Bones.Length];
        targetRotations = new Quaternion[Bones.Length];
        debugRotations = new Quaternion[Bones.Length];
        targetPositions = new Vector3[Bones.Length];

        root = GameObject.Instantiate(Bones[0].parent);
        root.name = "ragdoll";
        root.transform.position = Bones[0].parent.position;
        root.transform.rotation = Bones[0].parent.rotation;

        Vector3[] anchors = new Vector3[Bones.Length];
        joints = new ConfigurableJoint[Bones.Length];

        CapsuleCollider[] capcols = root.GetComponentsInChildren<CapsuleCollider>();

        for (int i = 0; i < capcols.Length; i++)
        {
            RagdollBones[i] = capcols[i].transform;
            RagdollBones[i].GetComponent<CapsuleCollider>().isTrigger = false;
            RagdollBones[i].gameObject.layer = 9;
            RagdollBones[i].GetComponent<Rigidbody>().isKinematic = false;
            ConfigurableJoint j;
            if (j = RagdollBones[i].GetComponent<ConfigurableJoint>())
            {
                joints[i] = j;
                anchors[i] = j.anchor;
                initialRotations[i] = RagdollBones[i].localRotation;
                JointDrive drive = joints[i].slerpDrive;
                drive.positionSpring = ragdollStrength;
                joints[i].slerpDrive = drive;
            }
        }

        root.localScale = Bones[0].lossyScale;


        for (int i = 0; i < RagdollBones.Length; i++)
        {
            if (joints[i])
            {
                joints[i].anchor = anchors[i];
            }
        }
        root.gameObject.SetActive(false);
    }

    public void Update()
    {
        updateAngles();
        updateSpeed();


        if (Agent) UpdateAgent();
    }
    public void LateUpdate()
    {
        updateRagdoll();
    }
    public void UpdateIK()
    {
        UpdateFeetIK();
        IKPassData();
    }
    public void UpdateRootMotion()
    {
        if (anim.applyRootMotion)
        {
            Vector3 rootMotion = anim.deltaPosition;

            Vector3 currentVelocity = RB.velocity;

            Vector3 animatorVelocity = rootMotion / Time.deltaTime;

            animatorVelocity.y = currentVelocity.y;

            RB.velocity = animatorVelocity;

            Vector3 animatorAngularVelocity = anim.angularVelocity;
            RB.angularVelocity = animatorAngularVelocity;
        }
    }
    public void updateRagdoll()
    {
        if (enableRagdoll)
        {
            if (characterCollider.enabled)
            {
                root.gameObject.SetActive(true);
                for (int i = 0; i < Bones.Length; i++)
                {
                    RagdollBones[i].position = Bones[i].position;
                    RagdollBones[i].rotation = Bones[i].rotation;
                    RagdollBones[i].GetComponent<Rigidbody>().velocity = anim.velocity;
                }
                anim.applyRootMotion = false;
                RB.isKinematic = true;
                characterCollider.enabled = false;
            }
            for (int i = 0; i < Bones.Length; i++)
            {
                Bones[i].position = RagdollBones[i].position;
                Bones[i].rotation = RagdollBones[i].rotation;
                if (joints[i])
                {
                    SetTargetRotationInternal(joints[i], targetRotations[i], initialRotations[i]);
                }
            }
            transform.position = Vector3.Lerp(transform.position, RagdollBones[0].position, 5 * Time.deltaTime);
        }
        else
        {
            if (!characterCollider.enabled)
            {
                root.gameObject.SetActive(false);
                for (int i = 0; i < Bones.Length; i++)
                {
                    if (joints[i])
                        initialRotations[i] = RagdollBones[i].localRotation;
                }
                anim.applyRootMotion = true;
                RB.isKinematic = false;
                characterCollider.enabled = true;
            }
        }
    }


    float Ydelta = 0f;
    bool move = false;
    bool run = false;
    public bool isMoving = false;
    public bool isRunning = false;
    public float rotationSpeed = 2;
    public float runSpeed = 1;
    void updateAngles()
    {
        float maxBlend = 0;
        if (isMoving)
            maxBlend = Responsiveness * Time.deltaTime;
        else
            maxBlend = 100;
        float YdeltaClamped = Ydelta;

        float angle =
            Vector3.SignedAngle(target * Vector3.forward, transform.forward, Vector3.up);
        Ydelta = (angle / (90 / rotationSpeed));

        float diff = Ydelta - YdeltaClamped;
        Ydelta = YdeltaClamped + Mathf.Clamp(diff, -maxBlend, maxBlend);

        anim.SetFloat("Turn", Ydelta);
    }


    float Speed = 0;
    void updateSpeed()
    {

        if (run)
        {
            if (!move)
            {
                Speed = runSpeed;
            }
            else
            {
                Utilities.LinearLerp(ref Speed, runSpeed, 3);
            }
        }
        else
        {
            Utilities.LinearLerp(ref Speed, 0, 1f);
        }
        anim.SetFloat("Speed", Speed);
        anim.SetBool("moving", move);

        isMoving = move;
        isRunning = run;
    }

    public void setAxis(Vector2 vector)
    {
        if (vector.magnitude > 0)
            axis = combat ? Vector2.Lerp(axis, vector, 8 * Time.deltaTime) : vector;
        //else axis = Vector2.Lerp(axis, new Vector2(0, 1), 2 * Time.deltaTime);
    }
    public void RotateTarget(float angle)
    {
        target *= Quaternion.AngleAxis(angle, Vector3.up);
    }

    Vector3 targetPosition;

    public void UpdateAgent()
    {
        if (targetPosition != null)
        {
            if ((transform.position - targetPosition).magnitude < 0.5f)
            {
                SetTargetPoint(targetPosition);
                Move(false);
            }
            else
            {
                Move(true);
            }
        }
    }
    public void SetTargetPoint(Vector3 point)
    {

        targetPosition = point;
        target = Quaternion.LookRotation((point - transform.position).normalized);
    }

    public void SetTargetRotation(Quaternion rotation)
    {
        
        Vector3 euler = rotation.eulerAngles;
        rotation = Quaternion.Euler(0, euler.y, euler.z);
        if (!combat)
        {
            //target = isMoving ? rotation * Quaternion.LookRotation(new Vector3(axis.x, 0, axis.y)) : rotation;
            target = rotation * Quaternion.LookRotation(new Vector3(axis.x, 0, axis.y));
        }
        else
        {
            target = rotation;
            anim.SetFloat("StrafeX", axis.x);
            anim.SetFloat("StrafeY", axis.y);
        }
        //Debug.DrawRay(transform.position+Vector3.up, target * Vector3.forward, Color.red);
    }


    public void Move(bool _move)
    {
        move = _move;
    }
    public void Run(bool _run)
    {
        run = _run;
    }


    void IKPassData()
    {
        for (int i = 0; i < Bones.Length; i++)
        {
            targetRotations[i] = Bones[i].localRotation;
            debugRotations[i] = Bones[i].rotation;
            targetPositions[i] = Bones[i].position;
        }
    }
    Quaternion[] targetRotations;
    Quaternion[] debugRotations;
    Vector3[] targetPositions;

    void SetTargetRotationInternal(ConfigurableJoint joint, Quaternion targetRotation, Quaternion startRotation)
    {
        Vector3 right = joint.axis;
        Vector3 forward = Vector3.Cross(joint.axis, joint.secondaryAxis).normalized;
        Vector3 up = Vector3.Cross(forward, right).normalized;
        Quaternion worldToJointSpace = Quaternion.LookRotation(forward, up);

        Quaternion resultRotation = Quaternion.Inverse(worldToJointSpace);

        resultRotation *= Quaternion.Inverse(targetRotation) * startRotation;

        resultRotation *= worldToJointSpace;
        joint.targetRotation = resultRotation;
    }

    public void Jump()
    {
        if (!jumping && !falling)
        {
            anim.SetTrigger("Jump");
        }
    }

    public void JumpAnim()
    {
        if (!jumping)
        {
            jumping = true;
            anim.applyRootMotion = false;
            falling = true;
            RB.velocity =
            new Vector3(RB.velocity.x, jumpForce, RB.velocity.z);
        }
    }

    public void Attack()
    {

        if (combat)
            anim.SetTrigger("Attack");
        else
            Combat(true);
    }

    bool combat = false;
    public void Combat(bool b)
    {
        if (!falling)
        {
            anim.SetBool("Combat", b);
            combat = b;
            axis = new Vector2(0, 1);
        }
    }
    public void Impact()
    {
        anim.SetTrigger("Impact");
        AudioSource AS = transform.GetComponent<AudioSource>();
        AS.clip = Resources.Load<AudioClip>("SFX/impact");
        AS.Play();
        GameObject hitVFX = GameObject.Instantiate(Resources.Load<GameObject>("Hit VFX"));
        hitVFX.transform.position = wielded.transform.position + wielded.transform.forward;
        GameObject.Destroy(hitVFX, 1.5f);
        //GameObject hitVFX = GameObject.Instantiate(Resources.Load<GameObject>("HitVFX"));
    }

    public GameObject wielded;
    public Transform sheathed, unsheathed;
    public void Wield(ref GameObject weapon)
    {
        Transform prevParent = combat ? unsheathed : sheathed;
        if (wielded)
        {
            prevParent = wielded.transform.parent;
            GameObject.Destroy(wielded);

        }
        wielded = GameObject.Instantiate(weapon);
        wielded.transform.parent = prevParent;
        wielded.transform.localPosition = Vector3.zero;
        wielded.transform.localRotation = Quaternion.identity;
        weapon = wielded;
        DisableWeaponTriggers();
    }
    public void unsheathAnim()
    {
        if (wielded)
        {
            wielded.transform.parent = sheathed;
            wielded.transform.localPosition = Vector3.zero;
            wielded.transform.localRotation = Quaternion.identity;

            AudioSource swordAS = wielded.GetComponent<AudioSource>();
            swordAS.clip = Resources.Load<AudioClip>("SFX/sheath");
            swordAS.Play();
        }
    }
    public void sheathAnim()
    {
        if (wielded)
        {
            wielded.transform.parent = unsheathed;
            wielded.transform.localPosition = Vector3.zero;
            wielded.transform.localRotation = Quaternion.identity;
            AudioSource swordAS = wielded.GetComponent<AudioSource>();
            swordAS.clip = Resources.Load<AudioClip>("SFX/unsheath");
            swordAS.Play();
        }
    }
    public void EnableWeaponTriggers()
    {
        if (wielded)
        {
            wielded.GetComponent<Collider>().enabled = true;
            AudioSource swordAS = wielded.GetComponent<AudioSource>();
            swordAS.clip = Resources.Load<AudioClip>("SFX/swoosh");
            swordAS.Play();
        }
    }
    public void DisableWeaponTriggers()
    {
        if (wielded)
            wielded.GetComponent<Collider>().enabled = false;
    }


    public void disableAnimator()
    {
        anim.enabled = false;
    }
}