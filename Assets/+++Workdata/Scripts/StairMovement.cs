using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairMovement : MonoBehaviour
{
    public enum StairDirection { Left, Right }

    public StairDirection direction = StairDirection.Right;
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
            yIntercept = player.position.y - (-0.75f * player.position.x);
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

        float horizontalMovement = Time.deltaTime * speed * (direction == StairDirection.Right ? 1 : -1);
        newPosition.x += horizontalMovement;
        newPosition.y = CalculateYPosition(newPosition.x);
        player.position = newPosition;
    }

    private float CalculateYPosition(float x)
    {
        return -0.75f * x + yIntercept; // Funktion f(x) = -0.75x + 1.75
    }

    private void UpdatePlayerAnimators()
    {
        List<Animator> playerAnimators = player.gameObject.GetComponent<PlayerMovement>().animators;
        foreach (Animator anim in playerAnimators)
        {
            if (anim.gameObject.activeInHierarchy)
            {
                anim.SetFloat(AnimatorStrings.directionX, direction == StairDirection.Right ? 1 : -1);
            }
        }
    }
}
