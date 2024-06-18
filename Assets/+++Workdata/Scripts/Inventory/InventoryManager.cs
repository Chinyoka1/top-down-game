using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public int filledSlots;
    public int inventorySlotLimit = 60;
    
    [SerializeField] private GameObject inventoryContainer;
    [SerializeField] private InventorySlot[] inventorySlots;
    [SerializeField] private GameState gameState;
    [SerializeField] private InputReader inputReader;
    private GameController gameController;

    private void Awake()
    {
        RefreshInventory();
        gameController = FindObjectOfType<GameController>();
    }

    private void OnEnable()
    {
        inputReader.inventoryAction.performed += ToggleInventory;
    }

    private void OnDisable()
    {
        inputReader.inventoryAction.performed -= ToggleInventory;
    }

    private void ToggleInventory(InputAction.CallbackContext context)
    {
        RefreshInventory();
        inventoryContainer.SetActive(!inventoryContainer.activeInHierarchy);
    }

    private void ClearInventory()
    {
        foreach (InventorySlot inventorySlot in inventorySlots)
        {
            inventorySlot.Deactivate();
        }
        filledSlots = 0;
    }
    
    public void RefreshInventory()
    {
        ClearInventory();
        List<State> states = gameState.GetStates();
        int a = 0;
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            // Check if this slot has already been filled with an item in a previous iteration,
            // skip if it has
            if (inventorySlots[i].HasItem()) continue;
            
            // try to fill the slot if there is an item
            if (a < states.Count)
            {
                StateInfo stateInfo = gameState.GetStateInfo(states[a].id);
                stateInfo.amount = states[a].amount;
                // if the item amount is greater than the stack size, create full slot and reduce amount in every
                // iteration until there is no rest amount left
                if (stateInfo.amount > stateInfo.stackSize)
                {
                    int rest = stateInfo.amount - stateInfo.stackSize;
                    for (int j = 0; rest > 0; j++)
                    {
                        // create slot with full stack
                        inventorySlots[i+j].SetStateInfo(stateInfo);
                        filledSlots++;
                        // assign rest to amount
                        if (stateInfo.amount > stateInfo.stackSize)
                        {
                            rest = stateInfo.amount - stateInfo.stackSize;
                        }
                        else
                        {
                            rest = 0;
                        }
                        stateInfo.amount = rest;
                    }
                }
                else
                {
                    inventorySlots[i].SetStateInfo(stateInfo);
                    filledSlots++;
                }

                a++;
            }
            else
            {
                inventorySlots[i].Deactivate();
            }
        }
    }
}