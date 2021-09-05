using GranCook.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GranCook
{   
    public class GameBoardBehavior : MonoBehaviour
    {
        public float Length => gridSize * cellInBetweenDistance;
        public IPlayer Player { get; set; }

        public int gridSize = 5;
        public float cellScale = 1f;
        public float cellInBetweenDistance = 1f;


        public Color[] debugCellColors = new Color[6];
        public bool debugShowGridCells = false;

        Vector2[,] grid;

        Transform cellContainer;

        GameObject cursor;

        // Start is called before the first frame update
        void Start()
        {
            cellContainer = transform.Find("GameBoard");
            grid = GetGrid(cellContainer.position);

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
            cursor.transform.position = Vector2.Lerp(cursor.transform.position, cursorPosInWorld, .2f);
        }

        void GenerateCursor()
        {
            GameObject cursorPrefab = Resources.Load("Prefabs/UIs/Main/Cursor") as GameObject;
            cursor = Instantiate(cursorPrefab);
            cursor.transform.parent = cellContainer;
            cursor.transform.localPosition = Vector2.zero;
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

                    cellObj.transform.parent = cellContainer;
                    Vector3 pos = grid[i, j];
                    pos.z = 1;
                    cellObj.transform.position = pos;
                    cellObj.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = cellValue.ToString();
                    cellObj.GetComponent<SpriteRenderer>().color = debugCellColors[cellValue];
                }
            }
        }


        Vector2[,] GetGrid(Vector2 gridOrigin)
        {
            Vector2[,] grid = new Vector2[gridSize, gridSize];
            float half = Length / 2;
            float distance = Length / gridSize;
            Vector2 origin = new Vector2(-half + distance /2 , half - distance / 2);

            for (int i = 0; i < gridSize; i++)
            {
                for(int j = 0; j < gridSize; j++)
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
