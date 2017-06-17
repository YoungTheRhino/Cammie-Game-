using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public struct CraftCost
{
    public string resource;
    public int cost;

    public CraftCost(string r, int c)
    {
        resource = r;
        cost = c;
    }
}
public abstract class Item : Thing {

    ItemIcon UIicon;

    public GameObject player;

    public CraftCost[] cost;
	
    void Start()
    {
        OnStart();
    }
    public virtual void OnStart()
    {
        //cost = new CraftCost[0];
        cost = new CraftCost[1]
            {
                new CraftCost("mushroom", 1)
            };
    }

    public void Assign(ItemIcon icon)
    {
        UIicon = icon;
    }
    public virtual void Use(GameObject p)
    {

    }
    public void Hover()
    {
        UIicon.Hover();
        foreach (CraftCost c in cost)
        {
            
            Inventory.resources[c.resource].ui.Enlarge();
        }
    }

}
