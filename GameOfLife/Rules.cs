using System;

namespace GameOfLife
{
    public static class Rules
    {
        // Alive cells with fewer than two live neighbours, die through under population.
        public static CellStatusResult Underpopulated(string neighbours, CellStatus cellStatus)
        {
            if (cellStatus == CellStatus.Dead) return CellStatusResult.NoChange;

            var count = neighbours.Split('X').Length - 1;
            if (count < 2)
            {
                return CellStatusResult.Die;
            };
            return CellStatusResult.NoChange;
        }

        // Alive cells with two or three live neighbours, lives on to the next generation.
        public static CellStatusResult WellPopulated(string neighbours, CellStatus cellStatus)
        {
            var count = neighbours.Split('X').Length - 1;
            if ((count == 2 || count == 3) && cellStatus == CellStatus.Alive)
            {
                return CellStatusResult.Live;
            }
            return CellStatusResult.NoChange;
        }

        // Alive cells with more than three live neighbours, die through over population.
        public static CellStatusResult OverPopulated(string neighbours, CellStatus cellStatus)
        {
            if (cellStatus == CellStatus.Dead) return CellStatusResult.NoChange;

            var count = neighbours.Split('X').Length - 1;
            if (count > 3)
            {
                return CellStatusResult.Die;
            }
            return CellStatusResult.NoChange;
        }

        // Dead cells with exactly three live neighbours becomes a live cell, through reproduction.
        public static CellStatusResult Reproduce(string neighbours, CellStatus cellStatus)
        {
            var count = neighbours.Split('X').Length - 1;
            if (count == 3 && cellStatus == CellStatus.Dead)
            {
                return CellStatusResult.Live;
            }
            return CellStatusResult.NoChange;
        }
    }
}