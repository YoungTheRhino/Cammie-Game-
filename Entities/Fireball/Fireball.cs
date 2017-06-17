using UnityEngine;
using System.Collections;

public class Fireball : ProjectileBase
{
    //iTweenEvent tweenevent;
    bool updeflected = false;
    Rigidbody2D firerigidbody;
    public float archeight;
    private Transform trans;
    private Vector3 diff;
    private Vector3 halfDiff;
    Vector2 dir;

    // Use this for initialization
    public override void Start() {
        spawnbeacon = FindObjectOfType<spawnbeacon>().gameObject;
        spawnlocation = spawnbeacon.transform.position;
        firerigidbody = GetComponent<Rigidbody2D>();
        dir = spawnlocation - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        trans = gameObject.transform;
        firerigidbody.velocity = new Vector2(-1 * speed, 0);
        StartCoroutine(DestructTimer());

    }

    // Update is called once per frame
    public override void Update() {



    }

    public override void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);

        if (deflected)
        {
            if (col.gameObject.tag == "enemy")
            {
                EnemyScript takedamage = col.gameObject.GetComponent<EnemyScript>();
                takedamage.TakeDamage(direction);
                Destroy(this.gameObject);
            }
        }
    }
    /*public override void DamagePlayer(GameObject hit)
    {
        player = hit.GetComponent<PlayerCombatScript>();
        player.TakeDamage(damage);
        Destroy(this.gameObject);
    }
    */

    public override void SideDeflected()
    {
        deflected = true;
        gameObject.RemoveTag("hostile");
        gameObject.AddTag("friendly");
        firerigidbody = GetComponent<Rigidbody2D>();
        firerigidbody.velocity = new Vector2(deflectspeed, 0);
        direction = 1;

    }
    public override void UpDeflected()
    {
        base.UpDeflected();
        deflected = true;
        gameObject.RemoveTag("hostile");
		gameObject.AddTag("friendly");
		firerigidbody = GetComponent<Rigidbody2D>();
		firerigidbody.velocity = new Vector2(deflectspeed, 0);
		direction = 0;
    }
    public override void DownDeflected()
    {
        base.DownDeflected();
        deflected = true;
        gameObject.RemoveTag("hostile");
		gameObject.AddTag("friendly");
		firerigidbody = GetComponent<Rigidbody2D>();
		firerigidbody.velocity = new Vector2(deflectspeed, 0);
		direction = 2;//Destroy(this.gameObject);
    }
    public override IEnumerator DestructTimer()
    {
        yield return new WaitForSeconds(destructtime);
        Destroy(this.gameObject);
        Debug.Log("Fireball Self-Destruct");
    }

    /*public IEnumerator Arc()
    {
        Debug.Log("Fireball Arc");
        Vector2 midpoint = new Vector2((transform.position.x + owner.transform.position.x) / 2, ((transform.position.y + owner.transform.position.y) / 2) + archeight);
        while 
    }*/

}
