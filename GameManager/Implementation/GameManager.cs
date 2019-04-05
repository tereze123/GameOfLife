using Application.Enums;
using Application.Implementation;
using Application.Interfaces;
using Domain;
using Domain.Factory;
using Domain.Interfaces;
using Domain.Statistics;
using FileOperations.Interfaces;
using Presentation.Interfaces;
using System;
using System.Collections.Generic;

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
        private GameLoop _loop;

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
            _loop = new GameLoop();
        }

        public void SaveGame(bool[,] array)
        {
            _fileOperations.SaveGameToFile(array);
        }

        public void SaveGame(List<GameModelState> gameModelStates)
        {
            _fileOperations.SaveGameToFile(gameModelStates);
        }

        public void PauseGame(bool[,] gameArray)
        {
            PausedGameMenuEnum usersChoice = this.GetValidPausedGameInputFromUser();
            switch (usersChoice)
            {
                case PausedGameMenuEnum.ContinueGame:
                    this.GamePlay(gameArray);
                    break;
                case PausedGameMenuEnum.SaveGame:
                    this.SaveGame(gameArray);
                    break;
                case PausedGameMenuEnum.ExitTheGame:
                    Environment.Exit(0);
                    return;
                default:
                    return;
            }
        }

        public void PauseGame(List<GameModelState> gameList)
        {
            PausedGameMenuEnum usersChoice = this.GetValidPausedGameInputFromUser();
            switch (usersChoice)
            {
                case PausedGameMenuEnum.ContinueGame:
                    this.PlayMultipleGames(gameList);
                    break;
                case PausedGameMenuEnum.SaveGame:
                    this.SaveGame(gameList);
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
                    this.PlayMultipleGames();
                    break;
            }
        }

        private void PlayMultipleGames()
        {
            GameListFactory factory = new GameListFactory();
            var fullGameList = factory.GetGameList(1000);
            var selectedGameNumberList = GetSelectedGameNumberList();
            _loop.PlayMultiGame(8, fullGameList, selectedGameNumberList);
            this.PauseGame(fullGameList);
        }

        private void PlayMultipleGames(List<GameModelState> fullGameList)
        {
            var selectedGameNumberList = GetSelectedGameNumberList();
            _loop.PlayMultiGame(8, fullGameList, selectedGameNumberList);
           // this.PauseGame(fullGameList,selectedGameNumberList);
        }

        private List<int> GetSelectedGameNumberList()
        {
            var selectedGameList = new List<int>();
            _inputAndOutput.Write(Properties.Resources.TextOutputForFieldNumberInputForManyGames);
            for (int i = 0; i < 8; i++)
            {
                selectedGameList.Add(int.Parse(_inputAndOutput.GetUserInput()));
            }
            return selectedGameList;
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

        private int GetValidFieldNumbersForMultiGameDisplay()
        {
            var userChoice = "";
            do
            {
                _inputAndOutput.Write(Properties.Resources.TextOutputForFieldNumberInputForManyGames);
                userChoice = _inputAndOutput.GetUserInput();
            } while (!(_validateUserInput.IsPausedGameUserInputValid(userChoice)));
            return int.Parse(userChoice);
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

