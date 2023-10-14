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

    public GameObject hexCellPrefab;
    public float hexSize;

    public GameObject Item1;

    public bool isHex;
    
    // Start is called before the first frame update
    void Start() {
        if (!isHex) CreateGrid();
        else CreateHexGrid();
    }

    // Update is called once per frame
    void Update() {
    }

    //creates grid on startup
    private void CreateGrid() {

        gameGrid = new GameObject[height, width];

        if (gridCellPrefab == null) {
            Debug.LogError("ERROR : Grid Cell prefab not assigned to Game Grid");
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

/*        //create trees on map
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
        }*/
    }


    private void CreateHexGrid()
    {
        gameGrid = new GameObject[height, width];

        if (hexCellPrefab == null)
        {
            Debug.LogError("ERROR : Grid Cell prefab not assigned to Game Grid");
            return;
        }

        // Variables pour déterminer la taille et l'espacement des cellules hexagonales
        float hexWidth = Mathf.Sqrt(3) * hexSize;
        float xOffset = hexWidth * 0.75f;
        float zOffset = hexWidth * 0.65f;

        // Crée la grille d'hexagones
        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                float xPos = x * xOffset;
                // Pour décaler chaque ligne impaire
                if (z % 2 == 1)
                {
                    xPos += xOffset * 0.5f;
                }

                // Crée une nouvelle cellule hexagonale pour chaque cellule de la grille
                gameGrid[x, z] = Instantiate(hexCellPrefab,
                    new Vector3(xPos, 0, z * zOffset), Quaternion.Euler(0, 90, 0));

                // Configure les valeurs de chaque cellule hexagonale
                gameGrid[x, z].GetComponent<GridCell>().SetPosition(x, z);
                gameGrid[x, z].transform.parent = transform;
                gameGrid[x, z].gameObject.name = "Grid Space(X: " + x.ToString() + " , Z: " + z.ToString() + ")";
            }
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