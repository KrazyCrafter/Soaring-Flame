using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerMover : MonoBehaviour
{
    private float moveX, moveY;
    public float baseSpeed;

    private float rotateY;
    public float sensitivity;
    private float yRotation;

    private UnityEngine.Vector3 playerPosOrigin;
    public PlayerPosBounderies playerBounderies;
    private Transform playerTransform;
    private Rigidbody playerRigidbody;

    public TextMeshProUGUI CoinCounter;
    public TextMeshProUGUI PopCounter;
    public Transform HealthBar;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GetComponent<Transform>();
        playerPosOrigin = playerTransform.position;
        playerBounderies = new PlayerPosBounderies(20f, 12f);
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Movement();
        Rotation();
        CheckPlayerPosition();
        CoinCounter.text = "$:" + V.Coins;
        PopCounter.text = "Pops:" + V.Pops;
        HealthBar.localScale = new UnityEngine.Vector3 (V.BaseHealth / 200f,1,1);
    }

    private void CheckPlayerPosition()
    {
        if(Math.Abs(playerTransform.position.x - playerPosOrigin.x) > playerBounderies.Position.x)
        {
            if(playerTransform.position.x > playerPosOrigin.x)
            {
                // Debug.Log("x -> +\n" + playerTransform.position);
                playerTransform.position = new UnityEngine.Vector3(playerPosOrigin.x + playerBounderies.Position.x, playerTransform.position.y, playerTransform.position.z);
            }
            else 
            {
                // Debug.Log("x -> -\n" + playerTransform.position);
                playerTransform.position = new UnityEngine.Vector3(playerPosOrigin.x - playerBounderies.Position.x, playerTransform.position.y, playerTransform.position.z);
            }
        }

        if(Math.Abs(playerTransform.position.z - playerPosOrigin.z) > playerBounderies.Position.z)
        {
            if(playerTransform.position.z > playerPosOrigin.z)
            {
                // Debug.Log("z -> +\n" + playerTransform.position);
                playerTransform.position = new UnityEngine.Vector3(playerTransform.position.x, playerTransform.position.y, playerPosOrigin.z + playerBounderies.Position.z);
            }
            else 
            {
                // Debug.Log("z -> -\n" + playerTransform.position);
                playerTransform.position = new UnityEngine.Vector3(playerTransform.position.x, playerTransform.position.y, playerPosOrigin.z - playerBounderies.Position.z);
            }
        }
    }

    void OnMove(InputValue moveValue) 
    {
        UnityEngine.Vector2 moveVector = moveValue.Get<UnityEngine.Vector2>();

        moveX = moveVector.x;
        moveY = moveVector.y;
    }

    void OnRotate(InputValue rotateValue)
    {
       UnityEngine.Vector2 rotateVector = rotateValue.Get<UnityEngine.Vector2>();

        rotateY = rotateVector.y * sensitivity;
    }

    private void Movement()
    {
        UnityEngine.Vector3 movement = playerTransform.forward * moveY + playerTransform.right * moveX;

        playerRigidbody.AddForce(baseSpeed * movement, ForceMode.Force);
    }

    private void Rotation()
    {
        yRotation += rotateY;

        playerTransform.rotation = UnityEngine.Quaternion.Euler(0.0f, yRotation, 0.0f);
    }
}
public class PlayerPosBounderies 
{
    private UnityEngine.Vector3 position;
    public PlayerPosBounderies(float x, float z)
    {
        position = new UnityEngine.Vector3(x, 0.0f, z);        
    }

    public UnityEngine.Vector3 Position => position;
}