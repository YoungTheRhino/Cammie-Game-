using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    public bool attacking = false;
    public PlayMakerFSM enemyFSM;
    public Animator anim;
    public EnemyVariables variables;
    public bool alive = true;
    public string[] resources;
    public string reward;

    public int versionNumber;
    public Text healthtext;

    LevelData levelData;

    public float delaytime = .5f;
    public GameObject gameManager;
    public GameObject player;
    public PlayerCombatVariables playervariables;
    protected PlayerCombatScript combatScript;
    public CutsceneManager cutsceneMan;

    public Inventory inventory;
    public int playerdamage;
    public TurnManager turnmanager;
    public int deflectdir;
    public CombatCamera combatCam;

    public void AssignLevData(LevelData lev)
    {
        levelData = lev;
    }
    public virtual void Start () {

        anim = GetComponent<Animator>();
        variables = GetComponent<EnemyVariables>();
        gameManager = MultiTags.FindWithMultiTag("gamemanager");
        player = GameObject.FindGameObjectWithTag("Player");
        playervariables = player.GetComponent<PlayerCombatVariables>();
        combatScript = player.GetComponent<PlayerCombatScript>();
        turnmanager = GameObject.FindGameObjectWithTag("turn").GetComponent<TurnManager>();
        cutsceneMan = turnmanager.cutsceneMan;
        combatCam = CutsceneManager.instance.combatCamScript;

    }

    public virtual void TakeDamage(int dir)
    {
        //playervariables = player.GetComponent<PlayerVariables>();
        deflectdir = dir;
        playerdamage = playervariables.attack;
        variables.TakeDamage(playerdamage);
        turnmanager.direction = dir;
        anim.SetTrigger("EnemyHit");
    }
    // Update is called once per frame
    public virtual void TurnChosen()
    {
        variables = GetComponent<EnemyVariables>();
        Debug.Log("TurnChosen");
        if (gameObject.HasTag("dead"))
        {
            StartCoroutine(TurnDelay());
            
        }
        else
        {
            attacking = true;
            ChooseAttack();
        }

    }

    public virtual void ChooseAttack()
    {

    }
    public virtual void StopAttacking()
    {
        attacking = false;
    }
    public virtual void TurnEnded()
    {
        attacking = false;
    }
    public virtual IEnumerator TurnDelay()
    {
        yield return new WaitForSeconds(delaytime);
        if (alive ==false)
        {
            this.gameObject.tag = "dead";
        }
        TurnEnded();
    }
    public virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "friendly")
        {
            //TakeDamage();
        }
    }
    public virtual void HPZero()
    {
        alive = false;
        
        if (attacking)
        {
            StartCoroutine(TurnDelay());
        }
        anim.SetTrigger("End");
        anim.SetBool("Dead", true);
        StartCoroutine(FadeTo(0.0f, 1.0f));
        turnmanager.DirectionSet(deflectdir);
        //this.gameObject.tag = "dead";

    }

    public virtual void EnemyDeath()
    {
        inventory = levelData.GetComponent<Inventory>();
        inventory.AddResource(reward, 1);
        this.gameObject.tag = "dead";
    }
    public void Deletion()
    {
        DestroyImmediate(this.gameObject);
    }
    public virtual IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = GetComponent<SpriteRenderer>().material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            GetComponent<SpriteRenderer>().material.color = newColor;
            yield return null;
        }
    }
    public virtual void PlayerDeath()
    {

    }
}
