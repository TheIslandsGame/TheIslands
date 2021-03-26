using Gameplay;

namespace Player.Movement
{
    public interface IPlayerInputHandler : PlayerInput.IPlayerActions
    {
        void Tick(float delta);
    }
}