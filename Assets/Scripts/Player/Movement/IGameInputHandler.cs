namespace Player.Movement
{
    public interface IGameInputHandler
    {
        void SetJumping(bool jumping);
        void SetCrouching(bool crouching);
        void SetSprinting(bool sprinting);

        bool IsJumping();
        bool IsCrouching();
        bool IsSprinting();
        CharacterController2D GetController();
    }
}