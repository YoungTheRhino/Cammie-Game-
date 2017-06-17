using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombItem : Item {

    
    public override void OnStart()
    {
        cost = new CraftCost[1]
            {
                new CraftCost("shell", 1)
            };
    }
    public override void Use(GameObject p)
    {
        base.Use(p); //Inflict damage and/or change environment
    }
    // Update is called once per frame
    void Update () {
		
	}
}
