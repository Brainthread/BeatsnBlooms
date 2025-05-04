using UnityEngine;

public class WinLossText : MonoBehaviour
{
    [SerializeField] private GameObject lossObject;
    [SerializeField] private GameObject winObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventHandler.current.onLoss += OnLoss;
    }

    void OnLoss()
    {
        if (lossObject != null)
        {
            lossObject.SetActive(true);
        }
    }
    private void OnWin()
    {
        if(winObject != null)
        {
            winObject.SetActive(true);
        }
    }

}
