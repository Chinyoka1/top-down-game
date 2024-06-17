using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairMovement : MonoBehaviour
{
    public enum StairDirection { Left, Right }
    public enum StairMoveDirection { Up, Down }

    public StairDirection stairDirect = StairDirection.Right;
    public StairMoveDirection moveDirection = StairMoveDirection.Up;
    public float speed = 4f;
    public bool autoMove;
    public float stairSlope = 0.9f;

    [SerializeField]
    private bool isOnStairs;
    private Transform player;
    private float yIntercept;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject.transform;
        playerMovement = player.gameObject.GetComponent<PlayerMovement>();
        if ((stairDirect == StairDirection.Right && moveDirection == StairMoveDirection.Up) ||
            (stairDirect == StairDirection.Left && moveDirection == StairMoveDirection.Down))
        {
            stairSlope *= -1;
        }
    }

    private void FixedUpdate()
    {
        if (autoMove)
        {
            if (isOnStairs)
            {
                MovePlayer();
            }
        }
        else
        {
            playerMovement.isOnStairs = isOnStairs;
            playerMovement.stairSlope = stairSlope;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isOnStairs = true;
            if (autoMove)
            {
                player.gameObject.GetComponent<PlayerMovement>().DisableInput();
                yIntercept = player.position.y - ( -stairSlope * player.position.x);
                UpdatePlayerAnimators();
            }
            else
            {
                playerMovement.isOnStairs = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isOnStairs = false;
            if (autoMove)
            {
                playerMovement.EnableInput();
            }
            else
            {
                playerMovement.isOnStairs = false;
            }
        }
    }

    private void MovePlayer()
    {
        Vector3 newPosition = player.position;

        float horizontalMovement = Time.deltaTime * speed * (stairDirect == StairDirection.Right ? 1 : -1);
        newPosition.x += horizontalMovement;
        newPosition.y = CalculateYPosition(newPosition.x);
        player.position = newPosition;
    }

    private float CalculateYPosition(float x)
    {
        return -stairSlope * x + yIntercept; // Funktion f(x) = -0.75x + 1.75
    }

    private void UpdatePlayerAnimators()
    {
        foreach (Animator anim in playerMovement.animators)
        {
            if (anim.gameObject.activeInHierarchy)
            {
                anim.SetFloat(AnimatorStrings.directionX, stairDirect == StairDirection.Right ? 1 : -1);
            }
        }
    }
}
