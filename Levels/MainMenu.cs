using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{

    public GameObject gamestatemanager;
    Game gameManager;
    MainMenuState mainmenu;

    void Start()
    {
        gameManager = gamestatemanager.GetComponent<Game>();
        mainmenu = gamestatemanager.GetComponent<MainMenuState>();
        gameManager.pushState(mainmenu);
    }
}