using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    public int[,] Grid { get; private set; }
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

    // Update is called once per frame
    void Update() {
        
    }

    void StartGrid() {
        Grid = new int[gridRows,gridColumns];
    }

    public Vector3 GetGridPosition(float xPos, float yPos) {
        // if xPos and yPos inside grid dimensions
        // Debug.Log($"X and Y Pos are: ({xPos}, {yPos})");
        if (xPos >= transform.position.x && yPos <= transform.position.y &&
        xPos <= transform.position.x + gridRows * gridCellWidth &&
        yPos >= transform.position.y + gridColumns * -gridCellHeight) {
            xPos = (int)((transform.position.x - xPos) / gridCellWidth);
            yPos = (int)((transform.position.y - yPos) / gridCellHeight);
            // Debug.Log($"Inside Grid with X, Y: ({-xPos}, {-yPos})");
            xPos = transform.position.x + xPos * -GridOffset.x + GridOffset.x/2;
            yPos = transform.position.y + yPos * -GridOffset.y - GridOffset.y/2;
            return new Vector3(xPos, yPos, 0f);
        } else return Vector3.zero;
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
