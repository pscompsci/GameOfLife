using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOfLife;
using System;
using System.IO;

namespace GameOfLifeTests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void Create_Game_OK()
        {
            // Arrange
            Game game;

            // Act
            game = new Game();

            //Assert
            Assert.IsNotNull(game);
        }

        [TestMethod]
        public void PrintHorizontalLine_Prints_OK()
        {
            // Arrange
            // . . .
            // X X X
            // . X .
            Cell[,] cells = new Cell[3, 3] {
                { new Cell(),     new Cell(),     new Cell() },
                { new Cell(true), new Cell(true), new Cell(true) },
                { new Cell(),     new Cell(true), new Cell() } };

            // Act
            string result = Game.PrintHorizontalLine(cells, 2);

            // Assert
            Assert.AreEqual(".X.", result);
        }

        [TestMethod]
        public void PrintCellInConsole_Prints_OK()
        {
            // Arrange
            Cell[,] cells = new Cell[1, 3] {
                { new Cell(),     new Cell(true),     new Cell() }
            };

            // Act
            using var consoleOutput = new ConsoleOutput();
            Game.PrintcellInConsole(cells);

            // Assert
            Assert.AreEqual(".X.", consoleOutput.GetOutput().Trim());
        }

        [TestMethod]
        public void GetNeighbours_Returns_Correct_String()
        {
            // Arrange
            // . . .
            // X X X
            // . X .
            Cell[,] cells = new Cell[3, 3] {
                { new Cell(),     new Cell(),     new Cell() },
                { new Cell(true), new Cell(true), new Cell(true) },
                { new Cell(),     new Cell(true), new Cell() } };

            // Act
            // cell of interest returns as 0 in the string
            string result = Game.GetNeighbours(cells, 1, 1);

            // Assert
            Assert.AreEqual("...X0X.X.", result);
        }
    }

    // Reference: 
    // https://www.codeproject.com/articles/501610/getting-console-output-within-a-unit-test
    // See that post for information about this approach
    public class ConsoleOutput : IDisposable
    {
        private StringWriter _stringWriter;

        public ConsoleOutput()
        {
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);
        }

        public string GetOutput() => _stringWriter.ToString();

        public void Dispose() => Console.SetOut(Console.Out);
    }

}
