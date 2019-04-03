namespace Application.Interfaces
{
    public interface IGameManager
    {
        void PauseGame(bool[,] initialArray, bool[,] nextGenerationArray, int currentArray);
        void PlayGame(bool[,] initialArray, bool[,] nextGenerationArray, int cursorLeft = 1, int cursorTop = 1, int iterationCount = 1);
        void SaveGame(bool[,] array);
        void StartGameFromLoadedFile();
        void StartMenu();
        void StartNewGame(int cursorLeft = 0, int cursorTop = 0);
        bool IsGamePaused();

    }
}