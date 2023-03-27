using System;
using UnityEngine;
using UnityEngine.InputSystem;

class CharacterInputController : MonoBehaviour
{
    private GameInput _gameInput;
    private IControllable _controllable;

    private void Awake()
    {
        _controllable = GetComponent<IControllable>();

        if (_controllable == null)
        {
            throw new Exception($"You should implement IControllable interface on object: {gameObject.name}");
        }

        _gameInput =  new GameInput();
        _gameInput.Enable();
    }

    private void OnEnable()
    {
        _gameInput.Gameplay.Jump.performed += OnJumpPerformed;
    }

    private void OnJumpPerformed(InputAction.CallbackContext obj)
    {
        _controllable.Jump();
    }

    private void OnDisable()
    {
        _gameInput.Gameplay.Jump.performed -= OnJumpPerformed;
    }

    private void Update()
    {
        ReadMove();
    }

    private void ReadMove()
    {
        var inputDirection = _gameInput.Gameplay.Movement.ReadValue<Vector2>();
        var direction = new Vector3(inputDirection.x, 0f, inputDirection.y);
        
        _controllable.Move(direction);
    }
}
