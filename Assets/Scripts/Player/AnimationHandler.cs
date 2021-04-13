using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    public class AnimationHandler : MonoBehaviour
    {
        private Animator animator;
        private static readonly int Crouching = Animator.StringToHash("Crouching");
        private static readonly int Jump1 = Animator.StringToHash("Jump");
        private static readonly int Land1 = Animator.StringToHash("Land");
        private static readonly int XVelocity = Animator.StringToHash("xVelocity");
        private static readonly int YVelocity = Animator.StringToHash("yVelocity");
        private static readonly int ONGround = Animator.StringToHash("onGround");
        private static readonly int TotalVelocity = Animator.StringToHash("totalVelocity");
        private bool wasGrounded = true;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void Move(Vector2 vel, bool grounded, bool crouching)
        {
            animator.SetFloat(XVelocity, Math.Abs(vel.x));
            animator.SetFloat(YVelocity, vel.y);
            animator.SetFloat(TotalVelocity, Math.Abs(vel.magnitude));
            animator.SetBool(ONGround, grounded);
            if (!wasGrounded && grounded)
            {
                Land();
            }
            wasGrounded = grounded;
        }

        public void Land()
        {
            animator.SetTrigger(Land1);
        }
        public void Jump()
        {
            animator.SetTrigger(Jump1);
        }
        public void Crouch(bool crouching)
        {
            animator.SetBool(Crouching, crouching);
        }
    }
}