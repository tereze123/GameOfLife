using Domain.Factory;
using FileOperations.Interfaces;
using Domain;
using Domain.Interfaces;
using Application.Enums;
using Application.Interfaces;
using Presentation.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Application
{
    public class GameManager:IGameManager
    {
        private readonly GameModelState _gameModel;
        private readonly IFileOperations _fileOperations;
        private readonly IColorOfOutput _colorOfOutput;
        private readonly IDrawField _drawField;
        private readonly IInput _input;
        private readonly IOutputText _outputText;
        private readonly IValidateUserInput _validateUserInput;
        private readonly IGameField _gameField;
        private readonly IGameLogic _gameLogic;
        private readonly IStatistics _statistics;

        public GameManager(
            IFileOperations fileOperations,
            IColorOfOutput colorOfOutput, 
            IDrawField drawField, 
            IInput input, 
            IOutputText outputText, 
            IValidateUserInput validateUserInput,
            IGameField gameField,
            IGameLogic gameLogic,
            IStatistics statistics
            )
        {
            _gameModel = new GameModelState();
            _fileOperations = fileOperations;
            _colorOfOutput = colorOfOutput;
            _drawField = drawField;
            _input = input;
            _outputText = outputText;
            _validateUserInput = validateUserInput;
            _gameField = gameField;
            _gameLogic = gameLogic;
            _statistics = statistics;
        }

        public void PlayMultiGame(int gameCount, List<GameModelState> games)
        {
            int cursorXPosition = 0;

            _outputText.ClearScreen();
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
            var userChoice = (PausedGameMenuEnum)(_input.GetValidUserInputForPausedGame(initialArray));

            switch (userChoice)
            {
                case PausedGameMenuEnum.ContinueGame:
                    _outputText.ClearScreen();
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
                _drawField.DrawStatistics(arraySize, iterationCount, allCellCount, aliveCellCount, deadCellCount);

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
            _outputText.ClearScreen();
            PlayGame(initialArray, nextGenerationArray);
        }

        public void StartNewGame()
        {
            int sizeOfField = _input.GetValidFieldSizeFromUser();
            _gameModel.InitialArray = _gameField.CreateArray(sizeOfField);
            _gameModel.NextGenerationArray = _gameField.CreateArray(sizeOfField);
            _gameField.InitializeArray(_gameModel.InitialArray);
            _outputText.ClearScreen();
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
            var usersChoice = (StartMenuEnum)(_input.GetValidUserInputForStartMenu());

            switch (usersChoice)
            {
                case StartMenuEnum.StartNewGame:
                    StartNewGame();
                    break;

                case StartMenuEnum.StartGameFromLoadedFile:
                    StartGameFromLoadedFile();
                    break;
                case StartMenuEnum.StartMultipleGames:
                    var gameList = GameListFactory.GetGameList(1000);
                    PlayMultiGame(gameList.Count,gameList);
                    break;
            }
        }
    }
}

