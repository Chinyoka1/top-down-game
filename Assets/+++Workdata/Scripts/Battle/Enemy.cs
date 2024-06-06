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
        transform.position = Vector3.MoveTowards(transform.position, soundTarget.transform.position, walkSpeed * Time.deltaTime);
        // set the enemy's movement direction in the animator
        anim.SetFloat(AnimatorStrings.directionX, soundTarget.transform.position.x - transform.position.x);
        anim.SetFloat(AnimatorStrings.directionY, soundTarget.transform.position.y - transform.position.y);
    }

    private void AttackViewTarget()
    {
        viewTarget = viewDetectionZone.detectedCollider;
        anim.SetBool(AnimatorStrings.hasTarget, true);
    }
}