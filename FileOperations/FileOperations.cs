using System;
using System.IO;
using System.Linq;
using FileOperations.Interfaces;

namespace FileOperations
{
    public class FileOperations : IFileOperations
    {
        private void CreateFolderOnDesktop()
        {
            var folder = Path.Combine(GetPath(), "TESTA FOLDERIS");
            Directory.CreateDirectory(folder);
        }

        private string GetPath()
        {
            var path = "C:\\Users\\tereze.elize.empele\\Desktop\\";
            return path;
        }

        public void WriteTheArrayIntoFile(int[,] array)
        {
            CreateFolderOnDesktop();
            var arrayLength = array.GetLength(0);
            var path = Path.Combine(GetPath(), "TESTA FOLDERIS", "SavedGame.txt");

            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(path, false))
            {
                for (int i = 0; i < arrayLength; i++)
                {
                    for (int j = 0; j < arrayLength; j++)
                    {
                        file.Write(array[i, j].ToString());
                    }
                }
            }
        }

        public int[,] ReturnSavedArrayFromFile()
        {
            string text;
            int counter = 0;
            var path = Path.Combine(GetPath(), "TESTA FOLDERIS", "SavedGame.txt");
            text = File.ReadAllText(path);

            var arraySize = (int)Math.Sqrt(text.Count());

            int[,] array = new int[arraySize, arraySize];

            for (int i = 0; i < arraySize; i++)
            {
                for (int j = 0; j < arraySize; j++)
                {
                    int integer = (text[counter]);
                    if (integer == 49)
                    {
                        array[i, j] = 1;
                    }
                    else
                    {
                        array[i, j] = 0;
                    }
                    counter++;
                }
            }
            return array;
        }
    }
}
