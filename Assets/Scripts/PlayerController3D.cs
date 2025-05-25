using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController3D : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody rb;
    private Vector2 moveInput;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject character;
    [SerializeField] private float characterRotationSpeed = 180;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;
        bool moving = moveInput * moveSpeed != Vector2.zero;
        anim.SetBool("Moving", moving);
        if(moving)
        {
            Vector2 movementDirection = -new Vector3(moveInput.x, moveInput.y, 0).normalized;
            Quaternion rotation = Quaternion.LookRotation(movementDirection, -Vector3.forward);
            character.transform.rotation = Quaternion.RotateTowards(character.transform.rotation, 
                rotation, 
                characterRotationSpeed * Time.deltaTime);
        }
       

    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
