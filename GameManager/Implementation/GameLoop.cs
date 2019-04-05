using Domain;
using Domain.Statistics;
using Presentation;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Implementation
{
    public class GameLoop
    {
        private bool[,] InitialArray { get; set; }

        private bool[,] NextGenerationArray { get; set; }
        private readonly Statistics _statistics;
        private readonly DrawFieldForConsole _drawField;
        private GameField _gameField;

        public GameLoop()
        {
            _statistics = new Statistics();
            _drawField = new DrawFieldForConsole(new ColorOfOutput());
            _gameField = new GameField(new GameLogic());
        }
        private bool IsGamePaused()
        {
            return (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape);
        }

        public bool[,] PlayGame(bool[,] gameField, int iterationCount = 1)
        {
            if (IsGamePaused())
            {
                return gameField;
            }

            int allCellCount;
            int aliveCellCount;
            int deadCellCount;
            int arraySize = gameField.GetLength(0);


            allCellCount = _statistics.GetAllCellCount(gameField);
            aliveCellCount = _statistics.GetAliveCellCount(gameField);
            deadCellCount = _statistics.GetDeadCellCount(allCellCount, aliveCellCount);

            _drawField.DrawGameArrayOnScreen(gameField);
            _drawField.DrawStatistics(
                arraySize,
                iterationCount,
                allCellCount,
                aliveCellCount,
                deadCellCount);
            iterationCount++;
            Thread.Sleep(1000);

            PlayGame(_gameField.GetNewGenerationArray(gameField),  iterationCount);
            return _gameField.GetNewGenerationArray(gameField);
        }


        public void PlayMultiGame(int visibleGameCount, List<GameModelState> games, List<int> selectedGames)
        {
            Console.Clear();
            int cursorXPosition = 0;
            int cursorYPosition = 0;
            int deadCellCount;
            int aliveCellCount = 0;
            int allCellCount;
            int iterationCounter = 0;

            for (int i = 0; i < games.Count; i++)
            {
                games[i].GameField = _gameField.CreateArray(10);
                games[i].GameField = _gameField.CreateArray(10);
                _gameField.InitializeArray(games[i].GameField);
            }
            do
            {
                allCellCount = 0;
                deadCellCount = 0;
                aliveCellCount = 0;

                Parallel.ForEach(games, (g) =>
                {
                    var gameField = new GameField(new GameLogic());
                    g.GameField = gameField.GetNewGenerationArray(g.GameField);
                });

                allCellCount += _statistics.GetAllCellCountMultiGame(games);
                aliveCellCount += _statistics.GetAliveCellCountMultiGame(games);
                deadCellCount = allCellCount - aliveCellCount;

                for (int i = 0; i < visibleGameCount; i++)
                {
                    cursorXPosition = (((i % 2) + 1) * ((i % 2) + 1)) * (games[selectedGames[i]].GameField.GetLength(0));
                    cursorYPosition = (i / 2) * (games[selectedGames[i]].GameField.GetLength(0));
                    _drawField.DrawGameArrayOnScreen(games[selectedGames[i]].GameField, cursorXPosition, cursorYPosition);
                }

                iterationCounter++;
                _drawField.DrawStatistics(
                    10,
                    iterationCounter,
                    allCellCount,
                    aliveCellCount,
                    deadCellCount,
                    games.Count,
                    visibleGameCount);

                Thread.Sleep(1000);
            } while (!this.IsGamePaused());
        }
    }
}

