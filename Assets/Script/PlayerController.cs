using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody playerRigidbody;
    private Vector3 moveDirection;

    public int playerNumber;

    public bool backToStart;

    public static PlayerController singleton;

    public bool timeOut;
    void Start()
    {
        singleton = this;
        if (playerRigidbody == null)
        {
            playerRigidbody = GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        if (playerNumber == 1)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            moveDirection = new Vector3(moveHorizontal, 0f, moveVertical).normalized;

        }
        else
        {
            float moveHorizontal = Input.GetAxis("Horizontal2");
            float moveVertical = Input.GetAxis("Vertical2");
            moveDirection = new Vector3(moveHorizontal, 0f, moveVertical).normalized;

        }
    }

    void FixedUpdate()
    {
        playerRigidbody.MovePosition(playerRigidbody.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "cubeRed" && playerNumber == 1 && backToStart == false)
        {
            backToStart = true;
        }
        else if (other.gameObject.tag == "cubeBlue" && playerNumber == 2 && backToStart == false)
        {
            backToStart = true;
        }
        if (other.gameObject.tag == "startRed" && playerNumber == 1 && backToStart == true)
        {
            backToStart = false;
            CircleManager.singleton.GenerateNewCircle("CubeRed(Clone)",1,timeOut);
        }
        else if(other.gameObject.tag == "startBlue" && playerNumber == 2 && backToStart == true)
        {
            backToStart = false;
            CircleManager.singleton.GenerateNewCircle("CubeBlue(Clone)", 2, timeOut);

        }
    }
}