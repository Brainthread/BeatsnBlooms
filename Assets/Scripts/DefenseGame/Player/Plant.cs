using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private PlantAction[] plantActions;

    public void Activate(int ability)
    {
        plantActions[ability].Activate();
    }

}

public abstract class PlantAction : MonoBehaviour
{
    [SerializeField] private Texture2D icon; 
    public abstract void Activate();
    public abstract Texture2D GetIcon();
}