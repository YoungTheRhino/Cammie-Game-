using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnManager : ManagerBase
{
    public GameObject currentarena;
    public BattlegroundData arenaData;
    public EnemyScript enemyAttacking;
    public CutsceneManager cutsceneMan;
    public List<GameObject> enemylist = new List<GameObject>();
    public List<GameObject> enemiesdefeated = new List<GameObject>();
    public GameObject[] currentenemies;
    public int direction;
    

    void Awake()
    {

    }
    public void SpawnDone()
    {

    }

    public void StartCombat()
    {
        player.GetComponent<PlayerCombatScript>().turnmanager = this;
        EnemyCount();
        
    }
    void Update()
    {

    }
    void EnemyCount()
    {
        enemylist.Clear();
        currentenemies = GameObject.FindGameObjectsWithTag("enemy");
        
        if (currentenemies.Length > 0)
        {
            foreach (GameObject character in currentenemies)
            {
                enemylist.Add(character);
                enemylist.Sort(delegate (GameObject a, GameObject b)
                {
                    return (a.GetComponent<EnemyVariables>().speed).CompareTo(b.GetComponent<EnemyVariables>().speed);
                });
                
            }
            StartCoroutine(EnemyTurn());

        }
        else if(currentenemies.Length == 0)
        {
            arenaData.SetState(true, false);
            player.GetComponent<PlayerCombatScript>().ChooseDir();
            Debug.Log("End Combat");
        }
    }
    public void DirectionSet(int dir)
    {
        direction = dir;

    }
    
    IEnumerator EnemyTurn()
    {
        foreach (GameObject enemy in enemylist)
        {
            if (enemy != null)
            {
                enemyAttacking = enemy.GetComponent<EnemyScript>();
                enemyAttacking.TurnChosen();
                if(enemyAttacking != null)
                yield return new WaitWhile(() => enemyAttacking.attacking);
            }
            
        }
        
            EnemyCount();
        }

    public void DeleteEnemies()
    {
        Debug.Log("DeleteEnemies");
        
    }
    public void EndCombat(int dir)
    {
        //arenaData = currentarena.GetComponent<BattlegroundData>();
        if (dir == 4 && arenaData.nextarenas[4] == null)
        {
            return;
        }
        if (enemyAttacking != null)
        {
            enemyAttacking.StopAttacking();
            StopCoroutine(EnemyTurn());
        }
        //DeleteEnemies();
        for (int i = 0; i < enemylist.Count; i++)
        {
            Debug.Log(enemylist[i] + " should be deleted");
            enemylist[i].GetComponent<EnemyScript>().Deletion();
            enemylist[i] = null;
            Debug.Log("Destroy");
        }
        currentenemies = new GameObject[0];
        enemyAttacking = null;
        Debug.Log("EndCombat");
        arenaData.SetState(false, false);
        enemylist.Clear();
        
        arenaData.ChangeArena(arenaData.CheckDirection(dir));

    }

    public void PlayerDeath()
    {
        enemyAttacking.StopAttacking();
        StopCoroutine(EnemyTurn());
    }
}