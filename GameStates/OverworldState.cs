using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class OverworldState : GameState, IPausable {
    Game gameManager;
    public GameState combatState;
    public GameObject levelManager;

    LevelLoader levelLoader;
    public int overworldscene = 1;
    public int resetScene;
    public int combatscene;
    public override void Awake()
    {
        base.Awake();
        gameManager = GetComponent<Game>();
        combatState = GetComponent<CombatState>();
    }
    public override void Start()
    {
    }

    public override void Entered()
    {
        gameManager = GetComponent<Game>();
        gameManager.loadLevel(overworldscene);
    }

    void OnLevelWasLoaded(int level)
    {
        if (level == overworldscene)
        {
            levelLoader = Instantiate<GameObject>(levelManager).GetComponent<LevelLoader>();
        }
    }
    public override void GUpdate()
    {
        
    }
    public override void Leaving()
    {
        
    }

    public void LevelMenu()
    {
        levelLoader.LevelMenu();
    }
    public void EnterCombat()
    {
        
        Debug.Log("EnterCombat");
        //gameManager.popState();

       SceneManager.LoadScene(combatscene);
    }

    public void DeathReset()
    {

    }
    public void PauseGame()
    {
        
    }

    public void ResumeGame()
    {
        
    }
}
