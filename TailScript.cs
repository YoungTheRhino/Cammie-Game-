using UnityEngine;
using System.Collections;

public class TailScript : MonoBehaviour {
    PlayerCombatScript player;
    public int direction;

	void Start () {
        player = GetComponentInParent<PlayerCombatScript>();
        player.tail = this;
	}
	
    public void Deflect(int dir)
    {
        GetComponent<BoxCollider2D>().enabled = true;
        direction = dir;
    }
	
    public void DeflectReset()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }
	void Update () {
	
	}
}
