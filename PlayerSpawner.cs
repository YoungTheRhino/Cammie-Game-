using UnityEngine;
using System.Collections;

public class PlayerSpawner : spawnbeacon {
	// Use this for initialization
	void Start () {
	
	}
    public GameObject NoCombat()
    {
        Debug.Log("No Combat");
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            player = Instantiate<GameObject>(playerprefab);

            player.GetComponent<PlayerCombatVariables>().GetData();
            playerscript = player.GetComponent<PlayerCombatScript>();
            Debug.Log("Player Spawn");
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerscript = player.GetComponent<PlayerCombatScript>();
            playerscript.StageTriggerReset();
            Debug.Log("Player relocate.");
        }
        //playerscript = player.GetComponent<PlayerCombatScript>();
        player.transform.position = this.transform.position;
        
        playerscript.incombat = false;
        playerscript.enabled = true;
        
        return player;
    }
    public override GameObject Spawn()
    {
        Debug.Log("Combat Spawn");
        base.Spawn();
        playerscript.StageTriggerReset();

        return player;
    }
    // Update is called once per frame
    void Update () {
	
	}
}
