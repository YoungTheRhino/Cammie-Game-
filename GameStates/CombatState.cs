using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class CombatState : GameState {
    public int overworldscene = 1;
    public int combatscene = 2;
    Game gameManager;
    public GameObject player;
    PlayerVariables playerdata;
    GameState overworldstate;
    bool firstinstance = true;

    public List<GameObject> battlegrounds;

    public override void Start()
    {
        
    }
    public override void Entered () {
        gameManager = GetComponent<Game>();
        Debug.Log("Combat Entered");
        //gameManager.loadLevel(combatscene);

    }

    void OnLevelWasLoaded(int level)
    {
        if (level == combatscene)
        {
            if (firstinstance)
            {
                Debug.Log("OnLevelLoad");
                
                playerdata = GetComponent<PlayerVariables>();
                player = GameObject.FindWithTag("Player");
                firstinstance = false;
                HealthSync();
                
            }
            if (firstinstance==false)
                    {
                Debug.Log("2nd OnLevelLoad");
                firstinstance = true;
            }

        }
    }
        public void HealthSync()
    {
        Debug.Log("HealthSync");
        playerdata.StartCombat(player);
    }
	public override void GUpdate () {
	
	}
    public void CombatEnd(string levelName)
    {
        //gameManager = GetComponent<Game>();
        player = GameObject.FindWithTag("Player");
        overworldstate = GetComponent<OverworldState>();
        playerdata = GetComponent<PlayerVariables>();
        playerdata.CombatEnd();
        Debug.Log("CombatEnd");

        SceneManager.LoadSceneAsync(levelName);
        /*
        gameManager.popState();
        gameManager.pushState(overworldstate);
        */
       
        
    }
    public override void Leaving()
    {
        
    }

}
