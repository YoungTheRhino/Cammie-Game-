using UnityEngine;
using System.Collections;

public class MainMenuCamera : MonoBehaviour {
    public GameObject gameManager;
    Game game;
	// Use this for initialization
	void Start () {
        game = gameManager.GetComponent<Game>();
        //game.InitGame();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
