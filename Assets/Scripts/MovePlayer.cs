using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sensitivity = 100f;
    private Camera _cam;
    private GameObject _cameraPivot;
    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _jumpAction;
    private Vector2 _moveInput;
    private Vector2 _lookInput;
    private CharacterController _characterController;
    private float _turnAngleY;
    private float _turnAngleX;
    private float _gravity;
    private float _jump;
    private bool _isGrounded;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _lookAction = InputSystem.actions.FindAction("Look");
        _jumpAction = InputSystem.actions.FindAction("Jump");
        _characterController = GetComponent<CharacterController>();
        _cameraPivot = GameObject.Find("CameraPivot");
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        _moveInput = _moveAction.ReadValue<Vector2>();
        _lookInput = _lookAction.ReadValue<Vector2>();
        _turnAngleY += _lookInput.x * sensitivity * Time.deltaTime; 
        _turnAngleX = Mathf.Clamp(_turnAngleX - _lookInput.y * sensitivity * Time.deltaTime,-90,90); 
        transform.rotation = Quaternion.Euler(0, _turnAngleY, 0);
        _cameraPivot.transform.localRotation = Quaternion.Euler(_turnAngleX, 0, 0);
        if (_jumpAction.IsPressed() && _isGrounded)
        {
            _gravity = -9.81f;
            _jump = 3f;
            _isGrounded = false;
        }
        if (transform.position.y - 1.08f < 0.0001f)
        {
            _isGrounded = true;
            _gravity = 0f;
        }
        _jump += _gravity * Time.deltaTime;
        Vector3 moveDirection = transform.forward * _moveInput.y + transform.right * _moveInput.x + transform.up * _jump;
        _characterController.Move(moveSpeed * Time.deltaTime * moveDirection);
        
    }
}
