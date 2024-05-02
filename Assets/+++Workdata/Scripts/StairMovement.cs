using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairMovement : MonoBehaviour
{
    public enum StairDirection { Left, Right }
    public enum MoveDirection { Up, Down }

    public StairDirection stairDirect = StairDirection.Right;
    public MoveDirection moveDirection = MoveDirection.Up;
    public float speed = 4f;

    private bool isOnStairs;
    private Transform player;
    private float yIntercept;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject.transform;
    }

    private void Update()
    {
        if (isOnStairs)
        {
            MovePlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.gameObject.GetComponent<PlayerMovement>().DisableInput();
            //yIntercept = player.position.y - ((moveDirection == MoveDirection.Down ? -0.75f : 0.75f) * player.position.x);
            yIntercept = player.position.y - ( -0.75f * player.position.x);
            isOnStairs = true;
            UpdatePlayerAnimators();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.gameObject.GetComponent<PlayerMovement>().EnableInput();
            isOnStairs = false;
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
        //return (moveDirection == MoveDirection.Down ? -0.75f : 0.75f) * x + yIntercept; // Funktion f(x) = -0.75x + 1.75
        return -0.75f * x + yIntercept; // Funktion f(x) = -0.75x + 1.75
    }

    private void UpdatePlayerAnimators()
    {
        List<Animator> playerAnimators = player.gameObject.GetComponent<PlayerMovement>().animators;
        foreach (Animator anim in playerAnimators)
        {
            if (anim.gameObject.activeInHierarchy)
            {
                anim.SetFloat(AnimatorStrings.directionX, stairDirect == StairDirection.Right ? 1 : -1);
            }
        }
    }
}
