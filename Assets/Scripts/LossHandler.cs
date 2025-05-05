using UnityEngine;
using UnityEngine.SceneManagement;
public class LossHandler : MonoBehaviour
{
    [SerializeField] private int explorationScene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventHandler.current.onLoss += OnLoss;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnLoss()
    {
        Invoke("ChangeScene", 5);
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(explorationScene);
    }
}
