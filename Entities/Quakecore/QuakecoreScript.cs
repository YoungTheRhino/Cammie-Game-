using UnityEngine;
using System.Collections;

public class QuakecoreScript : EnemyScript {

    public override void Start()
    {
        base.Start();
    }

    public override void TurnChosen()
    {
        base.TurnChosen();
        
    }

    public override void ChooseAttack()
    {

        Quake();
    }

    public void Quake()
    {
        //check to see if player is grounded. if true, deal damage
        StartCoroutine(combatCam.ShakeByTime(.3f, .05f));
        if (combatScript.grounded == true)
            {
                combatScript.TakeDamage(variables.attack);
            }
       
        StartCoroutine(TurnDelay());
    }

    public void RockSlide()
    {

    }
}
