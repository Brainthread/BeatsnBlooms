using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Vector3 latestPosition;
    [SerializeField] private float unitMovementPerBeat = 30;
    private float movementSpeed = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        latestPosition = transform.position;
        movementSpeed = unitMovementPerBeat / MusicManager.instance.GetBeatsPerSecond();
        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
    }
}
