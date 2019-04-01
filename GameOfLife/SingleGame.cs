using System;
using System.Threading;

namespace GameOfLife
{
    public class SingleGame:Game
    {
        
        private readonly GameStatistics _statistics;
        private readonly Generations _generations;
        private readonly FileOperations _fileStore;
        private readonly Input _input;
        private readonly Output _output;


        public SingleGame(GameStatistics statistics, Generations generations,FileOperations fileStore, Output output, Input input)
        {
            this._statistics = statistics;
            this._generations = generations;
            this._fileStore = fileStore;
            this._input = input;
            this._output = output;
        }
        
        public void StartMenu()
        {
            int usersChoice = _input.GetValidUserInputForStartMenu();
            switch (usersChoice)
            {
                case 1:
                    StartNewGame();
                    break;

                case 2:
                    StartGameFromLoadedFile();
                    break;
                case 3:
                    MultipleGames games = new MultipleGames(_generations, new Output(_generations, new ConsoleManipulations()));
                    games.PlayMultiGame(new MultipleGames(_generations, new Output(_generations, new ConsoleManipulations())), new MultipleGames(_generations, new Output(_generations, new ConsoleManipulations())));
                    break;
            }
        }

        private void SaveGame(int[,] array)
        {
            this._fileStore.WriteTheArrayIntoFile(array);
        }

        private void PauseGame(int[,] firstArray, int[,] secondArray, int currentArray)
        {
           int continueGame = _input.GetValidUserInputForPausedGame(firstArray);

            switch (continueGame)
            {
                case 1:
                    _output.ClearGameScreen();
                    PlayGame(  firstArray,   secondArray);
                    break;
                case 2:
                    if (currentArray == 1) SaveGame(firstArray);                    
                    else SaveGame(secondArray);           
                    break;
                case 3:
                    Environment.Exit(0);
                    return;
                default:
                    return;
            }
        }

        private void PlayGame(  int[,] firstArray, int[,] secondArray, int cursorLeft = 1, int cursorTop = 1, int iterationCount = 1)
        {
            int allCellCount;
            int aliveCellCount;
            int deadCellCount;
            int arraySize = firstArray.GetLength(0);
            int currentArray = 0;          
            do
            {
                //GET STATISTICS
                allCellCount = _statistics.GetAllCellCount(firstArray);
                aliveCellCount = _statistics.GetAliveCellCount(firstArray);
                deadCellCount = _statistics.GetDeadCellCount(allCellCount, aliveCellCount);


                //DRAW ARRAY AND STATISTICS
               _output.DrawGameArrayOnScreen(firstArray, cursorLeft, cursorTop);
                _output.DrawStatistics(arraySize, iterationCount, allCellCount, aliveCellCount, deadCellCount);

                //DELAY APPLICATION 1 SECOND
                Thread.Sleep(1000);

                //SET CURRENT ARRAY FOR SAVING PURPOSES
                currentArray = 1;

                //GET THE NEXT GENERATION ARRAY
                secondArray = _generations.GetNewGenerationArray(firstArray,   secondArray);

                //GET STATISTICS
                allCellCount = _statistics.GetAllCellCount(secondArray);
                aliveCellCount = _statistics.GetAliveCellCount(secondArray);
                deadCellCount = _statistics.GetDeadCellCount(allCellCount, aliveCellCount);


                iterationCount++;
                //DRAW ARRAY AND STATISTICS
                _output.DrawGameArrayOnScreen(secondArray, cursorLeft, cursorTop);
                _output.DrawStatistics(arraySize, iterationCount, allCellCount, aliveCellCount, deadCellCount);

                //DELAY APPLICATION 1 SECOND
                Thread.Sleep(1000);

                //SET CURRENT ARRAY FOR SAVING PURPOSES
                currentArray = 2;

                //GET THE NEXT GENERATION ARRAY
                firstArray = _generations.GetNewGenerationArray(  secondArray,   firstArray);

                //INCREASE THE ITERATION COUNTER
                iterationCount++;

            } while (!Input.EscapeKeyWasPressed());

            PauseGame(firstArray, secondArray, currentArray);
        }

        private void StartGameFromLoadedFile()
        {
            int[,] firstArray = _fileStore.ReturnSavedArrayFromFile();
            int arraySize = firstArray.GetLength(0);
            var secondArray = _generations.CreateArray(arraySize);
            _output.ClearGameScreen();
            PlayGame(firstArray,secondArray);
        }

        private void StartNewGame(int cursorLeft = 0, int cursorTop = 0)
        {
            int sizeOfField = _input.GetValidFieldSizeFromUser();
            FirstArray = _generations.CreateArray(sizeOfField);
            SecondArray = _generations.CreateArray(sizeOfField);
            _generations.InitializeArray(FirstArray);
            _output.ClearGameScreen();
            PlayGame(FirstArray, SecondArray, cursorLeft, cursorTop);            
        }

        private void StartNewMultiGame(int cursorLeft = 0, int cursorTop = 0)
        {
            int sizeOfField = _input.GetValidFieldSizeFromUser();
            FirstArray = _generations.CreateArray(sizeOfField);
            SecondArray = _generations.CreateArray(sizeOfField);
            _generations.InitializeArray(FirstArray);
            _output.ClearGameScreen();
            PlayGame(FirstArray, SecondArray, cursorLeft, cursorTop);
        }

    }
}
