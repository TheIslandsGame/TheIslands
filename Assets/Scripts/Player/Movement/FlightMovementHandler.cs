using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Movement
{
    public class FlightMovementHandler : IPlayerInputHandler
    {
        private readonly GameObject player;
        private Vector2 movement;
        private const float Speed = 5F;

        public FlightMovementHandler(GameObject player)
        {
            this.player = player;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            movement = context.ReadValue<Vector2>();
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
            if (context.performed) Debug.Log("Jump");
        }
        public void OnCrouch(InputAction.CallbackContext context)
        {
            if (context.performed) Debug.Log("Jump");
        }
        public void OnSprint(InputAction.CallbackContext context)
        {
            if (context.performed) Debug.Log("Jump");
        }
        public void Tick(float delta)
        {
            player.transform.Translate(movement.x * Speed * delta, movement.y * Speed * delta, 0);
        }
    }
}