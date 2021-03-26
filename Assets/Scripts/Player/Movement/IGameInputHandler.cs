namespace Player.Movement
{
    public interface IGameInputHandler
    {
        void SetJumping(bool jumping);
        void SetCrouching(bool crouching);
        void SetSprinting(bool sprinting);
        CharacterController2D GetController();
    }
}