using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static int inventoryLimit = 60;
    public static int itemAmountLimit = 5;
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

    private void RefreshInventory()
    {
        List<State> states = gameState.GetStates();
        int a = 0;
        foreach (InventorySlot inventorySlot in inventorySlots)
        {
            if (a < states.Count)
            {
                StateInfo stateInfo = gameState.GetStateInfo(states[a].id);
                stateInfo.amount = states[a].amount;
                inventorySlot.SetStateInfo(stateInfo);
                a++;
            }
            else
            {
                inventorySlot.Deactivate();
            }
        }
    }

    private void ToggleInventory(InputAction.CallbackContext context)
    {
        if (inventoryContainer.activeInHierarchy)
        {
            ClearInventory();
        }
        else
        {
            RefreshInventoryStackLimit();
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
    
    private void RefreshInventoryStackLimit()
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
                        rest = inventorySlots[i+j].SetStateInfoAndReturnRest(stateInfo);
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
    
    private void RefreshInventoryAmountLimit()
    {
        List<State> states = gameState.GetStates();
        int overflowSlots = 0;
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            // Check if this slot has already been filled with an item in the previous iteration,
            // skip if it has
            if (i > 0 && inventorySlots[i].HasItem()) continue;
            
            int a = i - overflowSlots;
            if (a < states.Count)
            {
                StateInfo stateInfo = gameState.GetStateInfo(states[a].id);
                // Check if the items amount is greater than the limit, if it is, fill
                // one slot with the item and its amount limit, and fill the next slot with
                // the rest.
                
                //print(states[a].id + ": " + states[a].amount);
                if (states[a].amount > itemAmountLimit)
                {
                    int fullSlots = states[a].amount / itemAmountLimit;
                    int itemRemainder = states[a].amount % itemAmountLimit;
                    
                    stateInfo.amount = itemAmountLimit;
                    // set all full slots
                    for (int j = 0; j < fullSlots; j++)
                    {
                        inventorySlots[i + j].SetStateInfo(stateInfo);
                        overflowSlots++;
                    }

                    // set the slot that isn't full
                    if (itemRemainder > 0)
                    {
                        StateInfo overflow = new StateInfo
                        {
                            id = stateInfo.id,
                            name = stateInfo.name,
                            icon = stateInfo.icon,
                            description = stateInfo.description,
                            amount = itemRemainder
                        };
                        inventorySlots[i + fullSlots].SetStateInfo(overflow);
                    }
                }
                else
                {
                    stateInfo.amount = states[a].amount;
                    inventorySlots[i].SetStateInfo(stateInfo);
                }
            }
            else
            {
                inventorySlots[i].Deactivate();
            }
        }
    }
}