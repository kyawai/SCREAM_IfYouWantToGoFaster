using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private Transform _transform;

    private Vector2 _moveInput;

    private void Start()
    {
        Screen.lockCursor = true;
    }

    private void FixedUpdate()
    {
        Vector3 move = _transform.right * _moveInput.x + _transform.forward * _moveInput.y;
        move.y = 0f;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }
    
}
