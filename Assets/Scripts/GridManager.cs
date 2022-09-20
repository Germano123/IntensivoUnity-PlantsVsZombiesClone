using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    public bool[,] Grid { get; private set; }
    // how many rows and columns
    [SerializeField] int gridRows;
    [SerializeField] int gridColumns;
    // the size of the grid cell
    [SerializeField] float gridCellHeight;
    [SerializeField] float gridCellWidth;

    public Vector2 GridOffset { get; private set; }

    // Start is called before the first frame update
    void Start() {
        GridOffset = new Vector2(gridCellWidth, gridCellHeight);
        StartGrid();
    }

    void StartGrid() {
        Grid = new bool[gridRows,gridColumns];
        for(int i = 0; i < gridRows; i++) {
            for(int j = 0; j < gridColumns; j++) {
                Grid[i, j] = false;
            }
        }
    }

    public Vector2Int GetGridCoordinate(float x, float y) {
        int xPos = (int)((transform.position.x - x) / gridCellWidth);
        int yPos = (int)((transform.position.y - y) / gridCellHeight);
        return new Vector2Int(xPos, yPos);
    }

    bool InsideGridDimension(float x, float y) {
        // retrun true if the given x and y are inside grid dimensions
        if (x >= transform.position.x && y <= transform.position.y &&
        x <= transform.position.x + gridRows * gridCellWidth &&
        y >= transform.position.y + gridColumns * -gridCellHeight) return true;
        return false;
    }

    public Vector3 GetGridPosition(float xPos, float yPos) {
        // Debug.Log($"X and Y Pos are: ({xPos}, {yPos})");
        if (InsideGridDimension(xPos, yPos)) {
            Vector2Int pos = GetGridCoordinate(xPos, yPos);
            // Debug.Log($"Inside Grid with X, Y: ({-xPos}, {-yPos})");
            xPos = transform.position.x + pos.x * -GridOffset.x + GridOffset.x/2;
            yPos = transform.position.y + pos.y * -GridOffset.y - GridOffset.y/2;
            return new Vector3(xPos, yPos, 0f);
        } else return Vector3.zero;
    }

    public bool HasAllyAt(int x, int y) {
        return Grid[Mathf.Abs(x), Mathf.Abs(y)];
    }

    public void AddAllyAt(int x, int y) {
        if (!HasAllyAt(x, y)) {
            Grid[Mathf.Abs(x), Mathf.Abs(y)] = true;
        }
    }

    public void RemoveAllyAt(int x, int y) {
        Grid[Mathf.Abs(x), Mathf.Abs(y)] = false;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Vector3 offset = transform.position + new Vector3(gridCellWidth/2, -gridCellHeight/2, 0f);
        Vector3 gridsize = new Vector3(gridCellWidth, gridCellHeight, 0f);
        Vector3 cellPos = Vector3.zero;
        for(int i = 0; i < gridRows; i++) {
            for(int j = 0; j < gridColumns; j++) {
                cellPos = offset + Vector3.right * gridCellWidth * i + Vector3.up * -gridCellHeight * j;
                Gizmos.DrawWireCube(cellPos, gridsize);
            }
        }
    }
}
