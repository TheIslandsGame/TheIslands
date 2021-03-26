using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Movement
{
    public class DefaultMovementHandler : IPlayerInputHandler
    {
        private readonly IGameInputHandler inputHandler;
        private readonly float runSpeed;
        private Vector2 movement;
        
        public DefaultMovementHandler(IGameInputHandler inputHandler, float runSpeed)
        {
            this.inputHandler = inputHandler;
            this.runSpeed = runSpeed;
        }
        
        public void OnMove(InputAction.CallbackContext context)
        {
            movement = context.ReadValue<Vector2>();
            inputHandler.SetSprinting(Math.Abs(movement.x) > 0.1F);
        }
        public void OnLook(InputAction.CallbackContext context)
        {
            // NO-OP for now
        }
        public void OnLeaveGame(InputAction.CallbackContext context)
        {
            GameInputHandler.LeaveGame();
        }
        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed) inputHandler.SetJumping(true);
            if (context.canceled) inputHandler.SetJumping(false);
        }
        
        public void OnCrouch(InputAction.CallbackContext context)
        {
            if (context.performed) inputHandler.SetCrouching(true);
            if (context.canceled) inputHandler.SetCrouching(false);
        }
        
        public void OnSprint(InputAction.CallbackContext context)
        {
            if (context.performed) inputHandler.SetSprinting(true);
            if (context.canceled) inputHandler.SetSprinting(false);
        }
        public void Tick(float delta)
        {
            inputHandler.GetController().Move(movement.x * delta * runSpeed, inputHandler.IsCrouching(), inputHandler.IsJumping());
        }
    }
}