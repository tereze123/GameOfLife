using Domain;
using System.Collections.Generic;

namespace Domain.Factory
{
    public class GameListFactory
    {
        public static List<GameModelState> GetGameList(int gameCount)
        {
            var gameList = new List<GameModelState>();

            for (int i = 0; i < gameCount; i++)
            {
                gameList.Add(GameModelStateFactory.NewGameModelState());
            }
            return gameList;
        }
    }
}
