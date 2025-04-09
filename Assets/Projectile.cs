using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 latestPosition;
    private float unitMovementPerBeat = 30;
    private float movementSpeed = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        latestPosition = transform.position;
        movementSpeed = unitMovementPerBeat / MusicManager.instance.GetBeatsPerSecond();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
    }
}
