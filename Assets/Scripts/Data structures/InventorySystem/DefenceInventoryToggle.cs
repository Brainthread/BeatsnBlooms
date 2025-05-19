using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
public class DefenceInventoryToggle : MonoBehaviour
{
    [SerializeField] private TileAction.TileActionTypes tileType;
    [SerializeField] private int stackSize = 0;
    private bool infinite = false;
    private TMP_Text stackSizeUI; 
    private UnityEvent<TileAction.TileActionTypes> onToggleOn = new UnityEvent<TileAction.TileActionTypes>();

    //Buffer for remove
    private Toggle toggleRef;
    private ToggleGroup groupRef;

    public void ToggleOn(bool on)
    {
        if (!on) return;
        onToggleOn.Invoke(tileType);
    }

    public void SetupInfiniteToggle(TileAction.TileActionTypes type, int stackAmt, InventoryDefence defenceInstance, ToggleGroup toggleGroup)
    {
        SetupToggle(type, stackAmt, defenceInstance, toggleGroup);
        stackSizeUI = GetComponentInChildren<TMP_Text>();
        stackSizeUI.text = "∞";
        infinite = true;
    }
    public void SetupToggle(TileAction.TileActionTypes type, int stackAmt, InventoryDefence defenceInstance, ToggleGroup toggleGroup)
    {
        Toggle toggle = GetComponent<Toggle>();
        toggle.group = toggleGroup;
        toggleGroup.RegisterToggle(toggle);
        onToggleOn.AddListener(defenceInstance.SetCurrentTileType);
        stackSizeUI = GetComponentInChildren<TMP_Text>();

        tileType = type;
        stackSize = stackAmt;
        stackSizeUI.text = stackAmt.ToString();

        SetInstances(toggle, toggleGroup);
    }

    public void RemoveToggle()
    {
        groupRef.UnregisterToggle(toggleRef);
    }

    //Because order function calls we can't do this in Start or Awake...
    private void SetInstances(Toggle toggle, ToggleGroup group)
    {
        toggleRef = toggle;
        groupRef = group;
    }
}
