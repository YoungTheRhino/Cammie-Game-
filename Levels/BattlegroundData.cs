using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BattlegroundData : MonoBehaviour, CombatBool
{

    [System.Serializable]
    public struct EnemyInfo
    {
        public GameObject enemy;
        public float spawnChance;
    }


    public GameObject[] nextarenas;
    public TurnManager turnmanager;
    public Sprite background;
    public LevelData levelManager;

    public int nextDefault = 1;
    
    public Vector3 arenaoffset;

    public GameObject player;
    public Game gamemanager;
    public GameObject combatcamera;
    public GameObject cutsceneManager;
    protected CutsceneManager cutsceneMScript;

    public PlayerSpawner spawnbeacon;
    public EnemyInfo[] enemyspawns;
    public spawnbeacon[] spawns;
    public GameObject nextarena;
    public bool nextChoice = false;

    bool active, incombat;

    GameObject leveldata;
    EnemySpawner spawner;

    public void Assign(LevelData lev, TurnManager turn, PlayerSpawner pSpawn, GameObject p, EnemySpawner eSpawn)
    {
        levelManager = lev;
        spawnbeacon = pSpawn;
        turnmanager = turn;
        player = p;
        spawner = eSpawn;
        spawner.levelData = lev;
    }
    void Start ()
    {
       
        
        
    }
	
	public virtual GameObject SetupCombat()
    {
        //gamemanager = GameObject.FindGameObjectWithTag("manager");
        spawns = GetComponentsInChildren<spawnbeacon>();
        turnmanager.arenaData = this;
        
        
        if (enemyspawns.Length == 0 || !SpawnEnemy())
        {
            player = spawnbeacon.NoCombat();
            turnmanager.player = player;
            player.GetComponent<PlayerCombatScript>().SpawnedReady(levelManager, turnmanager);
            CutsceneManager.instance.BeginFade(-1);
            SetState(true, false);
        }
        else
        {
            player = spawnbeacon.Spawn();
            turnmanager.player = player;
            player.GetComponent<PlayerCombatScript>().SpawnedReady(levelManager, turnmanager);
            CutsceneManager.instance.BeginFade(-1);
            SetState(true, true);
            turnmanager.StartCombat();
        }

        return player; //get better player return reference to level data
    }

    public void SetState(bool a, bool c)
    {
        active = a;
        incombat = c;
    }

    bool SpawnEnemy()
    {
        bool enemyspawned = false;
        float spawnNumber = 0;
        spawnNumber = Random.value;
        for (int i = 0; i < enemyspawns.Length; i++)
        {
            if (enemyspawns[i].enemy == null)
            {
                continue;
            }
            if (i == 0)
            {
                if (spawnNumber <= enemyspawns[i].spawnChance)
                {
                    spawner.SpawnEnemy(enemyspawns[i].enemy);
                    enemyspawned = true;
                    break;
                }
            }
            else
            {
                if (spawnNumber > enemyspawns[i - 1].spawnChance && spawnNumber <= (enemyspawns[i].spawnChance + enemyspawns[i - 1].spawnChance))
                {
                    spawner.SpawnEnemy(enemyspawns[i].enemy);
                    enemyspawned = true;
                    break;
                }
            }
        }
        return enemyspawned;
    }

    public void ChangeArena(int dir)
    {
        
        Debug.Log("BattlegroundData ChangeArena");
        CutsceneManager.instance.BeginFade(1);
        if(nextChoice)
        levelManager.NextArena(nextarena);
        else
        {
            levelManager.NextArena(nextarenas[1]);
        }

    }

    public int CheckDirection(int dir)
    {
        int finalDirection;
        if (nextarenas[dir] == null)
        {
            nextarena = nextarenas[nextDefault];
            Debug.Log("Default Arena");
            finalDirection = nextDefault;
        }
        else
        {
            nextarena = nextarenas[dir];
            Debug.Log("Direction Arena");
            finalDirection = dir;
        }
        return finalDirection;
    }
    public void DestroyArena()
    {
        foreach (EnemyInfo spawner in enemyspawns)
        {
            //spawner.Destroy();
        }
        Destroy(this.gameObject);
    }
}
