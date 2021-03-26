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
                        movementHandler = new DefaultMovementHandler(this, runSpeed);
                        break;
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
        public bool IsJumping()
        {
            return isJump;
        }
        public bool IsCrouching()
        {
            return isCrouch;
        }
        public bool IsSprinting()
        {
            return isSprint;
        }
        public CharacterController2D GetController()
        {
            return controller;
        }
    }
}