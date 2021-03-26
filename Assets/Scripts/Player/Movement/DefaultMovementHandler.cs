using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Movement
{
    public class DefaultMovementHandler : IPlayerInputHandler
    {
        private readonly IGameInputHandler inputHandler;
        private readonly GameObject player;
        
        public DefaultMovementHandler(IGameInputHandler inputHandler, GameObject player)
        {
            this.inputHandler = inputHandler;
            this.player = player;
        }
        
        public void OnMove(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }
        public void OnLook(InputAction.CallbackContext context)
        {
            // NO-OP for now
        }
        public void OnLeaveGame(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }
        public void OnJump(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }
        public void OnCrouch(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }
        public void OnSprint(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }
        public void Tick(float delta)
        {
            throw new System.NotImplementedException();
        }
    }
}