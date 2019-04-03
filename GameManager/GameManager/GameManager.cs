﻿using FileOperations.Interfaces;
using GameEngine;
using GameEngine.Interfaces;
using GamePlayManager.Enums;
using GamePlayManaging.Interfaces;
using InputAndOutput.Interfaces;
using System;
using System.Threading;

namespace GamePlayManaging
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



        //public void PlayMultiGame(params MultipleGames[] games)
        //{
        //    int cursorXPosition = 0;

        //    __output.ClearGameScreen();
        //    int iterationCounter = 0;
        //    for (int i = 0; i < 2; i++)
        //    {
        //        games[i].FirstArray = __generations.CreateArray(10);
        //        games[i].SecondArray = __generations.CreateArray(10);
        //        __generations.InitializeArray(games[i].FirstArray);
        //    }
        //    do
        //    {
        //        for (int i = 0; i < 9; i++)
        //        {
        //            cursorXPosition = (((i % 2) + 1) * ((i % 2) + 1) * ((i % 2) + 1)) * (Generations.GetArraySize(games[i].FirstArray));
        //            __output.DrawGameArrayOnScreen(games[i].FirstArray, cursorXPosition, (i / 2) * (Generations.GetArraySize(games[i].FirstArray)));
        //            games[i].SecondArray = __generations.GetNewGenerationArray(games[i].FirstArray, games[i].SecondArray);
        //        }
        //        for (int i = 0; i < 9; i++)
        //        {
        //            cursorXPosition = (((i % 2) + 1) * ((i % 2) + 1) * ((i % 2) + 1)) * (Generations.GetArraySize(games[i].FirstArray));
        //            __output.DrawGameArrayOnScreen(games[i].SecondArray, cursorXPosition, (i / 2) * (Generations.GetArraySize(games[i].FirstArray)));
        //            games[i].FirstArray = __generations.GetNewGenerationArray(games[i].SecondArray, games[i].FirstArray);
        //            iterationCounter++;
        //        }
        //        Thread.Sleep(1000);
        //    } while (!Input.EscapeKeyWasPressed());
        //}
        public void SaveGame(int[,] array)
        {
            _fileOperations.SaveGameToFile(array);
        }

        public void PauseGame(int[,] initialArray, int[,] nextGenerationArray, int currentArray)
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
            int[,] initialArray,
            int[,] nextGenerationArray,
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
            int[,] initialArray = _fileOperations.LoadGameFromFile();
            int arraySize = initialArray.GetLength(0);
            var nextGenerationArray = _gameField.CreateArray(arraySize);
            _outputText.ClearScreen();
            PlayGame(initialArray, nextGenerationArray);
        }

        public void StartNewGame(int cursorLeft = 0, int cursorTop = 0)
        {
            int sizeOfField = _input.GetValidFieldSizeFromUser();
            _gameModel.FirstArray = _gameField.CreateArray(sizeOfField);
            _gameModel.SecondArray = _gameField.CreateArray(sizeOfField);
            _gameField.InitializeArray(_gameModel.FirstArray);
            _outputText.ClearScreen();
            this.PlayGame(_gameModel.FirstArray, _gameModel.SecondArray, cursorLeft, cursorTop);
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
                    //case StartMenuEnum.StartMultipleGames:
                    //MultipleGames games = new MultipleGames(_generations, new Output(_generations, new ConsoleManipulations()));
                    //games.PlayMultiGame(games, games, games, games, games, games, games, games, games);
                    //break;
            }
        }
    }
}

