using System;

namespace GameOfLife
{
    public class Cell
    {
        public bool State { get; private set; }

        public Cell(bool state = false) => State = state;

        public void SetToAlive() => State = true;
        public void SetToDie() => State = false;
        public bool IsAlive() => State;

        public char Print() => State ? 'X' : '.';

        public CellStatus CellStatus()
        {
            return State ? GameOfLife.CellStatus.Alive : GameOfLife.CellStatus.Dead;
        }

        public void SetToRandomState() => State = new Random().Next(100) % 2 == 0;
    }
}
