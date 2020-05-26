using GameOfLife;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLifeTests
{
    [TestClass]
    public class CellTests
    {
        [TestMethod]
        public void Cell_Creates_Dead_OK()
        {
            // Arrange
            Cell cell;

            // Act
            cell = new Cell();

            // Assert
            Assert.IsNotNull(cell);
        }

        [TestMethod]
        public void Cell_Creates_Alive_OK()
        {
            // Arrange
            Cell cell;

            // Act
            cell = new Cell(true);

            // Assert
            Assert.IsNotNull(cell);
        }

        [TestMethod]
        public void Cell_IsAlive_Returns_True_When_Alive()
        {
            // Arrange
            Cell cell = new Cell(true);

            // Act
            bool result = cell.IsAlive();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Cell_IsAlive_Returns_False_When_Dead()
        {
            // Arrange
            Cell cell = new Cell();

            // Act
            bool result = cell.IsAlive();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Cell_Dead_Prints_Dot()
        {
            // Arrange
            Cell cell = new Cell();

            // Act
            char result = cell.Print();

            // Assert
            Assert.AreEqual('.', result);
        }

        [TestMethod]
        public void Cell_Alive_Prints_X()
        {
            // Arrange
            Cell cell = new Cell(true);

            // Act
            char result = cell.Print();

            // Assert
            Assert.AreEqual('X', result);
        }

        [TestMethod]
        public void Cell_Set_Alive_Prints_X()
        {
            // Arrange
            Cell cell = new Cell();

            // Act
            cell.SetToAlive();
            char result = cell.Print();

            // Assert
            Assert.AreEqual('X', result);
        }

        [TestMethod]
        public void Cell_Set_Dead_Prints_Dot()
        {
            // Arrange
            Cell cell = new Cell(true);

            // Act
            cell.SetToDie();
            char result = cell.Print();

            // Assert
            Assert.AreEqual('.', result);
        }

        [TestMethod]
        public void Cell_Dead_Status_Dead()
        {
            // Arrange
            Cell cell = new Cell();

            // Act
            CellStatus result = cell.CellStatus();

            // Assert
            Assert.AreEqual(CellStatus.Dead, result);
        }

        [TestMethod]
        public void Cell_Alive_Status_Alive()
        {
            // Arrange
            Cell cell = new Cell(true);

            // Act
            CellStatus result = cell.CellStatus();

            // Assert
            Assert.AreEqual(CellStatus.Alive, result);
        }

        [TestMethod]
        public void Cell_Random_State_Dead_Or_Alive()
        {
            // Arrange
            Cell cell = new Cell();
            int countX = 0;
            char result;

            // Act
            for(int i = 0; i < 1000; i++)
            {
                cell.SetToRandomState();
                result = cell.Print();
                if (result == 'X') countX++;
            }

            // Assert
            Assert.AreNotEqual(0, countX);
        }
    }
}
