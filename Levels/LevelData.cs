using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public interface CombatBool
{
    void SetState(bool a, bool c);
    
}
public class LevelData : MonoBehaviour, CombatBool {

    public List<GameObject> battlegrounds;
    public OverworldState overworld;
    public GameObject playerPrefab;
    GameObject activePlayer;
    SpriteRenderer spriteRen;
    PlayerSpawner spawnBeac;

    EnemySpawner enemySpawn;
    public List<BattlegroundData.EnemyInfo> enemyList;


    Inventory inventory;
    public GameObject combatcamera;
    GameObject activecamera;
    public TurnManager turnmanager;
    public GameObject turnPrefab;
    Game gamemanager;
    public CutsceneManager cutsceneManager;
    public GameObject cutscenePrefab;
    GameObject initialarena;
    public GameObject currentarena;
    GameObject previousarena;
    BattlegroundData battledata;
    bool active, incombat;
    InputManager inputManager;

    //temp test
    public PlayInput playInput;
    public PauseInput pauseInput;

    public string inputkey;

    void Awake()
    {
        gamemanager = Game.instance;
        gamemanager.pushState(GameObject.FindGameObjectWithTag("manager").GetComponent<CombatState>());
        


    }

	// Use this for initialization
	void Start () {
        
        turnmanager = Instantiate(turnPrefab).GetComponent<TurnManager>();
        activecamera = Instantiate(combatcamera) as GameObject;
        currentarena = battlegrounds[0];
        cutsceneManager = Instantiate(cutscenePrefab).GetComponent<CutsceneManager>();
        inventory = GetComponent<Inventory>();
        spriteRen = GetComponent<SpriteRenderer>();
        spawnBeac = GetComponentInChildren<PlayerSpawner>();
        enemySpawn = GetComponentInChildren<EnemySpawner>();
        inputManager = GetComponent<InputManager>();
        //playInput = gameObject.AddComponent<PlayInput>();
        //pauseInput = gameObject.AddComponent<PauseInput>();
        SetupCombat();
           
        
	}
	
    void SetupCombat()
    {
        inputManager.Pause();
        CutsceneManager.instance.CameraAssign(activecamera);
        battledata = currentarena.GetComponent<BattlegroundData>();
        battledata.Assign(this, turnmanager, spawnBeac, playerPrefab, enemySpawn);
        spriteRen.sprite = battledata.background;
        combatcamera.transform.position = this.transform.position + new Vector3(0, 0, -10);
        activePlayer = battledata.SetupCombat();
        inputManager.PlayerAssign(activePlayer);
        inventory.player = activePlayer;
        inputManager.PushPlay();

    }
    
    public void SetState(bool a, bool c)
    {
        active = a;
        incombat = c;
    }

	
    public void NextArena(GameObject nextarena)
    {
        if(inputManager.inputStack.Peek() == playInput)
        {
            inputManager.InputPop();
        }
        previousarena = currentarena;
        currentarena = nextarena;
        
        StartCoroutine(StageDelay(1.5f));
        //StartCoroutine(WaitforInput());
    }

    IEnumerator StageDelay(float secs)
    {
        yield return new WaitForSeconds(secs);
        SetupCombat();
        
    }


    

    public IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = GetComponent<SpriteRenderer>().material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            GetComponent<SpriteRenderer>().material.color = newColor;
            yield return null;
        }
    }

    public void ChangeCamera(Camera newcamera)
    {
        if(newcamera.enabled == false)
        {
            Camera.main.enabled = false;
            newcamera.enabled = true;
        }
        else
        {   
            newcamera.enabled = false;
            Camera.main.enabled = true;
        }
    }
    public void EndofDungeon(Canvas victoryui, string nextScene)
    {
        Instantiate(victoryui);
        StartCoroutine(InputExit(nextScene));

    }

    public void PlayerDeath()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }
    IEnumerator InputExit(string nextScene)
    {
        while (!Input.GetKeyDown((inputkey)) && Input.touchCount == 0)
        {
            yield return null;
        }
        CutsceneManager.instance.BeginFade(1);
        gamemanager.GetComponent<CombatState>().CombatEnd(nextScene);


    }
}
