using UnityEngine;

public class BattleStarterScript : MonoBehaviour, IInteractable
{
    private bool isInteractable = true;
    public bool CanInteract()
    {
        return isInteractable;
    }

    public void Interact()
    {
        SceneTransition.instance.StartBattle();
    }

    /*void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            SceneTransition.instance.StartBattle();
        }
    }*/
}
