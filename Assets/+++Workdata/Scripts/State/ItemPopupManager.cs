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
    
    [SerializeField]
    private StateInfo[] stateInfos;

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
        foreach (StateInfo stateInfo in stateInfos)
        {
            if (stateInfo.id == id)
            {
                GameObject itemPopup = Instantiate(popupPrefab, itemPopupsContainer);
                TextMeshProUGUI itemText = itemPopup.transform.Find("Text").GetComponent<TextMeshProUGUI>();
                Image iconImage = itemPopup.transform.Find("Icon").GetComponent<Image>();

                itemText.text = "+" + stateInfo.amount + " " + stateInfo.name;
                iconImage.sprite = stateInfo.icon;
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
}
