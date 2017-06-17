using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class Thing : MonoBehaviour
{
    public bool known;
    public string description;
    public Sprite sprite;
    public int stock;
    public int space;
}
public class Resource : Thing
{
    new public string name;
    public int max;
    public Resource(string N, int amt, int m, Sprite s)
    {
        name = N;
        stock = amt;
        max = m;
        sprite = s;
    }
    public ItemIcon ui;
    
}
public class Inventory : ManagerBase, CombatBool
{

    public int size;
    int stock;
    public Item[] inventory;
    public static Dictionary<string, Resource> resources;
    GameObject UIprefab;
    public Sprite mushSprite;
    
    InputManager inputManager;
    public InventoryMenu menu;
    bool active, incombat;
    public static int rTypeNo = 4;
    public GameObject baseResourceUI;

    void Start()
    {
        menu = gameObject.AddComponent<InventoryMenu>();
        menu.baseResourceUI = baseResourceUI;
        inventory = GetComponents<Item>();
        resources = new Dictionary<string, Resource>()
        {
            {"mushroom", new Resource("Mushroom", 1, 5, mushSprite) },
            {"fruit", new Resource("Fruit", 0, 3, mushSprite) },
            {"shell", new Resource("Shell", 0, 3, mushSprite) },
            {"core", new Resource("Core", 0, 2, mushSprite) },
        };
        
        inputManager = GetComponent<InputManager>();
        

    }

    public void AddResource(string rType, int addAmount)
    {
        Resource R = resources[rType];
        R.stock += addAmount;
        R.stock = Mathf.Clamp(R.stock, 0, R.max);
        resources[rType] = R;
    }

    public void AddItem(Item newItem)
    {

        for (int i = 0; i < inventory.Length; i++)
        {
            if (newItem.GetType() == inventory[i].GetType() && inventory[i].known)
            {
                if (stock == size)
                {
                    break;
                }
                else
                {
                    inventory[i].stock += 1;
                    stock += inventory[i].space;
                    break;
                }
            }    
        }
        Debug.Log("Add Item");
        
    }

    
    public void MenuOn(bool inCombat)
    {
        incombat = inCombat;
        menu.incombat = incombat;
        inputManager.InputPush(menu);
        Debug.Log("Push menu " + menu);
    }
    
