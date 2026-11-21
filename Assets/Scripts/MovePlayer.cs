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
    private Vector2 _moveInput;
    private Vector2 _lookInput;
    private CharacterController _characterController;
    private float _turnAngleY;
    private float _turnAngleX;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _lookAction = InputSystem.actions.FindAction("Look");
        _characterController = GetComponent<CharacterController>();
        _cameraPivot = GameObject.Find("CameraPivot");
        _cam =  Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        _moveInput = _moveAction.ReadValue<Vector2>();
        _lookInput = _lookAction.ReadValue<Vector2>();
        _turnAngleY += _lookInput.x * sensitivity * Time.deltaTime; 
        _turnAngleX -= _lookInput.y * sensitivity * Time.deltaTime; 
        transform.rotation = Quaternion.Euler(0, _turnAngleY, 0);
        _cameraPivot.transform.localRotation = Quaternion.Euler(_turnAngleX, 0, 0);
        Vector3 moveDirection = transform.forward * _moveInput.y + transform.right * _moveInput.x;
        _characterController.Move(moveSpeed * Time.deltaTime * moveDirection);
        
    }
}
