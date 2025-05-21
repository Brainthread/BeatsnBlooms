using UnityEngine;

public class Bonsai : Growth
{
    [SerializeField] private GameObject leaves;
    private Vector3 targetSize = Vector3.one;
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private float timeUntilRightSize = 1;
    Vector3 initialScale;
    float timeCounter = 0;
    void Start()
    {
        initialScale = leaves.transform.localScale;
        leaves.SetActive(false);
        leaves.transform.localScale = Vector3.zero;
        Vector3 rotation = new Vector3(0, Random.Range(0, 360), 0);
        transform.rotation = Quaternion.Euler(rotation);

    }

    public override void Grow()
    {
        leaves.SetActive(true);
    }
    void Update()
    {
        if(leaves.activeSelf&&timeCounter<timeUntilRightSize)
        {
            timeCounter += Time.deltaTime;
            float currentSize = animationCurve.Evaluate(timeCounter/timeUntilRightSize);
            Vector3 size = currentSize*initialScale; 
            leaves.transform.localScale = size;
        }
    }

}


public abstract class Growth : MonoBehaviour
{
    public abstract void Grow();
}