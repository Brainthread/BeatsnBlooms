using UnityEngine;
using UnityEngine.Events;

public enum INVENTORY_STATE
{
    EXPLORE,
    DEFENCE,
    OFF
}

public enum GAME_STATE
{
    EXPLORE,
    DEFENCE,
    PAUSE
}

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public InventoryDefence inventoryDefence { get; private set; }
    public InventoryExplore inventoryExplore { get; private set; }

    private GAME_STATE gameState;
    private INVENTORY_STATE inventoryState;

    public bool UseTestInventory = true;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            inventoryDefence = GetComponentInChildren<InventoryDefence>();
            inventoryExplore = GetComponentInChildren<InventoryExplore>();
        }
        else Destroy(gameObject);


    }

    public void SetGameState(GAME_STATE state)
    {
        if (gameState == state) return;

        gameState = state;
        if (state == GAME_STATE.DEFENCE) inventoryState = INVENTORY_STATE.DEFENCE;
        else if(state == GAME_STATE.EXPLORE) inventoryState = INVENTORY_STATE.EXPLORE;

        SetInventoryState(inventoryState);
    }

    void SetInventoryState(INVENTORY_STATE state)
    {
        inventoryState = state;
        switch (inventoryState)
        {
            case INVENTORY_STATE.DEFENCE:
                inventoryDefence.gameObject.SetActive(true);
                inventoryExplore.gameObject.SetActive(false);
                if (UseTestInventory) InventorySystem.instance.SetupTestInventory(); //Add test tiles to tile inventory
                inventoryDefence.SetupDefenceInventory(); //Setup the the GUI for the defence game inventory
                
                break;
            case INVENTORY_STATE.EXPLORE:
                inventoryDefence.gameObject.SetActive(false);
                inventoryExplore.gameObject.SetActive(true);
                break;
            case INVENTORY_STATE.OFF:
                inventoryDefence.gameObject.SetActive(false);
                inventoryExplore.gameObject.SetActive(false);
                break;
        }
    }
}
