using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerCombatVariables : MonoBehaviour {
    public int health;
    public int maxhealth;
    public int attack;
    public int defense;



    public DeathCamera deathcamera;

    public Text healthtext;
    public GameObject gamemanager;
    public PlayerVariables outervariables;
    PlayerCombatScript combatscript;
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        combatscript = GetComponent<PlayerCombatScript>();
        gamemanager = GameObject.FindGameObjectWithTag("manager");
        outervariables = gamemanager.GetComponent<PlayerVariables>(); // mandatory for enemies to spawn for some reason
        deathcamera = GetComponentInChildren<DeathCamera>();


    }
	// Use this for initialization
	void Start () {

        GetData();
        anim.SetFloat("Health", health);
        healthtext.text = health.ToString();
	}

    public void GetData()
    {
        health = outervariables.health;
        maxhealth = outervariables.maxhealth;
        attack = outervariables.attack;
        defense = outervariables.defense;
    }



    public void TakeDamage(int damage)
    {
        health = health - (damage - defense);
        health = Mathf.Clamp(health, 0, maxhealth);
        healthtext.text = health.ToString();
        anim.SetInteger("Health", health);
        DeathCheck();
    }

    public void Heal(int healnumber)
    {
        health += healnumber;
        health = Mathf.Clamp(health, 0, maxhealth);
        healthtext.text = health.ToString();
        anim.SetInteger("Health", health);
    }
	// Update is called once per frame
	void Update () {
	
	}

    void DeathCheck()
    {
        if (health == 0)
        {
            Death();
        }
    }
    void Death()
    {
        Debug.Log("PlayerCombatVariables Death");
        StartCoroutine(combatscript.Death());
    }
    public void EndCombat()
    {
        outervariables.SetData(health, maxhealth, attack, defense);
        Debug.Log("SetData");
    }
}
