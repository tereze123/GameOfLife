using Domain.Factory;
using FileOperations.Interfaces;
using Domain;
using Domain.Interfaces;
using Application.Enums;
using Presentation.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using Domain.Statistics;

namespace Application
{
    public class GameManager
    {
        private readonly GameModelState _gameModel;
        private readonly IFileOperations _fileOperations;
        private readonly IDrawField _drawField;
        private readonly IInputAndOutput _inputAndOutput;
        private readonly IValidateUserInput _validateUserInput;
        private readonly IGameField _gameField;
        private readonly IStatistics _statistics;

        public GameManager(
            IDrawField drawField,
            IInputAndOutput inputAndOutput,
            IGameField gameField
            )
        {
            _gameModel = new GameModelState();
            _fileOperations = new FileOperations.FileOperations();
            _drawField = drawField;
            _inputAndOutput = inputAndOutput;
            _validateUserInput = new ValidateUserInput();
            _gameField = new GameField(new GameLogic());
            _statistics = new Statistics();
        }

        public void PlayMultiGame(int gameCount, List<GameModelState> games)
        {
            Console.Clear();
            int cursorXPosition = 0;

            int iterationCounter = 0;
            for (int i = 0; i < gameCount; i++)
            {
                games[i].InitialArray = _gameField.CreateArray(10);
                games[i].NextGenerationArray = _gameField.CreateArray(10);
                _gameField.InitializeArray(games[i].InitialArray);
            }
            do
            {
                for (int i = 0; i < gameCount; i++)
                {
                    cursorXPosition = (((i % 2) + 1) * ((i % 2) + 1) * ((i % 2) + 1)) * (games[i].InitialArray.GetLength(0));
                    _drawField.DrawGameArrayOnScreen(games[i].InitialArray, cursorXPosition, (i / 2) * (games[i].InitialArray.GetLength(0)));
                    games[i].NextGenerationArray = _gameField.GetNewGenerationArray(games[i].InitialArray, games[i].NextGenerationArray);
                }
                Thread.Sleep(1000);
                for (int i = 0; i < gameCount; i++)
                {
                    cursorXPosition = (((i % 2) + 1) * ((i % 2) + 1) * ((i % 2) + 1)) * (games[i].InitialArray.GetLength(0));
                    _drawField.DrawGameArrayOnScreen(games[i].NextGenerationArray, cursorXPosition, (i / 2) * (games[i].InitialArray.GetLength(0)));
                    games[i].InitialArray = _gameField.GetNewGenerationArray(games[i].NextGenerationArray, games[i].InitialArray);
                    iterationCounter++;
                }
                Thread.Sleep(1000);
            } while (!this.IsGamePaused());
        }

        public void SaveGame(bool[,] array)
        {
            _fileOperations.SaveGameToFile(array);
        }

        public void PauseGame(bool[,] initialArray, bool[,] nextGenerationArray, int currentArray)
        {
            PausedGameMenuEnum usersChoice = this.GetValidPausedGameInputFromUser();
            switch (usersChoice)
            {
                case PausedGameMenuEnum.ContinueGame:
                    PlayGame(initialArray, nextGenerationArray);
                    break;
                case PausedGameMenuEnum.SaveGame:
                    if (currentArray == 1) SaveGame(initialArray);
                    else SaveGame(nextGenerationArray);
                    break;
                case PausedGameMenuEnum.ExitTheGame:
                    Environment.Exit(0);
                    return;
                default:
                    return;
            }
        }

