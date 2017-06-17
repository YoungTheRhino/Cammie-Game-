using UnityEngine;
using System.Collections;

public class MushItem : Item {

    public int healAmt;

	// Use this for initialization

 
    public override void OnStart()
    {
        cost = new CraftCost[1]
            {
                new CraftCost("mushroom", 1)
            };
    }

	public override void Use(GameObject p)
    {
        
        PlayerCombatVariables var = p.GetComponent<PlayerCombatVariables>();
        var.Heal(healAmt);
    }
}
