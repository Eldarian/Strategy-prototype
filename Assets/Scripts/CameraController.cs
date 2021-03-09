using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    public Transform followTransform;

    Vector3 newPosition;
    [SerializeField] float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(followTransform != null)
        {
            transform.position = followTransform.position;
        }
        HandleKeyboardInput();

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            followTransform = null;
        }
    }

    private void HandleKeyboardInput()
    {
        var horizontalnput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        newPosition = transform.position += (transform.right * horizontalnput + transform.forward * verticalInput) * speed;

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * speed);
        
    }
}
