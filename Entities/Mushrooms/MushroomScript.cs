using UnityEngine;
using System.Collections;

public class MushroomScript : EnemyScript {

    public int healAmount;
    public int attackTimes = 3;
    public float normalChance = .8f;
    public int healTimes;

    public GameObject attackSpore;
    public GameObject healSpore;

	// Update is called once per frame
	public override void HPZero() {
        base.HPZero();
        
	}
    

    public override void TurnChosen()
    {

        Debug.Log("TurnChosen");
        if (gameObject.HasTag("dead"))
        {
            StartCoroutine(TurnDelay());

        }
        else
        {
            attacking = true;
            ChooseAttack();
        }
    }
     public override void ChooseAttack()
    {
        int currHeal = healTimes;
        for (int i = 0; i < attackTimes; i++)
        {
            if(Random.value < normalChance || currHeal == 0)
            {
                AttackType(0);
            }
            else
            {
                AttackType(1);
            }
        }

    }

    public void AttackType(int type)
    {
        if(type == 0)
        {
            anim.SetTrigger("Normal");
        }
        else if(type==1)
        {
            anim.SetTrigger("Heal");
        }
    }

    public void Spore(int type)
    {
        if (type==0)
        {

        }
        else if (type==1)
        {

        }
    }
    
}
