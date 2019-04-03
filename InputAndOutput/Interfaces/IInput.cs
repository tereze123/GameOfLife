namespace Presentation.Interfaces
{
    public interface IInput
    {
        int GetValidFieldSizeFromUser();
        int GetValidUserInputForPausedGame(bool[,] firstArray);
        int GetValidUserInputForStartMenu();
    }
}