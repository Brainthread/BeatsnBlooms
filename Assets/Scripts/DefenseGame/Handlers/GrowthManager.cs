using UnityEngine;

public class GrowthManager : MonoBehaviour
{
    [SerializeField] private Material renderMaterial;
    private MusicManager musicManager;
    [SerializeField] private AnimationCurve growthSpeed;
    [SerializeField] private AnimationCurve degrowthSpeed;
    [SerializeField] private float shiftSpeed = 0.1f;
    private bool lost = false;
    [SerializeField] private float recedeTime = 2;
    private float recedeCounter = 0;
    private float reachedLength = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicManager = MusicManager.instance;
        renderMaterial.SetFloat("_Progress", 0);
        renderMaterial.SetFloat("_SineOffset", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!lost)
        {
            renderMaterial.SetFloat("_Progress", growthSpeed.Evaluate(musicManager.GetSongProgression()));
            float fx0 = growthSpeed.Evaluate(musicManager.GetSongProgression() - 0.1f);
            float fx1 = growthSpeed.Evaluate(musicManager.GetSongProgression() + 0.1f);
            float derivative = (fx1 - fx0) / (0.2f);
            renderMaterial.SetFloat("_SineOffset", renderMaterial.GetFloat("_SineOffset") + shiftSpeed * derivative);
        }
        else
        {
            recedeCounter += Time.deltaTime;
            renderMaterial.SetFloat("_Progress", reachedLength*degrowthSpeed.Evaluate(recedeCounter/recedeTime));
        }

    }

    public void OnLose()
    {
        reachedLength = growthSpeed.Evaluate(musicManager.GetSongProgression());
        lost = true;
    }
}
