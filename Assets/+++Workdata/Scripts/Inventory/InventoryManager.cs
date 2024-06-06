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
        int overflowSlots = 0;
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            // Check if there's already an item in this slot, skip if there is
            if (i > 0 && inventorySlots[i].HasItem()) continue;
            int a = i - overflowSlots;
            if (a < states.Count)
            {
                StateInfo stateInfo = gameState.GetStateInfo(states[a].id);
                // Check if the items amount is greater than the limit, if it is, fill
                // one slot with the item and its amount limit, and fill the next slot with
                // the rest.
                print(states[a].id + ": " + states[a].amount);
                if (states[a].amount > itemAmountLimit)
                {
                    int fullSlots = states[a].amount / itemAmountLimit;
                    int itemRemainder = states[a].amount % itemAmountLimit;
                    stateInfo.amount = itemAmountLimit;
                    // set all full slots
                    for (int j = 0; j < fullSlots; j++)
                    {
                        inventorySlots[i+j].SetStateInfo(stateInfo);
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
                        inventorySlots[i+fullSlots].SetStateInfo(overflow);
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

    private void ToggleInventory(InputAction.CallbackContext context)
    {
        RefreshInventory();
        inventoryContainer.SetActive(!inventoryContainer.activeInHierarchy);
    }
}