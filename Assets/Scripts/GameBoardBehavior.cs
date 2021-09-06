using GranCook.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GranCook
{
    public class GameBoardBehavior : MonoBehaviour
    {
        public float Length => gridSize * cellInBetweenDistance;
        public IPlayer Player { get; set; }

        public int gridSize = 5;
        public float cellScale = 1f;
        public float cellInBetweenDistance = 1f;

        public float cellMovementSpeed = .5f;

        public float Distance => Length / gridSize;


        public Color[] debugCellColors = new Color[6];
        public Color debugEmptyColor;
        public bool debugShowGridCells = false;

        int rowToShiftIndex = -1;
        int colToShiftIndex = -1;
        public bool isShifting = false;
        public int shiftDirection = 1;
        Transform shiftLastCell;
        Transform shiftFirstCell;
        Transform cellToDelete;
        Vector2 lastCellDestination;
        bool rowShiftInitiated = false;
        bool colShiftInitiated = false;
        IList<IngredientMatch> ingredientMatches;

        Vector2[,] grid;

        Transform cellContainer;

        GameObject cursor;

        // Start is called before the first frame update
        void Start()
        {
            cellContainer = transform.Find("GameBoard");
            grid = GetGrid(cellContainer.position);
            ingredientMatches = new List<IngredientMatch>();

            GenerateCursor();
            GenerateGridGameObjects();

        }

        // Update is called once per frame
        void Update()
        {
            // Move the cursor 
            Vector2 cursorPosInBoard = Player.GameBoard.Cursor;
            Vector3 cursorPosInWorld = grid[(int)cursorPosInBoard.x, (int)cursorPosInBoard.y];
            cursorPosInWorld.z = -1;
            cursor.transform.position = Vector2.Lerp(cursor.transform.position, cursorPosInWorld, cellMovementSpeed + Time.deltaTime);

            if (rowToShiftIndex > -1)
            {
                ShiftRow(rowToShiftIndex, shiftDirection);
            }

            if (colToShiftIndex > -1)
            {
                ShiftCol(colToShiftIndex, shiftDirection);
            }

            if (!isShifting)
            {
                if (rowShiftInitiated)
                {
                    CheckForMatchingCols();
                }

                if (colShiftInitiated)
                {
                    CheckForMatchingRows();
                }
            }

        }

        public void CheckForMatchingRows()
        {
            for (int i = 0; i < gridSize; i++)
            {
                if (Player.GameBoard.CheckRowMatch(i, out int matchingIngredientType))
                {
                    ingredientMatches.Add(new IngredientMatch(i, matchingIngredientType));
                }
            }

            if (ingredientMatches.Any())
            {
                ClearMatchingRows();
                RowClearAnimation();
            }

            colShiftInitiated = false;
        }

        public void CheckForMatchingCols()
        {
            for (int i = 0; i < gridSize; i++)
            {
                if (Player.GameBoard.CheckColumnMatch(i, out int matchingIngredientType))
                {
                    ingredientMatches.Add(new IngredientMatch(i, matchingIngredientType));
                }
            }

            if (ingredientMatches.Any())
            {
                ClearMatchingCols();
                ColClearAnimation();
            }

            rowShiftInitiated = false;
        }

        public void ClearMatchingRows()
        {
            int matchCount = ingredientMatches.Count;
            foreach (var match in ingredientMatches)
            {
                Player.GameBoard.ClearRow(match.Index);
            }
            Player.GameBoard.ShiftClearedRows();
            Player.GameBoard.GenerateNewRows(matchCount);
            ingredientMatches.Clear();
            rowShiftInitiated = true;
        }

        public void ClearMatchingCols()
        {
            int matchCount = ingredientMatches.Count;
            foreach (var match in ingredientMatches)
            {
                Player.GameBoard.ClearColumn(match.Index);
            }
            Player.GameBoard.ShiftClearedColumns();
            Player.GameBoard.GenerateNewCols(matchCount);
            ingredientMatches.Clear();
            colShiftInitiated = true;
        }

        public void RowClearAnimation()
        {
            // TODO actual animation
            foreach (Transform child in cellContainer)
            {
                Destroy(child.gameObject);
            }
            GenerateGridGameObjects();
        }

        public void ColClearAnimation()
        {
            // TODO actual animation
            foreach (Transform child in cellContainer)
            {
                Destroy(child.gameObject);
            }
            GenerateGridGameObjects();
        }

        public void ShiftAxis(Vector2 position, Vector2 direction)
        {
            if (direction.x == 0)
            {
                StartColShift((int)position.y, (int)direction.y);
            }
            else
            {
                StartRowShift((int)position.x, (int)direction.x);
            }
        }

        #region Grid Row Movement
        public void StartRowShift(int rowIndex, int direction)
        {
            rowToShiftIndex = rowIndex;
            shiftDirection = direction;
        }

        void ShiftRow(int rowIndex, int direction)
        {
            if (!isShifting)
            {
                isShifting = true;
                rowShiftInitiated = true;
                ShiftRowInit(rowIndex, direction);
            }
            ShiftRowAnimation(rowIndex, direction);

            Vector2 v2 = shiftLastCell.position;
            if (Vector2.Distance(v2, lastCellDestination) < 0.008f)
            {
                ShiftAnimationCallback();
            }
        }

        void ShiftRowInit(int rowIndex, int direction)
        {
            if (direction > 0)
            {
                cellToDelete = cellContainer.Find($"{rowIndex},{gridSize - 1}");
                shiftLastCell = Instantiate(cellToDelete);
                shiftLastCell.parent = cellContainer;
                shiftFirstCell = cellContainer.Find($"{rowIndex},{0}");
                shiftLastCell.position = shiftFirstCell.position + (Vector3.left * Distance);
            }
            else
            {
                cellToDelete = cellContainer.Find($"{rowIndex},{0}");
                shiftLastCell = Instantiate(cellToDelete);
                shiftLastCell.parent = cellContainer;
                shiftFirstCell = cellContainer.Find($"{rowIndex},{gridSize - 1}");
                shiftLastCell.position = shiftFirstCell.position + (Vector3.right * Distance);
            }
            lastCellDestination = shiftFirstCell.position;
        }

        void ShiftRowAnimation(int rowIndex, int direction)
        {
            for (int i = 0; i < gridSize; i++)
            {
                Vector2 cellPos = grid[rowIndex, i];
                Vector3 destinationPos = new Vector3(cellPos.x + (Distance * direction), cellPos.y, 1);
                var cellObject = cellContainer.Find($"{rowIndex},{i}");
                cellObject.position = Vector3.Lerp(cellObject.position, destinationPos, cellMovementSpeed + Time.deltaTime);
            }
            shiftLastCell.position = Vector3.Lerp(shiftLastCell.position, lastCellDestination, cellMovementSpeed + Time.deltaTime);
        }

        #endregion

        #region Grid Column Movement
        public void StartColShift(int colIndex, int direction)
        {
            colToShiftIndex = colIndex;
            shiftDirection = direction;
        }

        void ShiftCol(int colIndex, int direction)
        {
            if (!isShifting)
            {
                isShifting = true;
                colShiftInitiated = true;
                ShiftColInit(colIndex, direction);
            }
            ShiftColAnimation(colIndex, direction);

            Vector2 v2 = shiftLastCell.position;
            if (Vector2.Distance(v2, lastCellDestination) < 0.008f)
            {
                ShiftAnimationCallback();
            }
        }

        void ShiftColInit(int colIndex, int direction)
        {
            isShifting = true;
            if (direction > 0)
            {
                cellToDelete = cellContainer.Find($"{0},{colIndex}");
                shiftLastCell = Instantiate(cellToDelete);
                shiftLastCell.parent = cellContainer;
                shiftFirstCell = cellContainer.Find($"{gridSize - 1},{colIndex}");
                shiftLastCell.position = shiftFirstCell.position + (Vector3.down * Distance);
            }
            else
            {
                cellToDelete = cellContainer.Find($"{gridSize - 1},{colIndex}");
                shiftLastCell = Instantiate(cellToDelete);
                shiftLastCell.parent = cellContainer;
                shiftFirstCell = cellContainer.Find($"{0},{colIndex}");
                shiftLastCell.position = shiftFirstCell.position + (Vector3.up * Distance);
            }
            lastCellDestination = shiftFirstCell.position;
        }

        void ShiftColAnimation(int colIndex, int direction)
        {
            for (int i = 0; i < gridSize; i++)
            {
                Vector2 cellPos = grid[i, colIndex];
                Vector3 destinationPos = new Vector3(cellPos.x, cellPos.y + (Distance * direction), 1);
                var cellObject = cellContainer.Find($"{i},{colIndex}");
                cellObject.position = Vector3.Lerp(cellObject.position, destinationPos, cellMovementSpeed + Time.deltaTime);
            }
            shiftLastCell.position = Vector3.Lerp(shiftLastCell.position, lastCellDestination, cellMovementSpeed + Time.deltaTime);
        }

        #endregion

        void ShiftAnimationCallback()
        {
            rowToShiftIndex = -1;
            colToShiftIndex = -1;
            isShifting = false;

            foreach (Transform child in cellContainer)
            {
                Destroy(child.gameObject);
            }
            GenerateGridGameObjects();
        }

        void GenerateCursor()
        {
            GameObject cursorPrefab = Resources.Load("Prefabs/UIs/Main/Cursor") as GameObject;
            cursor = Instantiate(cursorPrefab);
            cursor.transform.parent = transform;
            cursor.transform.localPosition = Vector3.forward * -1;

        }

        void GenerateGridGameObjects()
        {
            GameObject cellPrefab = Resources.Load("Prefabs/UIs/Main/Cell") as GameObject;

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    GameObject cellObj = Instantiate(cellPrefab);
                    int cellValue = Player.GameBoard.Grid[i, j];

                    cellObj.name = $"{i},{j}";
                    cellObj.transform.parent = cellContainer;
                    Vector3 pos = grid[i, j];
                    pos.z = 1;
                    cellObj.transform.position = pos;
                    cellObj.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = cellValue.ToString();
                    cellObj.GetComponent<SpriteRenderer>().color = cellValue >= 0 ? debugCellColors[cellValue] : debugEmptyColor;
                }
            }
        }

        Vector2[,] GetGrid(Vector2 gridOrigin)
        {
            Vector2[,] grid = new Vector2[gridSize, gridSize];
            float half = Length / 2;
            float distance = Length / gridSize;
            Vector2 origin = new Vector2(-half + distance / 2, half - distance / 2);

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    grid[i, j] = gridOrigin + origin;

                    origin.x += distance;
                }

                origin.x = -half + distance / 2;
                origin.y -= distance;
            }

            return grid;
        }

        private void OnDrawGizmos()
        {
            if (debugShowGridCells)
            {
                Vector2 pos = transform.Find("GameBoard").position;
                var grid = GetGrid(pos);
                var half = Length / 2;

                int count = 0;

                for (int i = 0; i < gridSize; i++)
                {
                    for (int j = 0; j < gridSize; j++)
                    {
                        if(count == 0)
                            Gizmos.color = Color.red;
                        else if(count == (gridSize * gridSize) - 1)
                            Gizmos.color = Color.green;
                        else
                            Gizmos.color = Color.white;

                        Gizmos.DrawSphere(grid[i, j], cellScale / 2);
                        count++;
                    }
                }

                // Top Line
                Gizmos.DrawLine(pos + new Vector2(-half, half), pos + new Vector2(half, half));

                // Left Line
                Gizmos.DrawLine(pos + new Vector2(-half, half), pos + new Vector2(-half, -half));

                // Bottom Line
                Gizmos.DrawLine(pos + new Vector2(-half, -half), pos + new Vector2(half, -half));

                // Right Line
                Gizmos.DrawLine(pos + new Vector2(half, -half), pos + new Vector2(half, half));
            }
        }

    }
}
