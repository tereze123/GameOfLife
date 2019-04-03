namespace FileOperations.Interfaces
{
    public interface IFileOperations
    {
        bool[,] LoadGameFromFile();
        void SaveGameToFile(bool[,] array);
    }
}
