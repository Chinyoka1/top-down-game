using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    private List<Animator> animators;

    private void OnEnable()
    {
        animators = GetComponent<PlayerMovement>().animators;
        inputReader.attackAction.performed += Attack;
        inputReader.scytheAction.performed += Scythe;
    }

    private void OnDisable()
    {
        inputReader.attackAction.performed -= Attack;
        inputReader.scytheAction.performed -= Scythe;
    }
    
    private void Attack(InputAction.CallbackContext context)
    {
        foreach (Animator animator in animators)
        {
            if (animator.gameObject.activeInHierarchy)
            {
                animator.SetTrigger(AnimatorStrings.attack);
            }
        }
    }
    
    private void Scythe(InputAction.CallbackContext context)
    {
        foreach (Animator animator in animators)
        {
            if (animator.gameObject.activeInHierarchy)
            {
                animator.SetTrigger(AnimatorStrings.scythe);
            }
        }
    }
}
