using UnityEngine;
using System.Collections;

public class MainMenuState : GameState 
    {
    public string sceneID;
    public GameState nextState;
    public Game gameManager;
    public OverworldState overworld;

    public override void Awake()
        {
        
    }

    public override void Start()
    {
        
      
        

    }

   public override void GUpdate()
    {
       
        if (Input.GetKey("a"))
        {
            Debug.Log("enter");
            NewGame();
        }
    }
   
    public void NewGame()
    {
        overworld = GetComponent<OverworldState>();
        gameManager = GetComponent<Game>();
        Debug.Log("New Game");
        Leaving();
        gameManager.pushState(overworld);
    }
     public override void Entered()
        {
        Debug.Log("Enter");
        gameManager = GetComponent<Game>();

        }
       
     public override void Leaving()
        {
        Debug.Log("Main Menu leaving");
       
    }
    }