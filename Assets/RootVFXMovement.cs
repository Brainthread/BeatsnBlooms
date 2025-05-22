using UnityEngine;

public class RootVFXMovement : MonoBehaviour
{
    float lifeTime = 3;
    [SerializeField] private float movementSpeed = 200;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime < 0 )
        {
            Destroy(this.gameObject);
        }
       transform.Translate(Vector3.forward * Time.deltaTime* movementSpeed); 
    }
}
