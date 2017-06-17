using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LevelLoader : MonoBehaviour {
    public GameObject gamemanager;
    OverworldState overworld;
    bool loading;
    
    public Canvas levelmenu;
	


	void Start () {
        levelmenu = GetComponentInChildren<Canvas>();
        }
            
	
    public void LevelMenu()
    {
        loading = true;
        Time.timeScale = 0;
        levelmenu.enabled=true;

    }

    public void ExitMenu()
    {
        loading = false;
        levelmenu.enabled = false;
        Time.timeScale = 1;
    }
    
    public void LevelButton(string levelname)
    {

        StartCoroutine(LoadLevel(levelname));
    }
    public IEnumerator LoadLevel(string levelname)
    {
        Time.timeScale = 1;
        AsyncOperation async = SceneManager.LoadSceneAsync(levelname);
        while (!async.isDone)
        {
            yield return null;
        }

    }

	void Update ()
    {
        for (int i = 0; i < Input.touchCount; ++i)
        {
            Vector2 test = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                test = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);

                if ((Physics2D.Raycast(test, (Input.GetTouch(i).position)).collider.tag == "enemy") && !loading)
                {
                    LevelMenu();
                }
         
            }
        }
    }
}