        public void PlayGame(
            bool[,] initialArray,
            bool[,] nextGenerationArray,
            int cursorLeft = 1,
            int cursorTop = 1,
            int iterationCount = 1)
        {
            int allCellCount;
            int aliveCellCount;
            int deadCellCount;
            int arraySize = initialArray.GetLength(0);
            int arrayNumberToSave = 0;
            Console.Clear();
            do
            {
                arrayNumberToSave = 1;

                allCellCount = _statistics.GetAllCellCount(initialArray);
                aliveCellCount = _statistics.GetAliveCellCount(initialArray);
                deadCellCount = _statistics.GetDeadCellCount(allCellCount, aliveCellCount);

                _drawField.DrawGameArrayOnScreen(initialArray, cursorLeft, cursorTop);
                _drawField.DrawStatistics(
                    arraySize,
                    iterationCount,
                    allCellCount,
                    aliveCellCount,
                    deadCellCount);

                Thread.Sleep(1000);

                nextGenerationArray = _gameField.GetNewGenerationArray(initialArray, nextGenerationArray);

                allCellCount = _statistics.GetAllCellCount(nextGenerationArray);
                aliveCellCount = _statistics.GetAliveCellCount(nextGenerationArray);
                deadCellCount = _statistics.GetDeadCellCount(allCellCount, aliveCellCount);


                iterationCount++;

                _drawField.DrawGameArrayOnScreen(nextGenerationArray, cursorLeft, cursorTop);
                _drawField.DrawStatistics(
                    arraySize, 
                    iterationCount, 
                    allCellCount, 
                    aliveCellCount, 
                    deadCellCount);

                Thread.Sleep(1000);

                arrayNumberToSave = 2;

                initialArray = _gameField.GetNewGenerationArray(nextGenerationArray, initialArray);

                iterationCount++;

            } while (!IsGamePaused());

            PauseGame(initialArray, nextGenerationArray, arrayNumberToSave);
        }

        public void StartGameFromLoadedFile()
        {
            bool[,] initialArray = _fileOperations.LoadGameFromFile();
            int arraySize = initialArray.GetLength(0);
            var nextGenerationArray = _gameField.CreateArray(arraySize);
            PlayGame(initialArray, nextGenerationArray);
        }

        public void StartNewGame()
        {
            int sizeOfField = this.GetValidFieldSizeInputFromUser();
            _gameModel.InitialArray = _gameField.CreateArray(sizeOfField);
            _gameModel.NextGenerationArray = _gameField.CreateArray(sizeOfField);
            _gameField.InitializeArray(_gameModel.InitialArray);
            this.PlayGame(_gameModel.InitialArray, _gameModel.NextGenerationArray);
        }

        public void Start()
        {
            this.StartMenu();
        }

        public bool IsGamePaused()
        {
            return (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape);
        }

        public void StartMenu()
        {
            var usersChoice = this.GetValidStartMenuInputFromUser();
            switch (usersChoice)
            {
                case StartMenuEnum.StartNewGame:
                    StartNewGame();
                    break;
                case StartMenuEnum.StartGameFromLoadedFile:
                    StartGameFromLoadedFile();
                    break;
                case StartMenuEnum.StartMultipleGames:
                    var gameList = GameListFactory.GetGameList(8);
                    PlayMultiGame(gameList.Count,gameList);
                    break;
            }
        }

        private int GetValidFieldSizeInputFromUser()
        {
            var usersInput = "";
            do
            {
                _inputAndOutput.Write(Properties.Resources.TextOutputForFieldSizeInput);
                usersInput = _inputAndOutput.GetUserInput();
            } while (!(_validateUserInput.IsFieldSizeUserInputValid(usersInput)));

            return int.Parse(usersInput);
        }

        private PausedGameMenuEnum GetValidPausedGameInputFromUser()
        {
            var userChoice = "";
            do
            {
                _inputAndOutput.Write(Properties.Resources.TextOutputForPausedGame);
                userChoice = _inputAndOutput.GetUserInput();
            } while (!(_validateUserInput.IsPausedGameUserInputValid(userChoice)));
            return (PausedGameMenuEnum)(int.Parse(userChoice));
        }

        private StartMenuEnum GetValidStartMenuInputFromUser()
        {
            var userChoice = "";
            do
            {
                _inputAndOutput.Write(Properties.Resources.TextOutputForStartMenu);
                userChoice = _inputAndOutput.GetUserInput();
            } while (!(_validateUserInput.IsPausedGameUserInputValid(userChoice)));
            return (StartMenuEnum)(int.Parse(userChoice));
        }
    }
}