    public bool CraftItem(Item newItem)
    {
        if (stock + newItem.space <= size)
        {
            CraftCost[] craft = newItem.cost;
            foreach (CraftCost c in craft)
            {
                Debug.Log("Cost of this item is " + c.cost + " of " + c.resource);
                if (resources[c.resource].stock - c.cost < 0)
                {
                    return false;
                }
            }
            foreach (CraftCost c in craft)
            {
                Resource r = resources[c.resource];
                r.stock -= c.cost;
                resources[c.resource] = r;
            }
            AddItem(newItem);
            menu.UpdateList();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UseItem(int slotNo)
    {
        Item temp = inventory[slotNo];
        if (temp.stock > 0)
        {
            temp.Use(player);
            stock -= temp.space;
            temp.stock -= 1;
            /*if (temp.stock < 0)
            {

                for (int i = slotNo; i < inventory.Length - 1; i++)
                {
                    inventory[i] = inventory[i + 1];
                }

                inventory[inventory.Length - 1] = null;
            }
            for (int i = slotNo; i < inventory.Length - 1; i++)
            {
                inventory[i] = inventory[i + 1];
            }

            inventory[inventory.Length - 1] = null;
            */
            menu.UpdateList();
        }
    }

    public void SetState(bool a, bool c)
    {
        active = a;
        incombat = c;
    }
}

public class InventoryMenu : MenuInput
{
    InputManager inputManager;
    public Item[] tempList;
    Inventory inventory;
    public string exitKey = "f";
    bool exitReady = false;
    Item currentItem;
    int currentIndex = 0;
    public bool incombat;
    GridLayoutGroup grid;
    ItemIcon[] itemUIs;
    ItemIcon[] resourceUIs;
    public GridLayoutGroup resourceGrid;
    CanvasGroup resourcePanel;
    public GameObject baseResourceUI;

    void Start()
    {
        inputManager = GetComponent<InputManager>();
        menuCanvas = GetComponentInChildren<Canvas>();
        inventory = GetComponent<Inventory>();
        for (int i = 0; i < menuCanvas.transform.childCount; i++)
        {
            if(menuCanvas.transform.GetChild(i).tag == "resourceUI" && !resourceGrid)
            {
                resourcePanel = menuCanvas.transform.GetChild(i).GetComponent<CanvasGroup>();
                resourceGrid = menuCanvas.transform.GetChild(i).GetComponentInChildren<GridLayoutGroup>();
               
            }
            else
            {
                grid = menuCanvas.transform.GetChild(i).GetComponentInChildren<GridLayoutGroup>();
            }
            //Debug.Log(menuCanvas.transform.GetChild(i));
        }
        grid.constraintCount = inventory.size;
        resourceGrid.constraintCount = 7;
        itemUIs = grid.GetComponentsInChildren<ItemIcon>();
        //for each dictionary entry in resources, create one resources UI
        foreach (KeyValuePair<string, Resource> resource in Inventory.resources)
        {
            ItemIcon UI = Instantiate(baseResourceUI).GetComponent<ItemIcon>();
            resource.Value.ui = UI;
            
            UI.transform.SetParent(resourceGrid.transform);
            UI.itemSprite = resource.Value.sprite;
        }
        resourceUIs = resourceGrid.GetComponentsInChildren<ItemIcon>();

        tempList = new Item[inventory.inventory.Length];
        resourcePanel.alpha = 0f;
        

    }
    public override void pushFunction()
    {
        base.pushFunction();
        UpdateList();
        currentItem = tempList[currentIndex];
        if(!incombat)
        {
            resourcePanel.alpha = 1.0f;
        }
        tempList[currentIndex].Hover();
        menuCanvas.enabled = true;

        StartCoroutine(InputPause());


    }

    public void UpdateList()
    {
        if(tempList != null)
        inventory.inventory.CopyTo(tempList, 0);
        for(int i = 0; i < tempList.Length && i < itemUIs.Length; i++)
        {
            itemUIs[i].DataSet(tempList[i]);
            tempList[i].Assign(itemUIs[i]);
        }

        foreach(KeyValuePair<string,Resource> r in Inventory.resources)
        {
            r.Value.ui.DataSet(r.Value);
        }
        //DataSet for resources

    }
    public override void InputUpdate()
    {
        //Debug.Log("Input Update Inventory");
        
        base.InputUpdate();
        if(Input.GetKeyDown("w"))
        {
            tempList[currentIndex].Hover();
            currentIndex += 1;
            currentIndex = Mathf.Clamp(currentIndex, 0, tempList.Length - 1);
            tempList[currentIndex].Hover();
        }
        else if (Input.GetKeyDown("s"))
        {
            tempList[currentIndex].Hover();
            currentIndex -= 1;
            currentIndex = Mathf.Clamp(currentIndex, 0, tempList.Length - 1);
            tempList[currentIndex].Hover();
        }
        else if(Input.GetKeyDown("space"))
        {

            if(tempList[currentIndex])
            inventory.UseItem(currentIndex);

            Debug.Log("Inventory Menu: Use of " + currentIndex + tempList.Length);
        }
        else if (Input.GetKeyDown("e"))
        {
            if (tempList[currentIndex] && !incombat)
                inventory.CraftItem(tempList[currentIndex]);

            Debug.Log("Inventory Menu: Craft");
        }
        else if(Input.GetKeyDown(exitKey) && exitReady)
        {
            Debug.Log(currentIndex);
            tempList[currentIndex].Hover();
            Disable();
            inputManager.InputPop();
        }

    }
    //Menu Coroutines for Active Inventory and Crafting Menu
    IEnumerator InputPause()
    {
        exitReady = false;
        yield return new WaitForSeconds(.05f);
        exitReady = true;
    }
    public override void Disable()
    {
        base.Disable();
        menuCanvas.enabled = false;
        resourcePanel.alpha = 0f;

    }
}
