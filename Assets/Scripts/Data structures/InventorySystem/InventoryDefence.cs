using UnityEngine;
using UnityEngine.UI;

public class InventoryDefence : MonoBehaviour
{
    private ToggleGroup toggleGroup;

    private void Start()
    {
        toggleGroup = GetComponentInChildren<ToggleGroup>();
    }

    public void Activate()
    {

    }

    public void OnToggleChange()
    {
        Toggle[] toggles = GetComponentsInChildren<Toggle>();
        int inc = 0;
 

        foreach(var t in toggles)
        {
            if (t.isOn) Debug.Log(inc);
        }
    }
}

//https://stackoverflow.com/questions/52739763/how-to-get-selected-toggle-from-toggle-group