using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : StateMachine, IEnemy
{
    public float maxHealth, damage;
    private float health;
    private Animator anim;
    public NavMeshAgent Agent;
    public Character Enemy;


    public float DetectionRadius = 30;
    LayerMask CharacterLayer;

    public Vector3 TargetPos { get; set; }
    public Vector3 InitialPos { get; set; }
    public float PlayerDist { get; set; }
    public float TargetDist { get; set; }

    public float StoppingDist = 1;
    public Vector3 PlayerPos { get; set; }
    public bool GroupSeePlayer { get; set; }
    public bool CanSeePlayer { get; set; }

    public int EnemyID { get; set; }

    public bool isVisualizing = true;

    [SerializeField] Transform sheathed, unsheathed;

    private GameObject enemyWeapon;


    private void Awake()
    {

        health = maxHealth;
        EnemyID = 0;

        agentInit();

        CharacterLayer = LayerMask.GetMask("Character");

        enemyWeapon = Resources.Load<GameObject>("Weapons/" + "EnemySword");

    }

    private void Start()
    {
        SetState(new SearchState(this));
        Enemy.Wield(ref enemyWeapon);
    }

    private void agentInit()
    {
        Agent = GetComponent<NavMeshAgent>();
        Agent.updatePosition = false;
        Agent.updateRotation = false;

        anim = GetComponent<Animator>();
        Enemy = new Character(anim);

        Enemy.runSpeed = 1f;

        InitialPos = transform.position;
        TargetPos = InitialPos;

        Enemy.Run(true);

        Enemy.sheathed = sheathed;
        Enemy.unsheathed = unsheathed;
    }
    
    void EnemyUpdate()
    {
        Enemy.SetTargetPoint(Agent.steeringTarget);
        Agent.nextPosition = transform.position;
        PlayerPos = simpleControl.Instance.transform.position;

        PlayerDist = (PlayerPos - transform.position).magnitude;
        TargetDist = (TargetPos - transform.position).magnitude;

        if(Agent.remainingDistance< StoppingDist)
            Enemy.Move(false);
        else
            Enemy.Move(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            Die();
    }

    void FixedUpdate()
    {
        EnemyUpdate();
        base.FixedUpdate();
    }

    public void EnemyAttack()
    {
        Enemy.Attack();
    }

    public void TakeDamage(float damageAmount)
    {
        Enemy.Impact();
        health -= damageAmount;
        if (health <= 0) Die();
    }
    public string DroppedItem;
    void Die()
    {
        CombatEvents.EnemyDied(this);
        Enemy.enableRagdoll = true ;
        Enemy.disableAnimator();
        GameObject dropped = Resources.Load<GameObject>("DroppedItems/" + DroppedItem);

        if(dropped)
        {
            Instantiate(dropped, transform.position, transform.rotation);
        }
        Enemy.wielded.transform.parent = null;
        Enemy.wielded.transform.GetComponent<Rigidbody>().isKinematic = false;
        Enemy.wielded.GetComponent<Collider>().enabled = true;
        Enemy.wielded.GetComponent<Collider>().isTrigger = false;
        Destroy(gameObject, 10);
        Destroy(Enemy.wielded, 10);
    }

}
