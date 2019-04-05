using Domain;
using System.Collections.Generic;

namespace FileOperations.Interfaces
{
    public interface IFileOperations
    {
        bool[,] LoadGameFromFile();
        void SaveGameToFile(bool[,] array);
        void SaveGameToFile(List<GameModelState> games);
    }
}
