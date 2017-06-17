using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

    public static Stack<GameState> gameStack = new Stack<GameState>();
    public GameState currentState ;
    public static Game instance;

    private string sceneID;
    public string SceneID
    {

        get
        {
            return sceneID;
        }
        set
        {
            sceneID = value;
        }
    }
    void Awake()
    {
        DontDestroyOnLoad(this);            //singleton pattern
        Game gamemanager = Game.instance;

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        
    }

    void Start()
    {
        //InitGame();
    }

    public void Update()
    {
        currentState = gameStack.Peek();
        if (currentState != null)
        { currentState.GUpdate(); }
    }

    public void InitGame()
    {
        Debug.Log("Initgame");
        MainMenuState mainmenu = GetComponent<MainMenuState>();
        pushState(mainmenu);

    }
   
    
    
    
    public void pushState(GameState newstate)
    {
        currentState = newstate;  
      gameStack.Push(currentState);
        currentState.Entered();


    }
    public void PauseGame()
    {
        GameState currentstate = gameStack.Peek();
        
    }
    public void loadLevel(int sceneID)
    {
        Debug.Log("Loaded");
        SceneManager.LoadScene(sceneID);
    }
    public void popState()
    {  
       GameState previousState = gameStack.Pop();
        previousState.Leaving();
    }
}





