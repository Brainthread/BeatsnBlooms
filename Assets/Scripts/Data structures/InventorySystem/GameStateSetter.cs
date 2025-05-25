using UnityEngine;

public class GameStateSetter : MonoBehaviour
{
    [SerializeField] private GAME_STATE state;
    private bool set = false;

    private void Update()
    {
        //Ugly as all hell but instance is null at start so we have to wait for it
        //to exist...
        if(!set && InventoryManager.instance != null)
        {
            set = true;
            InventoryManager.instance.SetGameState(state);
        }
    }
}
