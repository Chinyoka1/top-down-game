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
        inputReader.toolAction.performed += UseTool;
    }

    private void OnDisable()
    {
        inputReader.attackAction.performed -= Attack;
        inputReader.toolAction.performed -= UseTool;
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
    
    private void UseTool(InputAction.CallbackContext context)
    {
        foreach (Animator animator in animators)
        {
            if (animator.gameObject.activeInHierarchy)
            {
                animator.SetTrigger(AnimatorStrings.useTool);
            }
        }
    }
}
