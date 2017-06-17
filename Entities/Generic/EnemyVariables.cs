using UnityEngine;
using System.Collections;

public class EnemyVariables : MonoBehaviour {
    public int health;
    public int maxHealth;
    public int speed;
    public int attack;
    public int defense;
    public bool alive = true;

    EnemyScript attackscript;
    void Start () {
        attackscript = GetComponent<EnemyScript>();
        alive = true;
	}
	
    public int TakeDamage(int damage)
    {
        if (alive)
        health = health - (damage - defense);
        health = Mathf.Clamp(health, 0, maxHealth);
        DeathCheck();
        return health;
    }
	void DeathCheck()
    {
        if (health == 0)
        {
            attackscript.HPZero();
            alive = false;
        }
    }
	void Update () {
	
	}
}
