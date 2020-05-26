using GameOfLife;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLifeTests
{
    [TestClass]
    public class RulesTests
    {
        [DataTestMethod]
        [DataRow("....0....", DisplayName = "No neighbours, Alive should Die")]
        [DataRow("X...0....", DisplayName = "One neighbour, Alive should Die")]
        [DataRow("BBBB0.B..", DisplayName = "No neighbours at board edge, Alive should Die")]
        [DataRow("BBBB0.X..", DisplayName = "No neighbours at board edge, Alive should Die")]
        public void Underpopulated_Alive_Should_Die(string neighbours)
        {
            // Arrange
            CellStatus currentStatus = CellStatus.Alive;

            // Act
            CellStatusResult result = Rules.Underpopulated(neighbours, currentStatus);

            // Assert
            Assert.AreEqual(CellStatusResult.Die, result);
        }

        [DataTestMethod]
        [DataRow("....0....", DisplayName = "No neighbours, Dead no change")]
        [DataRow("X...0....", DisplayName = "One neighbour, Dead no change")]
        [DataRow("XX..0....", DisplayName = "Two neighbours, Dead no change")]
        [DataRow("XXX.0....", DisplayName = "Three neighbours, Dead no change")]
        [DataRow("XXXX0....", DisplayName = "Four neighbours, Dead no change")]
        [DataRow("XXXX0X...", DisplayName = "Five neighbours, Dead no change")]
        [DataRow("XXXX0XX..", DisplayName = "Six neighbours, Dead no change")]
        [DataRow("XXXX0XXX.", DisplayName = "Seven neighbours, Dead no change")]
        [DataRow("XXXX0XXXX", DisplayName = "Eight neighbours, Dead no change")]
        [DataRow("BBBB0.B..", DisplayName = "No neighbours at board edge, Dead no change")]
        public void Underpopulated_Dead_Stays_Dead(string neighbours)
        {
            // Arrange
            CellStatus currentStatus = CellStatus.Dead;

            // Act
            CellStatusResult result = Rules.Underpopulated(neighbours, currentStatus);

            // Assert
            Assert.AreEqual(CellStatusResult.NoChange, result);
        }

        [DataTestMethod]
        [DataRow("XXX.0....", DisplayName = "Three neighbours, Alive no change")]
        [DataRow("BBBX0X.X.", DisplayName = "Three neighbours at board edge, Alive no change")]
        public void Well_Populated_Alive_Stays_Alive(string neighbours)
        {
            // Arrange
            CellStatus currentStatus = CellStatus.Alive;

            // Act
            CellStatusResult result = Rules.WellPopulated(neighbours, currentStatus);

            // Assert
            Assert.AreEqual(CellStatusResult.Live, result);
        }

        [DataTestMethod]
        [DataRow("XXXX0....", DisplayName = "Four neighbours, Dead no change")]
        [DataRow("BBB.0X...", DisplayName = "One neighbour at board edge, Dead no change")]
        public void Not_Well_Populated_Dead_No_Change(string neighbours)
        {
            // Arrange
            CellStatus currentStatus = CellStatus.Dead;

            // Act
            CellStatusResult result = Rules.WellPopulated(neighbours, currentStatus);

            // Assert
            Assert.AreEqual(CellStatusResult.NoChange, result);
        }

        [DataTestMethod]
        [DataRow("XXXX0....", DisplayName = "Four neighbours, Alive should Die")]
        [DataRow("XXXX0X...", DisplayName = "Five neighbours, Alive should Die")]
        [DataRow("XXXX0XX..", DisplayName = "Six neighbours, Alive should Die")]
        [DataRow("XXXX0XXX.", DisplayName = "Seven neighbours, Alive should Die")]
        [DataRow("XXXX0XXXX", DisplayName = "Eight neighbours, Alive should Die")]
        public void Alive_And_Overpopulated_Should_Die(string neighbours)
        {
            // Arrange
            CellStatus currentStatus = CellStatus.Alive;

            // Act
            CellStatusResult result = Rules.OverPopulated(neighbours, currentStatus);

            // Assert
            Assert.AreEqual(CellStatusResult.Die, result);
        }

        [DataTestMethod]
        [DataRow("X...0....", DisplayName = "One neighbours, Alive no change")]
        [DataRow("X...0X...", DisplayName = "Two neighbours, Alive no change")]
        [DataRow("X...0XX..", DisplayName = "Three neighbours, Alive no change")]
        public void Alive_And_Not_Overpopulated_No_Change(string neighbours)
        {
            // Arrange
            CellStatus currentStatus = CellStatus.Alive;

            // Act
            CellStatusResult result = Rules.OverPopulated(neighbours, currentStatus);

            // Assert
            Assert.AreEqual(CellStatusResult.NoChange, result);
        }

        [DataTestMethod]
        [DataRow("XXX.0....", DisplayName = "Three neighbours, Dead will Live")]
        [DataRow("BBBX0XX..", DisplayName = "Three neighbours at board edge, Dead Will Live")]
        public void Dead_And_Correct_Amount_Of_Neighbours_Dead_Set_To_Live(string neighbours)
        {
            // Arrange
            CellStatus currentStatus = CellStatus.Dead;

            // Act
            CellStatusResult result = Rules.Reproduce(neighbours, currentStatus);

            // Assert
            Assert.AreEqual(CellStatusResult.Live, result);
        }

        [DataTestMethod]
        [DataRow("....0....", DisplayName = "No neighbours, Dead no change")]
        [DataRow("X...0....", DisplayName = "One neighbour, Dead no change")]
        [DataRow("XX..0....", DisplayName = "Two neighbours, Dead no change")]
        // Three neighbours no
        [DataRow("XXXX0....", DisplayName = "Four neighbours, Dead no change")]
        [DataRow("XXXX0X...", DisplayName = "Five neighbours, Dead no change")]
        [DataRow("XXXX0XX..", DisplayName = "Six neighbours, Dead no change")]
        [DataRow("XXXX0XXX.", DisplayName = "Seven neighbours, Dead no change")]
        [DataRow("XXXX0XXXX", DisplayName = "Eight neighbours, Dead no change")]
        public void Dead_And_InCorrect_Amount_Of_Neighbours_Dead_Stays_Dead(string neighbours)
        {
            // Arrange
            CellStatus currentStatus = CellStatus.Dead;

            // Act
            CellStatusResult result = Rules.Reproduce(neighbours, currentStatus);

            // Assert
            Assert.AreEqual(CellStatusResult.NoChange, result);
        }
    }
}
