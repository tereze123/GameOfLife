using System.Collections.Generic;

namespace Domain.Factory
{
    public class GameListFactory
    {

        public List<GameModelState> GetGameList(int gameCount)
        {
            GameModelStateFactory factory = new GameModelStateFactory();
            var gameList = new List<GameModelState>();

            for (int i = 0; i < gameCount; i++)
            {
                gameList.Add(factory.NewGameModelState());
            }
            return gameList;
        }
    }
}
