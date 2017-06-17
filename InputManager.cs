using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : ManagerBase {
    

    public Stack<InputLevel> inputStack = new Stack<InputLevel>();
    InputLevel currentLevel;
    public PlayInput playInput = new PlayInput();
    public PauseInput pauseInput = new PauseInput();

	void Awake () {
        playInput = gameObject.AddComponent<PlayInput>();
        pauseInput = gameObject.AddComponent<PauseInput>();

    }
	
    public void PlayerAssign(GameObject play)
    {
        player = play;
        playInput.player = player.GetComponent<PlayerCombatScript>();
        Debug.Log("PlayerAssign " + player);
    }

    public void Pause()
    {
        InputPush(pauseInput);
        Debug.Log("Push pause " + pauseInput);
    }
    public void PushPlay()
    {
        InputPush(playInput);
        Debug.Log("Push play " + playInput);
    }
    public void InputPush(InputLevel type)
    {
        Debug.Log("Push 2 play " + playInput);
        if (inputStack.Count > 0)
        {
            inputStack.Peek().Disable();
            Debug.Log(inputStack.Peek() + "Old Input");
        }
        

        Debug.Log(type + "New Input");
        inputStack.Push(type);
        inputStack.Peek().pushFunction();
        currentLevel = type;
    }

    public void InputPop()
    {
        Debug.Log(inputStack.Peek() + "will be popped.");
        inputStack.Pop();
        currentLevel = inputStack.Peek();
        currentLevel.pushFunction();
    }

    void Update()
    {
        if(currentLevel)
        {
            currentLevel.InputUpdate();

        }
    }

	
}

public class InputLevel : MonoBehaviour
{

    public virtual void pushFunction()
    {

    }

    public virtual void Disable()
    {

    }

    public virtual void InputUpdate()
    {

    }

    
}

public class PlayInput : InputLevel
{
    public PlayerCombatScript player;
    InputManager inputManager;

    void Start()
    {
        inputManager = GetComponent<InputManager>();
    }
    public void Assign(PlayerCombatScript play)
    {
        player = play;
    }
    public override void InputUpdate()
    {
        base.InputUpdate();
        
    }
    public override void pushFunction()
    {
        base.pushFunction();
        player.active = true;
    }

    public override void Disable()
    {
        base.Disable();
       player.active = false;
        Debug.Log("Player Deactivated");
    }
}

public class MenuInput : InputLevel
{
    protected Canvas menuCanvas;
    bool active;
    public void SetMenu(Canvas menu)
    {
        menuCanvas = menu;
    }

    public override void pushFunction()
    {
        base.pushFunction();
        menuCanvas.enabled = true;
        active = true;
    }

    public override void InputUpdate()
    {

    }

    public override void Disable()
    {
        base.Disable();
        menuCanvas.enabled = false;
        active = false;
    }

    
}

public class PauseInput : InputLevel
{

}