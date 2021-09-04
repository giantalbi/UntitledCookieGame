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
        Ingredient[,] IngredientGrid { get; set; }

        Ingredient[,] GenerateGameBoard();
        /// <summary>
        /// Moves the cursor
        /// </summary>
        /// <param name="dir">Direction vector</param>
        /// <returns>New cursor position</returns>
        Vector2 MoveCursor(Vector2 dir);
        void ShiftAxis(Vector2 pos, Vector2 dir);
        void ColShiftUp(int colIndex);
        void ColShiftDown(int colIndex);
        void RowShiftLeft(int rowIndex);
        void RowShiftRight(int rowIndex);

        /// <summary>
        /// Generates Row of Ingredients from top of grid
        /// </summary>
        void GenerateNewRow();

        /// <summary>
        /// Generates Column of Ingredients from right side of grid
        /// </summary>
        void GenerateNewCol();

        bool CheckColumnMatch(int col, out Ingredient matchType);
        bool CheckRowMatch(int row, out Ingredient matchType);

        void ClearColumn(int colIndex);
        void ClearRow(int rowIndex);
        /// <summary>
        /// Push non-empty columns to the left
        /// </summary>
        void ShiftClearedColumn();
        /// <summary>
        /// Push non-empty rows downward
        /// </summary>
        void ShiftClearedRow();
    }
}
