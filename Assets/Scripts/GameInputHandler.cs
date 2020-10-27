using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class GameInputHandler : MonoBehaviour, PlayerInput.IPlayerActions
{

    private const float MovementModifier = 30.0F;
    private PlayerInput _input;
    public Rigidbody2D body;
    public float moveSpeed = 0.3F;
    
    private void Awake()
    {
        _input = new PlayerInput();
        _input.Player.SetCallbacks(this);
    }

    private void OnEnable()
    {
        _input.Player.Enable();
    }

    private void OnDisable()
    {
        _input.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>() * moveSpeed * MovementModifier;
        body.velocity = move;
        Debug.Log("Move: " + move);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 look = context.ReadValue<Vector2>();
        Debug.Log("Look: " + look);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        Debug.Log("Fired!");
    }

    public void OnAltFire(InputAction.CallbackContext context)
    {
        Debug.Log("Alt fired!");
    }
}