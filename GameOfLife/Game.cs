using System;
using System.Collections.Generic;
using System.Threading;

namespace GameOfLife
{
    public enum CellStatus
    {
        Alive,
        Dead
    }

    public enum CellStatusResult
    {
        Live,
        Die,
        NoChange
    }

    public class Game
    {
        private static Cell[,] _cells;
        private const char SelfChar = '0';
        private const char OutOfBoundsChar = 'B';
        private delegate CellStatusResult Rule(string neighbours, CellStatus cellStatus);
        private static List<Rule> rules;
        static void Main(string[] args)
        {
            _cells = new Cell[50, 90];
            rules = new List<Rule>
            {
                new Rule(Rules.Underpopulated),
                new Rule(Rules.OverPopulated),
                new Rule(Rules.WellPopulated),
                new Rule(Rules.Reproduce)
            };
            Seed();

            int generations = 0;
            while (true)
            {
                Console.Clear();
                generations++;
                Console.WriteLine();
                Console.WriteLine($"------Generation {generations}------");
                PrintcellInConsole(GetCells());
                Tick();

                if (Console.KeyAvailable) return;

                Thread.Sleep(500);
            }
        }

        public static string PrintHorizontalLine(Cell[,] cell, int line)
        {
            var result = "";

            for (int i = 0; i < cell.GetLength(1); i++)
            {
                result = result + cell[line, i].Print();
            }

            return result;
        }

        public static void PrintcellInConsole(Cell[,] cell)
        {
            for (int i = 0; i < cell.GetLength(0); i++)
            {
                Console.WriteLine(PrintHorizontalLine(cell, i));
            }
        }

        public static Cell[,] GetCells()
        {
            return _cells;
        }

        public static void Seed()
        {
            for (var i = 0; i < _cells.GetLength(0); i++)
            {
                for (var j = 0; j < _cells.GetLength(1); j++)
                {
                    _cells[i, j] = new Cell();
                    _cells[i, j].SetToRandomState();
                }
            }
        }

        public static void Tick()
        {
            var nextGeneration = new Cell[_cells.GetLength(0), _cells.GetLength(1)];
            for (var i = 0; i < nextGeneration.GetLength(0); i++)
            {
                for (var j = 0; j < nextGeneration.GetLength(1); j++)
                {
                    nextGeneration[i, j] = new Cell(false);
                }
            }

            for (var i = 0; i < _cells.GetLength(0); i++)
            {
                for (var j = 0; j < _cells.GetLength(1); j++)
                {
                    var nextGenerationCellResult = CellStatusResult.NoChange;

                    var neighbours = GetNeighbours(_cells, i, j);

                    foreach (var rule in rules)
                    {
                        var result = rule(neighbours, _cells[i, j].CellStatus());

                        if (result == CellStatusResult.Live || result == CellStatusResult.Die)
                        {
                            nextGenerationCellResult = result;
                        }
                    }

                    //Apply Rule to next generation
                    switch (nextGenerationCellResult)
                    {
                        case CellStatusResult.Live:
                            nextGeneration[i, j].SetToAlive();
                            break;
                        case CellStatusResult.Die:
                            nextGeneration[i, j].SetToDie();
                            break;
                        case CellStatusResult.NoChange:
                            nextGeneration[i, j] = _cells[i, j];
                            break;
                    }
                }
            }
            _cells = nextGeneration;
        }

        public static string GetNeighbours(Cell[,] cells, int x, int y)
        {
            var neighbours = new[]
            {
                PrintSafeChar(cells, x - 1, y - 1), PrintSafeChar(cells, x - 1, y), PrintSafeChar(cells, x - 1, y + 1),
                PrintSafeChar(cells, x    , y - 1), SelfChar              , PrintSafeChar(cells, x    , y + 1),
                PrintSafeChar(cells, x + 1, y - 1), PrintSafeChar(cells, x + 1, y), PrintSafeChar(cells, x + 1, y + 1)
            };

            return new string(neighbours);
        }

        private static char PrintSafeChar(Cell[,] cells, int x, int y)
        {
            if (x > -1 && y > -1 && y < (cells.GetUpperBound(1) + 1) && x < (cells.GetUpperBound(0) + 1))
            {
                return cells[x, y].Print();
            }
            else
            {
                return OutOfBoundsChar;
            }
        }
    }
}
