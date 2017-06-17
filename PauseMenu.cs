using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
    public bool paused = false;
    public GameObject PauseUI;
	void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
       
    }

    void Start()
    {
        //PauseUI = this.gameObject;
        paused = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
            Pause();
    }
    public void Pause()
    {
        if (!paused)
        {
            
            this.gameObject.SetActive(true);
            Time.timeScale = 0;
            Debug.Log("Paused");
        }
        if (paused)
        {   
            this.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void Exit()
    {
        paused = false;
        this.gameObject.SetActive(false);
    }
}
