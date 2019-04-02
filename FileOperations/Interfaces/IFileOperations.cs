namespace FileOperations.Interfaces
{
    public interface IFileOperations
    {
        int[,] LoadGameFromFile();
        void SaveGameToFile(int[,] array);
    }
}
