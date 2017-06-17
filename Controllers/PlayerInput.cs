using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {
    bool grounded;

    public GameObject gamemanager;
    OverworldState overworld;
    public float movespeed;
    float movehorizontal;

    PlayerAnimation animationscript;

    void Start()
    {
        gamemanager = GameObject.Find("GManager");
        animationscript = GetComponent<PlayerAnimation>();
        overworld = gamemanager.GetComponent<OverworldState>();
    }
    void FixedUpdate () {
        Movement();
	}
    void Movement()
    {
      movehorizontal = Input.GetAxisRaw("Horizontal");
      Vector3 xmovement = new Vector3 (movehorizontal * movespeed, 0, 0);
        this.transform.Translate(xmovement);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("OnCollisionEnter");
        if (col.gameObject.tag == "enemy")
        {
            Debug.Log("Enemy Collision");
            overworld.LevelMenu();
            
        }
        
    }
}
