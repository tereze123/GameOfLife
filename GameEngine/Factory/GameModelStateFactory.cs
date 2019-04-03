using GameEngine;

namespace Domain.Factory
{
    public class GameModelStateFactory
    {
        public static GameModelState NewGameModelState()
        {
            return new GameModelState();
        }
    }
}
