using UnityEngine;

public class GrowthManager : MonoBehaviour
{
    [SerializeField] private Material renderMaterial;
    private MusicManager musicManager;
    [SerializeField] private AnimationCurve growthSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicManager = MusicManager.instance;
        renderMaterial.SetFloat("_Progress", 0);
    }

    // Update is called once per frame
    void Update()
    {
        renderMaterial.SetFloat("_Progress", growthSpeed.Evaluate(musicManager.GetSongProgression()));
    }
}
