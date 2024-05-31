using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]private InventorySlot[] inventorySlots;
    [SerializeField]private GameState gameState;

    private void Awake()
    {
        RefreshInventory();
    }

    public void RefreshInventory()
    {
        List<State> states = gameState.GetStates();
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < states.Count)
            {
                StateInfo stateInfo = gameState.GetStateInfo(i);
                stateInfo.amount = states[i].amount;
                
                inventorySlots[i].SetStateInfo(stateInfo);
            }
            else
            {
                inventorySlots[i].Deactivate();
            }
        }
    }
}
