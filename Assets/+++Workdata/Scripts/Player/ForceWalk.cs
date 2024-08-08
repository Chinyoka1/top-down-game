using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceWalk : MonoBehaviour
{
    public float duration = 1f;
    public enum Direction { Up, Down, Left, Right }

    public Direction direction = Direction.Down;
    
    private float walkSpeed = 4f;
    private Transform player;
    private PlayerMovement playerMovement;
    
    private void Update()
    {
        if(duration < 1f)
        {
            duration += Time.deltaTime;
            ForceWalkDown();
        }
    }

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject.transform;
        playerMovement = player.gameObject.GetComponent<PlayerMovement>();
    }

    public void StartForceWalk()
    {
        StartCoroutine(ForceWalkTimer());
        duration = 0f;
        playerMovement.DisableInput();
        UpdatePlayerAnimators();
    }

    private void ForceWalkDown()
    {
        Vector3 newPosition = player.position;

        switch (direction)
        {
            case Direction.Down:
                newPosition.y += Time.deltaTime * walkSpeed * -1;
                break;
            case Direction.Up:
                newPosition.y += Time.deltaTime * walkSpeed;
                break;
            case Direction.Left:
                newPosition.x += Time.deltaTime * walkSpeed * -1;
                break;
            case Direction.Right:
                newPosition.x += Time.deltaTime * walkSpeed;
                break;
        }

        player.position = new Vector3(newPosition.x, newPosition.y);
    }
    
    private void UpdatePlayerAnimators()
    {
        foreach (Animator anim in playerMovement.animators)
        {
            if (anim.gameObject.activeInHierarchy)
            {
                switch (direction)
                {
                    case Direction.Down:
                        anim.SetFloat(AnimatorStrings.directionY, -1);
                        break;
                    case Direction.Up:
                        anim.SetFloat(AnimatorStrings.directionY, 1);
                        break;
                    case Direction.Left:
                        anim.SetFloat(AnimatorStrings.directionX, -1);
                        break;
                    case Direction.Right:
                        anim.SetFloat(AnimatorStrings.directionX, 1);
                        break;
                }
            }
        }
    }
    
    IEnumerator ForceWalkTimer()
    {
        yield return new WaitForSeconds(1f);
        playerMovement.EnableInput();
    }
}
