using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerVariables : MonoBehaviour
{
    public int health;
    public int maxhealth;
    public int attack;
    public int defense;

    public Text healthtext;

    public GameObject player;
    PlayerCombatScript combatscript;
    Animator anim;


    public int _health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }

    }

    void Start()
    {
       
    }
    
    public void StartCombat(GameObject combatplayer)
    {
        player = combatplayer;
        anim = player.GetComponent<Animator>();
        combatscript = player.GetComponent<PlayerCombatScript>();
        Debug.Log("PlayerVariable Start Combat");

        
    }

    public void TakeDamage(int damage)
    {
        health = health - (damage - defense);
        health = Mathf.Clamp(health, 0, maxhealth);
        healthtext.text = health.ToString();
        anim.SetInteger("Health", health);
        if (health == 0)
        {
            combatscript.Death();
        }
    }

    public void CombatEnd()
    {
        player.GetComponent<PlayerCombatVariables>();
    }

    public void SetData(int chealth, int cmaxhealth, int cattack, int cdefense)
    {
        health = chealth;
        maxhealth = cmaxhealth;
        attack = cattack;
        defense = cdefense;
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        health = Mathf.Clamp(health, 0, maxhealth);
        //heal animation/special effect trigger
    }
}
