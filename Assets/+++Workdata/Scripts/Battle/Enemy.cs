using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public DetectionZone soundDetectionZone;
    public DetectionZone viewDetectionZone;
    public float walkSpeed = 2f;

    private Collider2D soundTarget;
    private Collider2D viewTarget;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (soundDetectionZone.detectedCollider)
        {
            FollowSoundTarget();
            if (viewDetectionZone.detectedCollider)
            {
                AttackViewTarget();
            }
            else
            {
                anim.SetBool(AnimatorStrings.hasTarget, false);
            }
        }
        else
        {
            anim.SetFloat(AnimatorStrings.directionY, 0);
            anim.SetFloat(AnimatorStrings.directionX, 0);
        }
    }

    private void FollowSoundTarget()
    {
        soundTarget = soundDetectionZone.detectedCollider;
        // make the enemy follow the player (soundTarget)
        transform.position =
            Vector3.MoveTowards(transform.position, soundTarget.transform.position, walkSpeed * Time.deltaTime);
        // set the enemy's movement direction in the animator
        float dirX = soundTarget.transform.position.x - transform.position.x;
        float dirY = soundTarget.transform.position.y - transform.position.y;
        anim.SetFloat(AnimatorStrings.directionX, dirX);
        anim.SetFloat(AnimatorStrings.directionY, dirY);
        RotateX(dirX, dirY);
    }

    private void RotateX(float dirX, float dirY)
    {
        // check which direction enemy is faced and set the viewDetectionZone rotation
        if (dirX > 0)
        {
            // if dirY is stronger than dirX, use dirY instead
            if (dirY > dirX || dirY * -1 > dirX)
            {
                RotateY(dirY);
            }
            else
            {
                viewDetectionZone.transform.eulerAngles = Vector3.forward * 90; // right
            }
        }
        else if (dirX < 0)
        {
            // if dirY is stronger than dirX, use dirY instead
            if (dirY < dirX || dirY * -1 < dirX)
            {
                RotateY(dirY);
            }
            else
            {
                viewDetectionZone.transform.eulerAngles = Vector3.forward * -90; // left
            }
        }
    }

    private void RotateY(float dirY)
    {
        if (dirY > 0)
        {
            viewDetectionZone.transform.eulerAngles = Vector3.forward * 180; // up
        }
        else if (dirY < 0)
        {
            viewDetectionZone.transform.eulerAngles = Vector3.forward * 0; // down
        }
    }

    private void AttackViewTarget()
    {
        viewTarget = viewDetectionZone.detectedCollider;
        anim.SetBool(AnimatorStrings.hasTarget, true);
    }
}