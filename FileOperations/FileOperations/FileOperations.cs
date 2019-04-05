using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain;
using FileOperations.Interfaces;

namespace FileOperations
{
    public class FileOperations : IFileOperations
    {
        public void SaveGameToFile(bool[,] array)
        {
            CreateFolderOnDesktop();
            int value;
            var arrayLength = array.GetLength(0);
            var path = Path.Combine(this.GetPath(), "TESTA FOLDERIS", "SavedGame.txt");

            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(path, false))
            {
                for (int i = 0; i < arrayLength; i++)
                {
                    for (int j = 0; j < arrayLength; j++)
                    {
                        value = (array[i, j] == true) ? 1 : 0;
                        file.Write(value.ToString());
                    }
                }
            }
        }

        public void SaveGameToFile(List<GameModelState> gameList)
        {
            CreateFolderOnDesktop();
            int value;
            var arrayLength = gameList[1].GameField.GetLength(0);
            var path = Path.Combine(this.GetPath(), "TESTA FOLDERIS", "SavedMultiGame.txt");

            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(path, false))
            {
                foreach (var game in gameList)
                {
                    for (int i = 0; i < arrayLength; i++)
                    {
                        for (int j = 0; j < arrayLength; j++)
                        {
                             value = (gameList[i].GameField[i, j] == true) ? 1 : 0;
                            file.Write(value.ToString());
                        }
                        file.WriteLine();
                    }
                    file.WriteLine();
                }
            }
        }

        public bool[,] LoadGameFromFile()
        {
            string text;
            int counter = 0;
            var path = Path.Combine(this.GetPath(), "TESTA FOLDERIS", "SavedGame.txt");
            text = File.ReadAllText(path);

            var arraySize = (int)Math.Sqrt(text.Count());

            bool[,] array = new bool[arraySize, arraySize];

            for (int i = 0; i < arraySize; i++)
            {
                for (int j = 0; j < arraySize; j++)
                {
                    int integer = (text[counter]);
                    if (integer == 49)
                    {
                        array[i, j] = true;
                    }
                    else
                    {
                        array[i, j] = false;
                    }
                    counter++;
                }
            }
            return array;
        }

        private void CreateFolderOnDesktop()
        {
            var folder = Path.Combine(this.GetPath(), "TESTA FOLDERIS");
            Directory.CreateDirectory(folder);
        }

        private string GetPath()
        {
            var path = "C:\\Users\\tereze.elize.empele\\Desktop\\";
            return path;
        }
    }
}
