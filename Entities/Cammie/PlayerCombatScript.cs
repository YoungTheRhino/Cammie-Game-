using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum BattleState
{
    idle = 1,
    swing = 2,
    jumping = 3,
    hurt = 4
}
public class PlayerCombatScript : MonoBehaviour {
    public Animator anim;
    Game gamemanager;
    public PlayerCombatVariables variables;
    public TurnManager turnmanager;

    Rigidbody2D rigid;
    public BattleState battlestates = BattleState.idle;

  
    public bool alive = true;
    public bool incombat = false;
    public bool active = true;
    public TailScript tail;
    public bool grounded;

    public LevelData leveldata;
    InputManager inputManager;
    Inventory inventory;

    public float jumpforce;
    public float holdJumpFloat;
    public float holdHeight;
    public bool heldKey;

    public Camera dCamera;
    DeathCamera deathCamera;
    bool jumpQueue;

    public string inventoryKey = "f";

    string[] stageTriggers = new string[] { "StageUp", "StageRight", "StageDown", "StageJump", "StageLeft" };

    void Start() {
        alive = true;
        dCamera = GetComponentInChildren<Camera>();
        deathCamera = GetComponentInChildren<DeathCamera>();
        variables = GetComponent<PlayerCombatVariables>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        tail = GetComponentInChildren<TailScript>();

    }

    public void SpawnedReady(LevelData lev, TurnManager turn)
    {
        leveldata = lev;
        inputManager = leveldata.GetComponent<InputManager>();
        turnmanager = turn;
        inventory = leveldata.GetComponent<Inventory>();
        inputManager.PlayerAssign(this.gameObject);
    }

    public void StageTriggerReset()
    {
        for (int i = 0; i < stageTriggers.Length; i++)
        {
            anim.SetBool(stageTriggers[i], false);
        }
    }
    // Update is called once per frame
    void Update() {
        if (alive && active)
        {
            if (battlestates == BattleState.idle)
            {
                if (Input.GetKeyDown("w"))
                {
                    if (incombat)
                    {
                        DeflectUp();
                    }
                    else
                    {
                        EndCombat(0);
                        turnmanager.EndCombat(0);

                    }
                }
                else if (Input.GetKeyDown("s"))
                {
                    if (incombat)
                    {
                        DeflectDown();
                    }
                    else
                    {
                        EndCombat(2);
                        turnmanager.EndCombat(2);

                    }
                }
                else if (Input.GetKeyDown("d"))
                {
                    if (incombat)
                    {
                        battlestates = BattleState.swing;
                        DeflectRight();
                    }
                    else
                    {
                        EndCombat(1);
                        turnmanager.EndCombat(1);

                    }
                }

                else if (Input.GetKeyDown("a"))
                {
                    ExitLeft();
                }
                else if (Input.GetKeyDown("space"))
                {
                    if (incombat)
                    {
                        Jump();
                    }
                    else
                    {
                        EndCombat(3);
                        turnmanager.EndCombat(3);

                    }

                }

            }

            else if (battlestates == BattleState.jumping)
            {
                if (Input.GetKeyUp("space"))
                {
                    heldKey = false;
                }
            }
        }
    }

    public void DeflectRight()
    {
        battlestates = BattleState.swing;
        anim.SetTrigger("TailSide");
        //tail.Deflect(1);
        Debug.Log("Tailside");
    }
    public void DeflectUp()
    {
        battlestates = BattleState.swing;
        anim.SetTrigger("TailSide");
        //anim.SetTrigger("TailUp");
        
        Debug.Log("Tailup");
        //tail.Deflect(0);
    }
    public void DeflectDown()
    {
        battlestates = BattleState.swing;
        anim.SetTrigger("TailSide");
        //anim.SetTrigger("TailDown");
        //tail.Deflect(2);
        Debug.Log("Taildown");
    }

    void FixedUpdate()
    {
        if (alive && battlestates == BattleState.idle && active)
        {
            if(Input.GetKeyDown(inventoryKey))
            {
                ResetStats();
                inventory.MenuOn(incombat);
            }
        }
        else if (alive &&  battlestates == BattleState.jumping && active)
        {
            if (Input.GetKey("space"))
            {
                holdJumpFloat -= .1f;
                holdJumpFloat = Mathf.Clamp(holdJumpFloat, 0, 1);
            }
            
        }
        if (grounded && jumpQueue)
        {
            battlestates = BattleState.jumping;
            anim.ResetTrigger("TailSide");
            anim.ResetTrigger("Jump");
            anim.SetTrigger("Jump");
            
            rigid.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
            jumpQueue = false;
        }
            else if(!grounded && holdJumpFloat > 0 && heldKey)
        {
            rigid.AddForce(Vector2.up * holdHeight, ForceMode2D.Impulse);
            Debug.Log("hold jump");
        }
        
    }
    public void Jump()
    {
        
        if (grounded == true & battlestates == BattleState.idle)
        {
            heldKey = true;
            jumpQueue = true;
            holdJumpFloat = 1.0f;
            Debug.Log("Jump");
        }

    }

    public void ExitLeft()
    {
        turnmanager.direction = 4;
        turnmanager.EndCombat(4);
    }
    public void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "ground")
        {
            grounded = false;
            anim.SetBool("Grounded", false);
        }
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "ground")
        {
            heldKey = false;
            grounded = true;
            if (battlestates == BattleState.jumping && !jumpQueue)
            {
                battlestates = BattleState.idle;
            }
              
            anim.SetBool("Grounded", true);

        }
    }

   public void TakeDamage(int damage)
    {
        if (alive)
        {
            heldKey = false;
            variables.TakeDamage(damage);
            anim.ResetTrigger("TailSide");
            anim.ResetTrigger("Jump");
            anim.SetTrigger("Hit");
            battlestates = BattleState.hurt;
            rigid.velocity = new Vector2(0, 0);
        }
    }
    public void DeflectReset()
    {
        tail.DeflectReset();
        battlestates = BattleState.idle;

    }

    public void TailDeflect(int dir)
    {
        //for animation event
        tail.Deflect(dir);
    }

    public void ResetStats()
    {
        //for use of resetting to idle state, multiple variables
        if(battlestates != BattleState.jumping)
        battlestates = BattleState.idle;

        heldKey = false;
        
    }
    public IEnumerator Death()
    {
        Debug.Log("Player Death");
        turnmanager.PlayerDeath();
        leveldata.ChangeCamera(dCamera);
        
        alive = false;
        this.GetComponent<PlayerCombatScript>().enabled = false;
        anim.SetBool("End", true);
        yield return new WaitForSeconds(3.0f);
        yield return StartCoroutine(deathCamera.BeginFade(1));
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);


        // after fade to black, reset from campfire

    }

    public void ChooseDir()
    {
        incombat = false;
    }
    public void EndCombat(int dir)
    {
        this.enabled = false;
        anim.SetTrigger("StageMove");
        anim.SetBool(stageTriggers[dir],true);
    }
}
