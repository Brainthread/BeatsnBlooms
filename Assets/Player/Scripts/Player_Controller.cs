using UnityEngine;

[SelectionBase]

public class Player_Controller : MonoBehaviour
{
    #region Editor
    [Header("Movement Attributes")]
    [SerializeField] float _moveSpeed = 70f;


    [Header("Dependencies")]
    [SerializeField] Rigidbody2D _rb;
    #endregion

    #region Internal
    private Vector2 _moveDir = Vector2.zero;
    #endregion

    #region Tick
    private void Update()
    {
        GatherInput();
    }

    private void FixedUpdate()
    {
        MovementUpdate();
    } 
    #endregion

    #region Input
    private void GatherInput(){
        _moveDir.x = Input.GetAxisRaw("Horizontal");
        _moveDir.y = Input.GetAxisRaw("Vertical");

        print(_moveDir);
    }
    #endregion

    #region Movement
    private void MovementUpdate(){
        _rb.linearVelocity = _moveSpeed * Time.fixedDeltaTime * _moveDir.normalized;
    }
    #endregion
}
