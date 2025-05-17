using UnityEngine;

public class Bonsai : Growth
{
    [SerializeField] private GameObject leaves;
    private Vector3 targetSize = Vector3.one;
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private float timeUntilRightSize = 1;
    float timeCounter = 0;
    void Start()
    {
        leaves.SetActive(false);
        leaves.transform.localScale = Vector3.zero;
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
            Vector3 size = new Vector3(currentSize, currentSize, currentSize); 
            leaves.transform.localScale = size;
        }
    }

}


public abstract class Growth : MonoBehaviour
{
    public abstract void Grow();
}