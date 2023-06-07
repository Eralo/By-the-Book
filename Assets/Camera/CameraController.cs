using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    //POSITION
    
    public Vector3 newPosition;    
    public float movementSpeed = 1;    
    public float movementTime = 5;
    
    
    //ROTATION

    public float rotationAmount = 90;
    public Quaternion newRotation;

    public float rotationLag = 1;
    private float rotationTimer;
    
    //ZOOM

    public Transform cameraTransform;
    public Vector3 zoomAmount = new Vector3(0, -10, 10);
    public Vector3 newZoom;
    
    public float zoomLag = .2f;
    private float zoomTimer;
    
    
    //MOUSE CONTROLS

    public Vector3 dragStartPosition;
    public Vector3 dragCurrentPosition;

    public Vector3 rotateStartPosition;
    public Vector3 rotateCurrentPosition;

    
    // Start is called before the first frame update
    void Start() {
        newPosition = transform.localPosition;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }

    // Update is called once per frame
    void Update() {
        if (rotationTimer > 0) rotationTimer -= Time.deltaTime;
        if (zoomTimer > 0) zoomTimer -= Time.deltaTime;
        HandleMovementInput();
        HandleMouseInput();
    }

    void HandleMovementInput() {
        
        //MOVEMENT

        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow)) {
            newPosition += (transform.forward * movementSpeed);
        }
        
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            newPosition -= (transform.forward * movementSpeed);
        }    
        
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            newPosition += (transform.right * movementSpeed);
        }  
        
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.RightArrow)) {
            newPosition -= (transform.right * movementSpeed);
        }

        
        //ROTATION
        
        if (Input.GetKey(KeyCode.A) && rotationTimer<=0) {
            newRotation *=Quaternion.Euler(Vector3.up*rotationAmount);
            rotationTimer = rotationLag;
        }
        
        if (Input.GetKey(KeyCode.E) && rotationTimer<=0) {
            newRotation *=Quaternion.Euler(Vector3.up*-rotationAmount);
            rotationTimer = rotationLag;
        }
        
        //ZOOM
        
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
            newZoom += zoomAmount;
        }        
        if (Input.GetKey(KeyCode.R) && zoomTimer <=0) {
            newZoom += zoomAmount;
            zoomTimer = zoomLag;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
            newZoom -= zoomAmount;
        }
        if (Input.GetKey(KeyCode.F) && zoomTimer <=0) {
            newZoom -= zoomAmount;
            zoomTimer = zoomLag;
        }
        
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        cameraTransform.localPosition =
            Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }
    
    //MOUSE 

    void HandleMouseInput() {

        //TILE SELECTER
        if (Input.GetMouseButtonDown(1)) {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry)) {
                dragStartPosition = ray.GetPoint(entry);
            }
        }
        if (Input.GetMouseButton(1)) {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry)) {
                dragCurrentPosition = ray.GetPoint(entry);

                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }

        //FREE ROTATION
        if (Input.GetMouseButtonDown(2)) {
            rotateStartPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(2)) {
            rotateCurrentPosition = Input.mousePosition;

            Vector3 difference = rotateStartPosition - rotateCurrentPosition;

            rotateStartPosition = rotateCurrentPosition;
            
            newRotation*= Quaternion.Euler(Vector3.up * (-difference.x / 5f));
        }
    }
}
