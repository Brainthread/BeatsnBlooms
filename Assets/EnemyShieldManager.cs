using UnityEngine;

public class EnemyShieldManager : MonoBehaviour
{
    [SerializeField] private Material shieldMaterial;
    [SerializeField] private Material standardMaterial;

    [SerializeField] private bool[] shieldEnabledQueue;
    private int queueIndex = 0;
    private float shieldActivationDelay = 0.12f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        standardMaterial= GetComponent<Renderer>().material;
        EventHandler.current.onBeat += OnBeat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        EventHandler.current.onBeat -= OnBeat;
    }

    private void OnBeat()
    {
        queueIndex++;
        if(queueIndex>=shieldEnabledQueue.Length)
        {
            queueIndex = 0;
        }
        Invoke("SwitchShieldState", shieldActivationDelay);
    }

    private void SwitchShieldState()
    {
        //TODO: Make these systems more modular
        GetComponent<HealthManager>().SetInvulnerability(!shieldEnabledQueue[queueIndex]);
        GetComponent<Renderer>().material = shieldEnabledQueue[queueIndex] ? shieldMaterial : standardMaterial;
    }
}
