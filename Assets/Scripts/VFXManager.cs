using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public static VFXManager current;
    private int impactIndex = 0;
    private float impactT = 0;
    [SerializeField] ImpactFrame[] impactFrames;
    [SerializeField] Material impactMaterial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        current = this;
        impactIndex = impactFrames.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(impactIndex < impactFrames.Length)
        {
            impactMaterial.SetFloat("_Threshold", impactFrames[impactIndex].frameThreshold);
            impactT += Time.deltaTime;
            if (impactT > impactFrames[impactIndex].impactLength)
            {
                impactT = 0;
                impactIndex += 1;
            }
        }
        else
        {
            impactMaterial.SetInt("_Active", 0);
        }

    }


    public void PlayImpactFrames()
    {
        Invoke("PlayImpact", 0.06f);
    }
    private void PlayImpact()
    {
        impactIndex = 0;
        impactMaterial.SetInt("_Active", 1);
    }
}

[System.Serializable]
public class ImpactFrame
{
    public float impactLength;
    public float frameThreshold;
}
