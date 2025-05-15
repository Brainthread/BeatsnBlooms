using UnityEngine;


public enum INVENTORY_STATE
{
    EXPLORE,
    DEFENCE,
    OFF
}

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public InventoryDefence inventoryDefence { get; private set; }
    public InventoryExplore inventoryExplore { get; private set; }

    private INVENTORY_STATE inventoryState;

    void Start()
    {
        if (instance == null) instance = this;
        else Destroy(this);

        inventoryDefence = GetComponentInChildren<InventoryDefence>();
        inventoryExplore = GetComponentInChildren<InventoryExplore>();

        InventorySystem.instance.SetupTestInventory(); //Add test tiles to tile inventory
        inventoryDefence.SetupDefenceInventory(); //Setup the the GUI for the defence game inventory
    }

   void SetInventoryState(INVENTORY_STATE state)
    {
        inventoryState = state;
        switch (inventoryState)
        {
            case INVENTORY_STATE.DEFENCE:
                inventoryDefence.gameObject.SetActive(true);
                inventoryExplore.gameObject.SetActive(false);
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
