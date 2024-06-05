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
    [SerializeField]private GameObject inventoryContainer;
    [SerializeField]private InventorySlot[] inventorySlots;
    [SerializeField]private GameState gameState;
    [SerializeField]private InputReader inputReader;

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
            // Check if there's already an item in this slot (amount limit), skip if there is
            

            int a = i - overflowSlots;
            if (a < states.Count -1)
            {
                StateInfo stateInfo = gameState.GetStateInfo(states[a].id);
                // Check if the items amount is greater than the limit, if it is, fill
                // one slot with the item and its amount limit, and fill the next slot with
                // the rest.
                if (states[a].amount > itemAmountLimit)
                {
                    stateInfo.amount = itemAmountLimit;
                    inventorySlots[i].SetStateInfo(stateInfo);
                    overflowSlots++;
                    stateInfo.amount = states[a].amount - itemAmountLimit;
                    inventorySlots[i+1].SetStateInfo(stateInfo);
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
