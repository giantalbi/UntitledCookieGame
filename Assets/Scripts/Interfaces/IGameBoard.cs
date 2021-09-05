using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GranCook.Interfaces
{
    public interface IGameBoard
    {
        int[,] Grid { get; set; }
        Vector2 Cursor { get; set; }

        int[,] GenerateGameBoard();
        /// <summary>
        /// Moves the cursor
        /// </summary>
        /// <param name="dir">Direction vector</param>
        /// <returns>New cursor position</returns>
        void MoveCursor(Vector2 dir);
        void ShiftAxis(Vector2 pos, Vector2 dir);
        void ColShiftUp(int colIndex);
        void ColShiftDown(int colIndex);
        void RowShiftLeft(int rowIndex);
        void RowShiftRight(int rowIndex);

        /// <summary>
        /// Generates Rows of Ingredients from top of grid
        /// </summary>
        /// /// <param name="rowAmount">Number of rows to be generated</param>
        void GenerateNewRows(int rowAmount);

        /// <summary>
        /// Generates Columns of Ingredients from right side of grid
        /// </summary>
        /// <param name="colAmount">Number of columns to be generated</param>
        void GenerateNewCols(int colAmount);

        bool CheckColumnMatch(int colIndex, out int matchIngredient);
        bool CheckRowMatch(int rowIndex, out int matchIngredient);

        void ClearColumn(int colIndex);
        void ClearRow(int rowIndex);
        /// <summary>
        /// Push non-empty columns to the left
        /// </summary>
        void ShiftClearedColumns();
        /// <summary>
        /// Push non-empty rows downward
        /// </summary>
        void ShiftClearedRows();
    }
}
