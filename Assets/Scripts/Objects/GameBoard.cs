using GranCook.Extensions;
using GranCook.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GranCook.Objects
{
    public class GameBoard : IGameBoard
    {
        private const int GRID_SIZE = 5;
        public int[,] Grid { get; set; }
        public Vector2 Cursor { get; set; }

        public GameBoard()
        {
            var grid = GenerateGameBoard();
            Grid = grid;

            Cursor = new Vector2(GRID_SIZE / 2, GRID_SIZE / 2);
        }

        public int[,] GenerateGameBoard()
        {
            int[,] grid = new int[GRID_SIZE, GRID_SIZE];
            grid.FillAll();

            return grid;
        }

        #region Check for matching rows/cols


        public bool CheckColumnMatch(int colIndex, out int matchIngredient)
        {
            var col = Grid.GetColumn(colIndex);
            matchIngredient = col[0];

            return col.AllMatch();
        }

        public bool CheckRowMatch(int rowIndex, out int matchIngredient)
        {
            var row = Grid.GetRow(rowIndex);
            matchIngredient = row[0];

            return row.AllMatch();
        }

        #endregion

        #region Grid Clearing and Refilling


        public void ClearColumn(int colIndex)
        {
            int[] col = new int[GRID_SIZE];
            col.Populate(-1);
            Grid.SetColumn(colIndex, col);
        }

        public void ClearRow(int rowIndex)
        {
            int[] row = new int[GRID_SIZE];
            row.Populate(-1);
            Grid.SetRow(rowIndex, row);
        }

        public void ShiftClearedColumns()
        {
            int[] row = Grid.GetRow(0);
            int[] emptyCol = new int[GRID_SIZE];
            emptyCol.Populate(-1);
            IList<int[]> nonEmptyCols = new List<int[]>();

            for (int i = 0; i < row.Length; i++)
            {
                if (row[i] != -1)
                {
                    nonEmptyCols.Add(Grid.GetColumn(i));
                }
            }

            for (int i = 0; i < GRID_SIZE; i++)
            {
                int[] col = nonEmptyCols.ElementAtOrDefault(i) ?? emptyCol;
                Grid.SetColumn(i, col);
            }
        }

        public void ShiftClearedRows()
        {
            int[] col = Grid.GetColumn(0);
            int[] emptyRow = new int[GRID_SIZE];
            emptyRow.Populate(-1);
            Stack<int[]> nonEmptyRows = new Stack<int[]>();

            for (int i = 0; i < col.Length; i++)
            {
                if (col[i] != -1)
                {
                    nonEmptyRows.Push(Grid.GetRow(i));
                }
            }

            for (int i = GRID_SIZE - 1; i >= 0; i--)
            {
                int[] row;
                try
                {
                    row = nonEmptyRows.Pop();
                }
                catch(InvalidOperationException e)
                {
                    row = emptyRow;
                }
                Grid.SetRow(i, row);
            }
        }

        public void GenerateNewCols(int colAmount)
        {
            Grid.FillNewCols(colAmount);
        }

        public void GenerateNewRows(int rowAmount)
        {
            Grid.FillNewRows(rowAmount);
        }

        #endregion

        #region Grid Movement

        public void ShiftAxis(Vector2 pos, Vector2 dir)
        {

            switch(dir)
            {
                case Vector2 v when v == Vector2.up:
                    ColShiftUp(Convert.ToInt32(pos.y));
                    break;
                case Vector2 v when v == Vector2.down:
                    ColShiftDown(Convert.ToInt32(pos.y));
                    break;
                case Vector2 v when v == Vector2.left:
                    RowShiftLeft(Convert.ToInt32(pos.x));
                    break;
                case Vector2 v when v == Vector2.right:
                    RowShiftRight(Convert.ToInt32(pos.x));
                    break;
            }
        }

        public void ColShiftDown(int colIndex)
        {
            int[] col = Grid.GetColumn(colIndex);

            var temp = col[col.Length - 1];
            Array.Copy(col, 0, col, 1, col.Length - 1);
            col[0] = temp;

            Grid.SetColumn(colIndex, col);
        }

        public void ColShiftUp(int colIndex)
        {
            int[] col = Grid.GetColumn(colIndex);

            var temp = col[0];
            Array.Copy(col, 1, col, 0, col.Length - 1);
            col[col.Length - 1] = temp;

            Grid.SetColumn(colIndex, col);
        }

        public void RowShiftLeft(int rowIndex)
        {
            int[] row = Grid.GetRow(rowIndex);

            var temp = row[0];
            Array.Copy(row, 1, row, 0, row.Length - 1);
            row[row.Length - 1] = temp;

            Grid.SetRow(rowIndex, row);
        }

        public void RowShiftRight(int rowIndex)
        {
            int[] row = Grid.GetRow(rowIndex);

            var temp = row[row.Length - 1];
            Array.Copy(row, 0, row, 1, row.Length - 1);
            row[0] = temp;

            Grid.SetRow(rowIndex, row);
        }

        #endregion

        #region Cursor Movement
        public void MoveCursor(Vector2 dir)
        {
            Vector2 newCursor = Cursor;
            newCursor.x -= dir.y;
            newCursor.y += dir.x;

            if (newCursor.x < 0)
                newCursor.x = GRID_SIZE - 1;
            else if (newCursor.x >= GRID_SIZE)
                newCursor.x = 0;

            if (newCursor.y < 0)
                newCursor.y = GRID_SIZE - 1;
            else if (newCursor.y >= GRID_SIZE)
                newCursor.y = 0;

            Cursor = newCursor;
        }
        #endregion
    }
}
