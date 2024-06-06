using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public int slotLimit = 60;
    [SerializeField] private GameObject inventoryContainer;
    [SerializeField] private InventorySlot[] inventorySlots;
    [SerializeField] private GameState gameState;
    [SerializeField] private InputReader inputReader;

    private void Awake()
    {
        RefreshInventory();
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
        if (inventoryContainer.activeInHierarchy)
        {
            ClearInventory();
        }
        else
        {
            RefreshInventory();
        }
        inventoryContainer.SetActive(!inventoryContainer.activeInHierarchy);
    }

    private void ClearInventory()
    {
        foreach (InventorySlot inventorySlot in inventorySlots)
        {
            inventorySlot.Deactivate();
        }
    }
    
    private void RefreshInventory()
    {
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
                        // create slot with full stack, return rest and assign it as new amount
                        inventorySlots[i+j].SetStateInfo(stateInfo);
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