using Player.Movement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using PlayerInput = Gameplay.PlayerInput;

namespace Player
{
    public class GameInputHandler : MonoBehaviour, IGameInputHandler
    {

        public CharacterController2D controller;
        public bool isCrouch;
        public bool isJump;
        public bool isSprint;

        public MovementMode movementMode = MovementMode.Default;

        public enum MovementMode
        {
            DebugFlight,
            Default
        }
        
        public float runSpeed = 40F;

        private IPlayerInputHandler movementHandler;

        // TODO remove
        public GameObject player;

        private PlayerInput input;

        private void Awake()
        {
            input = new PlayerInput();
            InitMovement();
            input.Player.SetCallbacks(movementHandler);
        }

        private void InitMovement()
        {
            while (true)
            {
                switch (movementMode)
                {
                    case MovementMode.DebugFlight:
                        movementHandler = new FlightMovementHandler(player);
                        break;
                    case MovementMode.Default:
                    default:
                        movementHandler = new DefaultMovementHandler(this, player);
                        continue;
                }
                break;
            }
        }

        private void OnEnable()
        {
            input.Player.Enable();
        }

        private void OnDisable()
        {
            input.Player.Disable();
        }

        
        
        public void OnLook(InputAction.CallbackContext context)
        {
            // TODO is this even needed?
        }
        
        public static void LeaveGame()
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
            movementHandler.Tick(Time.fixedDeltaTime);
            //isJump = movement.y > 0.5F;
            //controller.Move(-movement.x * Time.fixedDeltaTime, isCrouch, isJump);
            //isJump = false;
        }
        public void SetJumping(bool jumping)
        {
            isJump = jumping;
        }
        public void SetCrouching(bool crouching)
        {
            isCrouch = crouching;
        }
        public void SetSprinting(bool sprinting)
        {
            isSprint = sprinting;
        }
        public CharacterController2D GetController()
        {
            return controller;
        }
    }
}