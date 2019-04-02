﻿using GamePlayManaging.Interfaces;
using InputAndOutput;
using FileOperations.Interfaces;
using System;
using System.Threading;
using InputAndOutput.Interfaces;
using GameEngine.Interfaces;

namespace GamePlayManaging
{
    public class GameManager:IGameManager
    {
        private readonly IFileOperations _fileOperations;
        private readonly IInputAndOutput _inputAndOutput;
        private readonly IGameEngine _gameEngine;

        public GameManager(IFileOperations fileOperations, IInputAndOutput inputAndOutput, IGameEngine gameEngine)
        {
            _fileOperations = fileOperations;
            _inputAndOutput = inputAndOutput;
            _gameEngine = gameEngine;
        }

        public void StartMenu()
        {
            int usersChoice = _inputAndOutput.GetValidUserInputForStartMenu();
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
                    games.PlayMultiGame(games, games, games, games, games, games, games, games, games);
                    break;
            }
        }

        public void PlayMultiGame(params MultipleGames[] games)
        {
            int cursorXPosition = 0;

            this._output.ClearGameScreen();
            int iterationCounter = 0;
            for (int i = 0; i < 2; i++)
            {
                games[i].FirstArray = this._generations.CreateArray(10);
                games[i].SecondArray = this._generations.CreateArray(10);
                this._generations.InitializeArray(games[i].FirstArray);
            }
            do
            {
                for (int i = 0; i < 9; i++)
                {
                    cursorXPosition = (((i % 2) + 1) * ((i % 2) + 1) * ((i % 2) + 1)) * (Generations.GetArraySize(games[i].FirstArray));
                    this._output.DrawGameArrayOnScreen(games[i].FirstArray, cursorXPosition, (i / 2) * (Generations.GetArraySize(games[i].FirstArray)));
                    games[i].SecondArray = this._generations.GetNewGenerationArray(games[i].FirstArray, games[i].SecondArray);
                }
                for (int i = 0; i < 9; i++)
                {
                    cursorXPosition = (((i % 2) + 1) * ((i % 2) + 1) * ((i % 2) + 1)) * (Generations.GetArraySize(games[i].FirstArray));
                    this._output.DrawGameArrayOnScreen(games[i].SecondArray, cursorXPosition, (i / 2) * (Generations.GetArraySize(games[i].FirstArray)));
                    games[i].FirstArray = this._generations.GetNewGenerationArray(games[i].SecondArray, games[i].FirstArray);
                    iterationCounter++;
                }
                Thread.Sleep(1000);
            } while (!Input.EscapeKeyWasPressed());
        }
        public void SaveGame(int[,] array)
        {
            _fileOperations.WriteTheArrayIntoFile(array);
        }

        public void PauseGame(int[,] firstArray, int[,] secondArray, int currentArray)
        {
            int continueGame = _inputAndOutput.GetValidUserInputForPausedGame(firstArray);

            switch (continueGame)
            {
                case 1:
                    _inputAndOutput.ClearScreen();
                    PlayGame(firstArray, secondArray);
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

        public void PlayGame(
            int[,] firstArray,
            int[,] secondArray,
            int cursorLeft = 1,
            int cursorTop = 1,
            int iterationCount = 1)
        {
            int allCellCount;
            int aliveCellCount;
            int deadCellCount;
            int arraySize = firstArray.GetLength(0);
            int arrayNumberToSave = 0;
            do
            {
                arrayNumberToSave = 1;

                allCellCount = _gameEngine.GetAllCellCount(firstArray);
                aliveCellCount = _gameEngine.GetAliveCellCount(firstArray);
                deadCellCount = _gameEngine.GetDeadCellCount(allCellCount, aliveCellCount);

                _inputAndOutput.DrawGameArrayOnScreen(firstArray, cursorLeft, cursorTop);
                _inputAndOutput.DrawStatistics(
                    arraySize,
                    iterationCount,
                    allCellCount,
                    aliveCellCount,
                    deadCellCount);

                Thread.Sleep(1000);

                secondArray = _gameEngine.GetNewGenerationArray(firstArray, secondArray);

                allCellCount = _gameEngine.GetAllCellCount(secondArray);
                aliveCellCount = _gameEngine.GetAliveCellCount(secondArray);
                deadCellCount = _gameEngine.GetDeadCellCount(allCellCount, aliveCellCount);


                iterationCount++;

                _inputAndOutput.DrawGameArrayOnScreen(secondArray, cursorLeft, cursorTop);
                _inputAndOutput.DrawStatistics(arraySize, iterationCount, allCellCount, aliveCellCount, deadCellCount);

                Thread.Sleep(1000);

                arrayNumberToSave = 2;

                firstArray = _gameEngine.GetNewGenerationArray(secondArray, firstArray);

                iterationCount++;

            } while (!IsGamePaused());

            PauseGame(firstArray, secondArray, arrayNumberToSave);
        }

        public void StartGameFromLoadedFile()
        {
            int[,] firstArray = _fileOperations.ReturnSavedArrayFromFile();
            int arraySize = firstArray.GetLength(0);
            var secondArray = _gameEngine.CreateArray(arraySize);
            _inputAndOutput.ClearScreen();
            PlayGame(firstArray, secondArray);
        }

        public void StartNewGame(int cursorLeft = 0, int cursorTop = 0)
        {
            int sizeOfField = _inputAndOutput.GetValidFieldSizeFromUser();
            _gameEngine.FirstArray = _gameEngine.CreateArray(sizeOfField);
            _gameEngine.SecondArray = _gameEngine.CreateArray(sizeOfField);
            _gameEngine.InitializeArray(_gameEngine.FirstArray);
            _inputAndOutput.ClearScreen();
            this.PlayGame(_gameEngine.FirstArray, _gameEngine.SecondArray, cursorLeft, cursorTop);
        }

        public bool IsGamePaused()
        {
            return (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape);
        }
    }
}

