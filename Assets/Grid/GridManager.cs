using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class GridManager : MonoBehaviour {

    public int height = 10;
    public int width = 10;
    public float GridSpaceSize = 40f;

    public GameObject[,] gameGrid;
    public GameObject gridCellPrefab;

    public GameObject Item1;
    
    // Start is called before the first frame update
    void Start() {
        CreateGrid();
    }

    // Update is called once per frame
    void Update() {
    }

    //creates grid on startup
    private void CreateGrid() {

        gameGrid = new GameObject[height, width];

        if (gridCellPrefab == null) {
            Debug.LogError("ERROR : Grid Cell not assigned to Game Grid");
        }

        //make grid
        for (int z = 0; z < height; z++) {
            for (int x = 0; x < width; x++) {

                //create new GridSpace for each cell
                gameGrid[x, z] = Instantiate(gridCellPrefab,
                    new Vector3(x * GridSpaceSize, 0, z * GridSpaceSize), Quaternion.identity);
                //sets each Gridcell it's own values
                gameGrid[x, z].GetComponent<GridCell>().SetPosition(x, z);
                gameGrid[x, z].transform.parent = transform;
                gameGrid[x, z].gameObject.name = "Grid Space(X: " + x.ToString() + " , Z: " + z.ToString() + ")";
                
            }
        }

        //create trees on map
        for (int i = 0; i < 10; i++) {

            int posX = UnityEngine.Random.Range(0,width);
            int posZ = UnityEngine.Random.Range(0,height);

            //find parent object
            GameObject parentTile = gameGrid[posX, posZ];

            //instantiate at random coordinates and scale the tree
            GameObject tree = Instantiate(Item1,
                new Vector3(posX * GridSpaceSize, GridSpaceSize, posZ * GridSpaceSize), Quaternion.identity);
                
            tree.transform.localScale = new Vector3(GridSpaceSize, GridSpaceSize, GridSpaceSize);

            //set parent
            tree.transform.SetParent(parentTile.transform);

            //set name
            tree.name = "Tree_A";
        }
    }

    //get grid position from world
        public Vector2Int GetGridPosFromWorld(Vector3 worldPosition) {
            int x = Mathf.FloorToInt(worldPosition.x / GridSpaceSize);
            int z = Mathf.FloorToInt(worldPosition.z / GridSpaceSize);

            x = Mathf.Clamp(x, 0, width);
            z = Mathf.Clamp(x, 0, height);

            return new Vector2Int(x, z);
        }
        
        //get the world position of a grid position

        public Vector3 GetWorldPosFromGridPos(Vector2 gridPos) {
            float x = gridPos.X * GridSpaceSize;
            float z = gridPos.Y * GridSpaceSize;

            return new Vector3(x, 0, z);
        }
}