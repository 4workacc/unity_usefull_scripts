﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    //movement
    public float movementSpeed = 1f;
    public float movementTime = 1f;

    public Vector3 newPosition;
    //fast moving
    public float normalSpeed;
    public float fastSpeed;

    //rotation
    public float rotationAmount = 1f;
    public Quaternion newRotation;

    //rotation
    public Transform cameraTransform;
    public Vector3 zoomAmount = new Vector3(0f, -10f, 10f);
    public Vector3 newZoom;

    //mouse move
    public Vector3 dragStartPosition;
    public Vector3 dragCurrentPosition;

    //mouse rotate
    public Vector3 rotateStartPosition;
    public Vector3 rotateCurrentPosition; 

    // Start is called before the first frame update
    void Start()
    {
        rotationAmount = 1f;
        movementTime = 1f;
        movementSpeed = 1f;
        zoomAmount = new Vector3(0f, -2f, 2f);

        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        HandleKeyBoard();
        HandleMouse();
    }
    void HandleKeyBoard ()
    {
        //fast moving
        if ( Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = fastSpeed;
        }
        else
        {
            movementSpeed = normalSpeed;
        }
        //moving
        if (Input.GetKey(KeyCode.W) || ( Input.GetKey(KeyCode.UpArrow ))) {
            newPosition += (transform.forward * movementSpeed);
        }
        if ( Input.GetKey(KeyCode.S) || ( Input.GetKey(KeyCode.DownArrow))) {
            newPosition += (transform.forward * -  movementSpeed);
        }
        if (Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.RightArrow)))
        {
            newPosition += (transform.right * movementSpeed);
        }
        if (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.LeftArrow)))
        {
            newPosition += (transform.right * -movementSpeed);
        }

        if (Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.RightArrow)))
        {
            newPosition += (transform.right * movementSpeed);
        }
        if (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.LeftArrow)))
        {
            newPosition += (transform.right * -movementSpeed);
        }
        //rotation
        if (Input.GetKey(KeyCode.Q) )
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if ( Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);

        } 
        //zoom
        if ( Input.GetKey(KeyCode.R))
        {
            newZoom += zoomAmount;
        }
        if ( Input.GetKey(KeyCode.F))
        {
            newZoom -= zoomAmount;
        }

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }
    void HandleMouse()
    {
        //mouse cam drag
        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;
            if ( plane.Raycast(ray, out entry) )
            {
                dragStartPosition = ray.GetPoint(entry);
            }
        }
        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;
            if (plane.Raycast(ray, out entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);
                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }
        //mouse zoom
        if ( Input.mouseScrollDelta.y != 0 )
        {
            newZoom += Input.mouseScrollDelta.y * zoomAmount;
        }
        //mouse rotate
        if ( Input.GetMouseButtonDown(2))
        {
            rotateStartPosition = Input.mousePosition;
        }
        if ( Input.GetMouseButton(2))
        {
            rotateCurrentPosition = Input.mousePosition;
            Vector3 diff = rotateStartPosition - rotateCurrentPosition;
            rotateStartPosition = rotateCurrentPosition;
            newRotation *= Quaternion.Euler(Vector3.up * (-diff.x / 5f));
        }
    }
}
