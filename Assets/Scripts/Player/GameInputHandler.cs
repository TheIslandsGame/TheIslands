using System;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = Gameplay.PlayerInput;

namespace Player
{
    public class GameInputHandler : MonoBehaviour, PlayerInput.IPlayerActions
    {

        public CharacterController2D controller;
        public bool isCrouch;
        public bool isJump;
        public Vector2 movement;
        public float runSpeed = 40F;

        // TODO remove
        public GameObject player;
        public float speed = 5F;

        private PlayerInput input;

        private void Awake()
        {
            input = new PlayerInput();
            input.Player.SetCallbacks(this);
        }
        
        private void OnEnable()
        {
            input.Player.Enable();
        }

        private void OnDisable()
        {
            input.Player.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            movement = context.ReadValue<Vector2>();
            if (movement.y >= 0.5F)
            {
                isJump = true;
            }
        }
        
        public void OnLook(InputAction.CallbackContext context)
        {
            // TODO is this even needed?
        }
        
        public void OnLeaveGame(InputAction.CallbackContext context)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            SceneManager.LoadScene("MainMenu");
#endif
        }
        
        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed) isJump = true;
            if (context.canceled) isJump = false;
        }
        
        public void OnCrouch(InputAction.CallbackContext context)
        {
            if (context.performed) isCrouch = true;
            if (context.canceled) isCrouch = false;
        }
        
        public void OnSprint(InputAction.CallbackContext context)
        {
            //if (context.performed) isSprint = true;
            //if (context.canceled) isSprint = false;
        }

        private void FixedUpdate()
        {
            player.transform.Translate(movement.x * speed * Time.fixedDeltaTime, movement.y * speed * Time.fixedDeltaTime, 0);
            //isJump = movement.y > 0.5F;
            //controller.Move(-movement.x * Time.fixedDeltaTime, isCrouch, isJump);
            //isJump = false;
        }
    }
}