using UnityEngine;
using System.Collections;

public abstract class ManagerBase : MonoBehaviour {

    public GameObject player;
    public Game gamemanager;
    public GameObject combatcamera;
    public GameObject cutsceneManager;

    public void Assign(GameObject play )
    {
        player = play;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
