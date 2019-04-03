namespace Presentation.Interfaces
{
    public interface IOutputText
    {
        void ClearScreen();

        void TextOutputForFieldSizeInput();

        void TextOutputForPausedGame();

        void TextOutputForStartMenuInput();
    }
}
