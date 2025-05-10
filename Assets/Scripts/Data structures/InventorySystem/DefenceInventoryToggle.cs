using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
public class DefenceInventoryToggle : MonoBehaviour
{
    [SerializeField] private TileAction.TileActionTypes tileType;
    private int stackSize = 0;
    private TMP_Text stackSizeUI; 
    private UnityEvent<TileAction.TileActionTypes> onToggleOn = new UnityEvent<TileAction.TileActionTypes>();


    public void ToggleOn(bool on)
    {
        if (!on) return;
        onToggleOn.Invoke(tileType);
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
    }
}
