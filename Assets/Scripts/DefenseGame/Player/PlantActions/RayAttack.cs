using UnityEngine;

public class RayAttack : PlantAction
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float damage = 1;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private LineRenderer[] lineRenderers; //Make line renderer gobj into prefabs & instantiate
    [SerializeField] private float lineFadeRate = 0.9f;

    private float currentWidth = 0f;

    public override void Activate(Plant plant)
    {
        lineRenderer.useWorldSpace = true;
        RaycastHit hit;
        lineRenderer.SetPosition(0, plant.transform.position);
        lineRenderer.SetPosition(1, plant.transform.position + plant.transform.forward * 200);
        if (Physics.Raycast(plant.transform.position, plant.transform.forward, out hit, 200, layerMask))
        {
            if (hit.transform && hit.transform.gameObject.GetComponent<HealthManager>())
            {
                lineRenderer.SetPosition(1, hit.transform.position);
                hit.transform.gameObject.GetComponent<HealthManager>().ApplyDamage(damage);
            }
        }
        currentWidth = 1f;
        SetLineRendererWidth(currentWidth);
        Debug.Log("Ray Attack");
    }

    void Update()
    {
         currentWidth -= lineFadeRate * Time.deltaTime;
         if(currentWidth < 0f)
         {
             currentWidth = 0f;
         }
         SetLineRendererWidth(currentWidth);
    }

    private void SetLineRendererWidth(float width)
    {
        AnimationCurve curve = lineRenderer.widthCurve;
        Keyframe[] keys = curve.keys;
        for (int i = 0; i < keys.Length; i++)
        {
            keys[i].value = width;
        }
        curve.keys = keys;
        lineRenderer.widthCurve = curve;
    }

    public override Texture2D GetIcon()
    {
        throw new System.NotImplementedException();
    }
}
