using System;
using UnityEngine;

class CharacterInputController : MonoBehaviour
{
    private IControllable _controllable;

    private void Awake()
    {
        _controllable = GetComponent<IControllable>();

        if (_controllable == null)
        {
            throw new Exception($"You should implement IControllable interface on object: {gameObject.name}");
        }
    }

    private void Update()
    {
        ReadMove();
        ReadJump();
    }

    private void ReadMove()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        _controllable.Move(direction);
    }

    private void ReadJump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _controllable.Jump();
        }
    }
}
