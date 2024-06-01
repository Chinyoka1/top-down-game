using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
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
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < states.Count)
            {
                StateInfo stateInfo = gameState.GetStateInfo(states[i].id);
                stateInfo.amount = states[i].amount;
                
                inventorySlots[i].SetStateInfo(stateInfo);
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
