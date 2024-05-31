using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemPopupManager : MonoBehaviour
{
    public Transform itemPopupsContainer;
    public GameObject popupPrefab;
    public GameState gameState;

    private void Awake()
    {
        ClearList();
    }

    private void OnEnable()
    {
        GameState.StateAdded += OnStateAdded;
    }

    private void OnDisable()
    {
        GameState.StateAdded -= OnStateAdded;
    }
    
    private void OnStateAdded(string id, int amount)
    {
        foreach (StateInfo stateInfo in gameState.stateInfos)
        {
            if (stateInfo.id == id)
            {
                StartCoroutine(CreateItemPopup(stateInfo, amount));
            }
        }
    }

    private void ClearList()
    {
        foreach (Transform child in itemPopupsContainer)
        {
            Destroy(child.gameObject);
        }
    }

    IEnumerator CreateItemPopup(StateInfo stateInfo, int amount)
    {
        ItemPopup itemPopup = Instantiate(popupPrefab, itemPopupsContainer).GetComponent<ItemPopup>();
        stateInfo.amount = amount;
        itemPopup.SetStateInfo(stateInfo);
        yield return new WaitForSeconds(2);
        Destroy(itemPopup.gameObject);
    }
}
