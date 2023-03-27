using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour, IControllable
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _jumpHeight = 2f;
    [SerializeField] private float _checkGroundRadius = 0.3f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private Transform _checkGroundPivot;
    [SerializeField] private LayerMask _layerMask;

    private bool _isGrounded;
    private float _velocity;
    private Vector3 _moveDirection;
    private CharacterController _controller;
    
    public void Move(Vector3 direction)
    {
        _moveDirection = direction;
    }
    
    public void Jump()
    {
        if (_isGrounded)
        {
            _velocity = Mathf.Sqrt(_jumpHeight * -2 * _gravity);
        }
    }

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        _isGrounded = IsOnTheGround();

        if (_isGrounded && _velocity < 0)
        {
            _velocity = -1;
        }

        InternalMove();
        DoGravity();
    }

    private void Update()
    {
        _controller.transform.rotation = Camera.main.transform.rotation;
    }

    private void InternalMove()
    {
        _controller.Move(_moveDirection * _speed * Time.fixedDeltaTime);
    }

    private void DoGravity()
    {
        _velocity += _gravity * Time.fixedDeltaTime;

        _controller.Move(Vector3.up * _velocity * Time.fixedDeltaTime);
    }
    
    private bool IsOnTheGround()
    {
        bool result = Physics.CheckSphere(_checkGroundPivot.position, _checkGroundRadius, _layerMask);
        return result;
    }
}
