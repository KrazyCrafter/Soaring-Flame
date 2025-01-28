using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMover : MonoBehaviour
{
    private float moveX, moveY;
    public float baseSpeed;

    private Transform cameraTransform;

    private Rigidbody cameraRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = GetComponent<Transform>();
        cameraRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Movement();
    }

    void OnMove(InputValue moveValue) 
    {
        UnityEngine.Vector2 moveVector = moveValue.Get<UnityEngine.Vector2>();

        moveX = moveVector.x;
        moveY = moveVector.y;
    }

    private void Movement()
    {
        UnityEngine.Vector3 movement = cameraTransform.forward * moveY + cameraTransform.right * moveX;

        cameraRigidbody.AddForce(baseSpeed * movement, ForceMode.Force);
    }
}
