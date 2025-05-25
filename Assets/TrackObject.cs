using UnityEngine;

public class TrackObject : MonoBehaviour
{
    [SerializeField] private Transform trackedObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(trackedObject.position);    
    }
}
