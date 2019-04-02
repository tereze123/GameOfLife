namespace GamePlayManaging.Interfaces
{
    public interface IGameManager
    {
        void PauseGame(int[,] firstArray, int[,] secondArray, int currentArray);
        void PlayGame(int[,] firstArray, int[,] secondArray, int cursorLeft = 1, int cursorTop = 1, int iterationCount = 1);
        void SaveGame(int[,] array);
        void StartGameFromLoadedFile();
        void StartMenu();
        void StartNewGame(int cursorLeft = 0, int cursorTop = 0);
        bool IsGamePaused();

    }
}