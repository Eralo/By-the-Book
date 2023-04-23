using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour {


    public int posX;
    public int posZ;

    //saves reference to placed GameObject
    public GameObject objectInThisGridSpace = null;
    //saves if grid space is available 
    public bool isOccupied = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        //setter position
    public void SetPosition(int x, int z) {
        posX = x;
        posZ = z;
    }

    public Vector2 GetPosition() {
        return new Vector2(posX, posZ);
    }
}
