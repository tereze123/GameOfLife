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
using Application.Interfaces;
using Application.Implementation;
using System.Collections.Concurrent;
using System.Threading.Tasks;

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
        private Loop _loop;

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
            _loop = new Loop();
        }

        public void PlayMultiGame(int gameCount, List<GameModelState> games)
        {
            Console.Clear();
            int cursorXPosition = 0;

            int iterationCounter = 0;

            for (int i = 0; i < gameCount; i++)
            {
                games[i].GameField = _gameField.CreateArray(10);
                games[i].GameField = _gameField.CreateArray(10);
                _gameField.InitializeArray(games[i].GameField);
            }
            do
            {
                Thread.Sleep(1000);
               
                var result = Parallel.ForEach(games, (g) =>
                 {
                   
                     g.GameField = _gameField.GetNewGenerationArray(g.GameField);
                     
                 });

                var idk = games.ToArray();
                for (int i = 0; i < gameCount; i++)
                {
                    cursorXPosition = (((i % 2) + 1) * ((i % 2) + 1) * ((i % 2) + 1)) * (idk[i].GameField.GetLength(0));
                    _drawField.DrawGameArrayOnScreen(idk[i].GameField, cursorXPosition, (i / 2) * (idk[i].GameField.GetLength(0)));
                }

            } while (!this.IsGamePaused());
        }

        public void SaveGame(bool[,] array)
        {
            _fileOperations.SaveGameToFile(array);
        }

        public void PauseGame(bool[,] gameArray)
        {
            PausedGameMenuEnum usersChoice = this.GetValidPausedGameInputFromUser();
            switch (usersChoice)
            {
                case PausedGameMenuEnum.ContinueGame:
                    this.GamePlay(gameArray);
                    _loop.PlayGame(gameArray);
                    break;
                case PausedGameMenuEnum.SaveGame:
                    SaveGame(gameArray);
                    break;
                case PausedGameMenuEnum.ExitTheGame:
                    Environment.Exit(0);
                    return;
                default:
                    return;
            }
        }

        public void GamePlay(bool[,] gameArray)
        {
            _loop.PlayGame(gameArray);
            this.PauseGame(gameArray);
        }

        public void StartGameFromLoadedFile()
        {
            bool[,] initialArray = _fileOperations.LoadGameFromFile();
            int arraySize = initialArray.GetLength(0);
            var nextGenerationArray = _gameField.CreateArray(arraySize);
            _loop.PlayGame(initialArray);
        }

        public void StartNewGame()
        {
            int sizeOfField = this.GetValidFieldSizeInputFromUser();
            _gameModel.GameField = _gameField.CreateArray(sizeOfField);
            _gameField.InitializeArray(_gameModel.GameField);
            _loop.PlayGame(_gameModel.GameField);
        }



        public bool IsGamePaused()
        {
            return (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape);
        }

        public void Start()
        {
            GameListFactory factory = new GameListFactory();
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
                    var gameList = factory.GetGameList(8);
                    PlayMultiGame(gameList.Count, gameList);
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

