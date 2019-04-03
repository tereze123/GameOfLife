namespace Presentation.Interfaces
{
    public interface IInput
    {
        int GetValidFieldSizeFromUser();
        int GetValidUserInputForPausedGame(int[,] firstArray);
        int GetValidUserInputForStartMenu();
    }
}