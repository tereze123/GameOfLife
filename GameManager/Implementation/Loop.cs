using Domain;
using Domain.Statistics;
using Presentation;
using System;
using System.Threading;

namespace Application.Implementation
{
    public class Loop
    {
        private bool[,] InitialArray { get; set; }

        private bool[,] NextGenerationArray { get; set; }
        private readonly Statistics _statistics;
        private readonly DrawFieldForConsole _drawField;
        private GameField _gameField;

        public Loop()
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

            _drawField.DrawGameArrayOnScreen(gameField, cursorLeft, cursorTop);
            _drawField.DrawStatistics(
                arraySize,
                iterationCount,
                allCellCount,
                aliveCellCount,
                deadCellCount);
            iterationCount++;
            Thread.Sleep(1000);

            PlayGame(_gameField.GetNewGenerationArray(gameField), cursorLeft, cursorTop, iterationCount);
            return _gameField.GetNewGenerationArray(gameField);
        }
    }
}

