using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerController3D : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody rb;
    private Vector2 moveInput;
    [SerializeField] private UnityEvent<bool> OnWalkingState;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;
        if (moveInput.magnitude > 0) OnWalkingState.Invoke(true);
        else OnWalkingState.Invoke(false);
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
