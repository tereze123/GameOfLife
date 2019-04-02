namespace GameOfLife
{
    public interface IFileOperations
    {
        int[,] ReturnSavedArrayFromFile();
        void WriteTheArrayIntoFile(int[,] array);
    }
}