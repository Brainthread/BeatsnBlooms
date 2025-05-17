using UnityEngine;

public class GrowthManager : MonoBehaviour
{
    public static GrowthManager instance;
    [SerializeField] private Material renderMaterial;
    private MusicManager musicManager;
    [SerializeField] private AnimationCurve growthSpeed;
    [SerializeField] private AnimationCurve degrowthSpeed;
    [SerializeField] private float shiftSpeed = 0.1f;
    private bool lost = false;
    [SerializeField] private float recedeTime = 2;
    private float recedeCounter = 0;
    private float reachedLength = 0f;
    private bool initialized = false;
    [SerializeField] private bool funcNFmod = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private float discreteProgression = 0;
    private float sufferingFactor = 0f;

    [SerializeField] private GameObject plane;
    [SerializeField] private GameObject growthObjectHolder;
    void Start()
    {
        instance = this;
    }

    public void Initialize()
    {
        sufferingFactor = 0;
        renderMaterial.SetFloat("_Progress", growthSpeed.Evaluate(sufferingFactor));
        musicManager = MusicManager.instance;
        renderMaterial.SetFloat("_Progress", 0);
        renderMaterial.SetFloat("_SineOffset", 0);
        EventHandler.current.onLoss += OnLose;
        initialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(initialized) {
            if (!lost)
            {
                float progression;
                if (funcNFmod)
                {
                    progression = musicManager.GetSongProgression();
                    sufferingFactor = progression;
                }
                else
                    progression = discreteProgression;
                sufferingFactor = Mathf.MoveTowards(sufferingFactor, progression, Time.deltaTime/10);
                    
                renderMaterial.SetFloat("_Progress", growthSpeed.Evaluate(sufferingFactor));
                float fx0 = growthSpeed.Evaluate(sufferingFactor - 0.1f);
                float fx1 = growthSpeed.Evaluate(sufferingFactor + 0.1f);
                float derivative = (fx1 - fx0) / (0.2f);
                renderMaterial.SetFloat("_SineOffset", renderMaterial.GetFloat("_SineOffset") + shiftSpeed * derivative);
                if(growthObjectHolder)
                {
                    float localScale = plane.transform.localScale.x*10;
                    float progressionPosition = plane.transform.position.x - localScale / 2;
                    progressionPosition += localScale * growthSpeed.Evaluate(sufferingFactor);
                    //print(progressionPosition);
                    for (int i = 0; i < growthObjectHolder.transform.childCount; i++)
                    {
                        Growth growth = growthObjectHolder.transform.GetChild(i).GetComponent<Growth>();
                        //print(growth.gameObject.transform.position.x + ":" + (plane.transform.position.x - localScale/2));
                        if (growth.gameObject.transform.position.x-10 < progressionPosition)
                        {
                            growth.Grow();
                        }
                    }
                }
            }
            else
            {
                recedeCounter += Time.deltaTime;
                renderMaterial.SetFloat("_Progress", reachedLength*degrowthSpeed.Evaluate(recedeCounter/recedeTime));
            }
        }

    }

    public void OnLose()
    {
        reachedLength = growthSpeed.Evaluate(musicManager.GetSongProgression());
        lost = true;
    }

    public void SetDiscreteProgression(float value)
    {
        discreteProgression = value;
    }
}
