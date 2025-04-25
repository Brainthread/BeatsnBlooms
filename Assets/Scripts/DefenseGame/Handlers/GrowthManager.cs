using UnityEngine;

public class GrowthManager : MonoBehaviour
{
    [SerializeField] private Material renderMaterial;
    private MusicManager musicManager;
    [SerializeField] private AnimationCurve growthSpeed;
    [SerializeField] private float shiftSpeed = 0.1f;
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
        float fx0 = growthSpeed.Evaluate(musicManager.GetSongProgression() - 0.1f);
        float fx1 = growthSpeed.Evaluate(musicManager.GetSongProgression() + 0.1f);
        float derivative = (fx1-fx0) / (0.2f);
        renderMaterial.SetFloat("_SineOffset", renderMaterial.GetFloat("_SineOffset") + shiftSpeed*derivative);
    }
}
