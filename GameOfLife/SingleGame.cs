using System;
using System.Threading;

namespace GameOfLife
{
    public class SingleGame:Game
    {
        
        private readonly GameStatistics statistics;
        private readonly Generations generations;
        private readonly FileOperations fileStore;
        private readonly UserInterFace userInterFace;

        public SingleGame(GameStatistics statistics, Generations generations,FileOperations fileStore, UserInterFace userInterFace)
        {
            this.statistics = statistics;
            this.generations = generations;
            this.fileStore = fileStore;
            this.userInterFace = userInterFace;
        }
        
        public void StartMenu()
        {
            int usersChoice = userInterFace.GetUserInputForStartMenu();
            switch (usersChoice)
            {
                case 1:
                    StartNewGame();
                    break;

                case 2:
                    StartGameFromLoadedFile();
                    break;
                case 3:
                    MultipleGames games = new MultipleGames(generations, statistics, userInterFace);
                    games.PlayMultiGame(new MultipleGames(generations, statistics, userInterFace), new MultipleGames(generations, statistics, userInterFace));
                    break;
            }
        }

        private void SaveGame(int[,] array)
        {
            this.fileStore.WriteTheArrayIntoFile(array);
        }

        private void PauseGame(int[,] firstArray, int[,] secondArray, int currentArray)
        {
           int continueGame = userInterFace.GetUserInputForPausedGame(firstArray);

            switch (continueGame)
            {
                case 1:
                    PlayGame(  firstArray,   secondArray);
                    break;
                case 2:
                    if (currentArray == 1)
                    {
                        SaveGame(firstArray);
                    }
                    else
                    {
                        SaveGame(secondArray);
                    }                    
                    break;
                case 3:
                    Environment.Exit(0);
                    return;
                default:
                    return;
            }
        }

        private void PlayGame(  int[,] firstArray, int[,] secondArray, int cursorLeft = 0, int cursorTop = 0)
        {
            int allCellCount;
            int aliveCellCount;
            int deadCellCount;
            int arraySize = firstArray.GetLength(0);
            int currentArray = 0;
            int iterationCount = 1;           
            do
            {
                //GET STATISTICS
                allCellCount = statistics.GetAllCellCount(firstArray);
                aliveCellCount = statistics.GetAliveCellCount(firstArray);
                deadCellCount = statistics.GetDeadCellCount(allCellCount, aliveCellCount);

                //DELAY APPLICATION 1 SECOND
                Thread.Sleep(1000);

                //DRAW ARRAY AND STATISTICS
                userInterFace.DrawGameArrayOnScreen(firstArray, cursorLeft, cursorTop);
                userInterFace.DrawStatistics(arraySize, iterationCount, allCellCount, aliveCellCount, deadCellCount);

                //SET CURRENT ARRAY FOR SAVING PURPOSES
                currentArray = 1;

                //GET THE NEXT GENERATION ARRAY
                secondArray = generations.GetNewGenerationArray(firstArray,   secondArray);

                //GET STATISTICS
                allCellCount = statistics.GetAllCellCount(secondArray);
                aliveCellCount = statistics.GetAliveCellCount(secondArray);
                deadCellCount = statistics.GetDeadCellCount(allCellCount, aliveCellCount);

                //DELAY APPLICATION 1 SECOND
                Thread.Sleep(1000);

                //DRAW ARRAY AND STATISTICS
                userInterFace.DrawGameArrayOnScreen(secondArray, cursorLeft, cursorTop);
                userInterFace.DrawStatistics(arraySize, iterationCount, allCellCount, aliveCellCount, deadCellCount);

                //SET CURRENT ARRAY FOR SAVING PURPOSES
                currentArray = 2;

                //GET THE NEXT GENERATION ARRAY
                firstArray = generations.GetNewGenerationArray(  secondArray,   firstArray);

                //INCREASE THE ITERATION COUNTER
                iterationCount++;

            } while ((!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)));

            PauseGame(firstArray,   secondArray, currentArray);
        }

        private void StartGameFromLoadedFile()
        {
            int[,] firstArray = fileStore.ReturnSavedArrayFromFile();
            int arraySize = firstArray.GetLength(0);
            var secondArray = generations.CreateArray(arraySize);
            userInterFace.ClearGameScreen();
            PlayGame(firstArray,secondArray);
        }

        private void StartNewGame(int cursorLeft = 0, int cursorTop = 0)
        {
            int sizeOfField = userInterFace.GetFieldSizeFromUser();
            FirstArray = generations.CreateArray(sizeOfField);
            SecondArray = generations.CreateArray(sizeOfField);
            generations.InitializeArray(FirstArray);
            userInterFace.ClearGameScreen();
            PlayGame(FirstArray, SecondArray, cursorLeft, cursorTop);            
        }

        private void StartNewMultiGame(int cursorLeft = 0, int cursorTop = 0)
        {
            int sizeOfField = userInterFace.GetFieldSizeFromUser();
            FirstArray = generations.CreateArray(sizeOfField);
            SecondArray = generations.CreateArray(sizeOfField);
            generations.InitializeArray(FirstArray);
            userInterFace.ClearGameScreen();
            PlayGame(FirstArray, SecondArray, cursorLeft, cursorTop);
        }

    }
}
