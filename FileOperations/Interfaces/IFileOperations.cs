namespace FileOperations.Interfaces
{
    public interface IFileOperations
    {
        int[,] ReturnSavedArrayFromFile();
        void WriteTheArrayIntoFile(int[,] array);
    }
}
