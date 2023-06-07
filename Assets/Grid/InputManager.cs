using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{

    public LayerMask layer;
    public GridCell targetCell;
    public GameObject selector;
    public float lag = 2f;
    public int height = 60;
    public Vector3 velocity = Vector3.forward;
    
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
       if (Input.GetMouseButtonDown(0)) {
           GridCell newTarget = isMouseOverCell();
           if (newTarget != null) {
               changeTarget(newTarget);
           }
       }
            //movement of selector to target (lerp is too slow)
       if (targetCell) {
           selector.transform.position = Vector3.SmoothDamp(selector.transform.position, targetCell.transform.position + 
               Vector3.up*height, ref velocity , Time.deltaTime * lag);
       }
    }
    
    //returns grid cell if mouse is over

    public GridCell isMouseOverCell() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if ( !IsPointerOverUIObject() && Physics.Raycast(ray, out RaycastHit hit, 10000f, layer)) {
            return hit.transform.GetComponent<GridCell>();
        }
        else {
            return null;
        }
    }

    private void changeTarget(GridCell newTarget) {
        if (targetCell) targetCell.GetComponent<MeshRenderer>().material.color = Color.white;
        targetCell = newTarget;
        targetCell.GetComponent<MeshRenderer>().material.color = Color.blue;
    }

     public static bool IsPointerOverUIObject() {
     PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
     eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
     List<RaycastResult> results = new List<RaycastResult>();
     EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
     return results.Count > 0;
 }
}
