using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public abstract class GameState : MonoBehaviour
{
    Game gamemanager;

public virtual void Awake()
    {
        gamemanager = GetComponent<Game>();
    }
   public virtual void Start()
    {
        //gamemanager.pushState(this);
    }
    public virtual void Entered()
    {

    }
    public virtual void Leaving()
    {

    }

    public virtual void InputFunction()
    {

    }
    public virtual void GUpdate()
    {

    }
    public virtual void OnGUI()
    {
    }
}

   