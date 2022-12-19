using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class simpleControl : MonoBehaviour
{
    Animator anim;

    static public bool canControlCam = false;

    [SerializeField] private Button eButton = null;

    public Character player;
    private PlayerInteraction playerInteraction = null;

    MouseCam cam;

    public GameObject gameplayCamera;

    [SerializeField] GameObject Sword;
    [SerializeField] Transform sheathed, unsheathed;

    public static simpleControl Instance;
    public void Start()
    {
        
        if(Instance == null)
            Instance = this;

        anim = GetComponent<Animator>();
        playerInteraction = gameObject.AddComponent<PlayerInteraction>();
        playerInteraction.EButton = eButton;
        player = new Character(anim);

        player.sheathed = sheathed;
        player.unsheathed = unsheathed;

        cam = new MouseCam(player);

        
    }

    private void Update()
    {
        cam.canControlCamera = canControlCam;
        cam.UpdateCam();
        if (canControlCam)
        {
            Inputs();
            StatsUI.Instance.gameObject.SetActive(true);
        }
        else
            player.Move(false);
    }

    void Inputs()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");


        Vector2 dir = new Vector2(Horizontal, Vertical);

        player.SetTargetRotation(cam.rotation);
        player.setAxis(dir);
        if (dir.magnitude > 0.1f)
        {
            player.Move(true);
        }
        else
        {
            player.Move(false);
        }

        if (Input.GetButton("Run"))
            player.Run(true);
        else
            player.Run(false);



        if (Input.GetButtonDown("Jump"))
        {
            player.Jump();
        }

        if (player.wielded)
        {
            if (Input.GetButtonDown("Attack"))
            {
                player.Attack();
            }

            if (Input.GetButtonDown("Combat"))
            {
                player.Combat(true);
            }
            if (Input.GetButtonUp("Combat"))
            {
                player.Combat(false);
            }
        }
        /*
        if (Input.GetKeyDown(KeyCode.R)) //ragdoll Test
        {
            TakeDamage(10);
            //player.enableRagdoll = !player.enableRagdoll;
        }*/

    }

    public void TakeDamage(float damageAmount)
    {
        Stat charHealth = CharacterStats.Instance.GetStat("Health");
        player.Impact();
        charHealth.RemoveValueFromStat(damageAmount);
        StatsUI.Instance.UpdateStatsUI();
        if (charHealth.StatValue <= 0) Die();
    }
    public void Die()
    {
        player.enableRagdoll = true;
        player.disableAnimator();
        Invoke("resetScene", 2);
    }

    public void resetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}