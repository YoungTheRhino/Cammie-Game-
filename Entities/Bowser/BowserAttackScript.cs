using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;


public class BowserAttackScript : EnemyScript {
    public GameObject fireball;
    public List<GameObject> activefireballs;
    public int fireballnumber;
    public int fireballstock;
    public float firedelay;

    public float fireChance;
    public float quakeChance;

    public float fireballspeed;

    public Fireball firevariables;
    
    public override void Start()
    {
        base.Start();
        //anim = GetComponent<Animator>();
        firevariables = fireball.GetComponent<Fireball>();
        fireballstock = fireballnumber;
    }


    public override void TurnChosen()
    {
        fireballstock = fireballnumber;
        base.TurnChosen();
       
    }

    public override void ChooseAttack()
    {
        FireAttack();
    }
    void Quake()
    {
        
        Debug.Log(combatCam);
        StartCoroutine(combatCam.ShakeByTime(1.0f, 2));
        if (combatScript.grounded == true)
        {
            combatScript.TakeDamage(variables.attack);
        }

        StartCoroutine(TurnDelay());
    }

    public void FireAttack()
    {
        reward = resources[0];
        anim.SetTrigger("Attack");
    }

    public override void TakeDamage(int dir)
    {
        base.TakeDamage(dir);
        reward = resources[0];
        Debug.Log("Enemy Take Damage");
    }

    public void Fireball()
    {
        
        GameObject fireballclone = Instantiate(fireball, transform.position, transform.rotation) as GameObject;
        activefireballs.Add(fireballclone);
        fireballstock = fireballstock - 1;
        Fireball fireballstats = fireballclone.GetComponent<Fireball>();
        fireballstats.speed = fireballspeed;
        fireballstats.owner = gameObject;
        fireballstats.damage = variables.attack;
       
    }
    public void FireAttackDone()
    {
        if (attacking)
        {
            if (fireballstock > 0)
            {
                //fireballstock = fireballstock - 1;
                FireAttack();
            }
            if (fireballstock == 0)
            {
                Debug.Log("FireAttackDone");
                StartCoroutine(TurnDelay());
            }
        }
    }
    public override void StopAttacking()
    {
        anim.SetTrigger("Stop");
        base.StopAttacking();
    }
    public override void TurnEnded()
    {
        base.TurnEnded();
        Debug.Log("Bowser Turn Ended");
    }
    
    public override IEnumerator TurnDelay()
    {
        while(activefireballs.Count > 0)
        {
            for (var i = activefireballs.Count - 1; i > -1; i--)
            {
                if (activefireballs[i] == null)
                    activefireballs.RemoveAt(i);
            }
            yield return null; 
        }
        yield return StartCoroutine(base.TurnDelay());
    }

    public IEnumerator FireDelay()
    {
        yield return new WaitForSeconds(firedelay);
    }
    public override void HPZero()
    {
            base.HPZero();
        
    }
    public override void EnemyDeath()
    {
        base.EnemyDeath();
        
    }

    public void BowserDeath()
    {
        EnemyDeath();
    }
}
