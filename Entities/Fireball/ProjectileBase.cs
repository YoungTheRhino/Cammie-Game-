using UnityEngine;
using System.Collections;

public class ProjectileBase : MonoBehaviour {
    public float speed;
    public int direction;
    public int damage;
    public float deflectspeed;
    public float destructtime;
    public GameObject owner;

    public iTween itween;
    public PlayerCombatScript player;

    public bool deflected = false;

    public PlayerVariables playervariables;
    public GameObject gmanager;

    public GameObject spawnbeacon;
    public Vector3 spawnlocation;

    // Use this for initialization
    public virtual void Start () {
        gmanager = MultiTags.FindWithMultiTag("gamemanager");
        playervariables = gmanager.GetComponent<PlayerVariables>();
        itween = GetComponent<iTween>();
        spawnbeacon = MultiTags.FindWithMultiTag("beacon");
        spawnlocation = spawnbeacon.transform.position;
    }
	
	// Update is called once per frame
	public virtual void Update () {
	
	}

    public virtual void SideDeflected()
    {

    }

    public virtual void UpDeflected()
    {

    }

    public virtual void DownDeflected()
    {

    }
    public virtual IEnumerator DestructTimer()
    {
        yield return new WaitForSeconds(destructtime);
    }

    public virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (!deflected)
        {
            if (col.gameObject.tag == "Player")
            {

                Debug.Log("Player hIT");
                DamagePlayer(col.gameObject);

            }

            if (col.gameObject.tag == "deflect")
            {
                if (col.GetComponent<TailScript>().direction == 0)
                {
                    UpDeflected();
                }
                if (col.GetComponent<TailScript>().direction == 1)
                {
                    SideDeflected();
                }
                if (col.GetComponent<TailScript>().direction == 2)
                {
                    DownDeflected();
                }
            }
        }
    }

    public virtual void DamagePlayer(GameObject hit)
    {
        player = hit.GetComponent<PlayerCombatScript>();
        player.TakeDamage(damage);
        Destroy(this.gameObject);
    }

}
