using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    public GameObject ItemPanel;
    public Image iconImage;
    public TextMeshProUGUI itemHeaderText;
    public TextMeshProUGUI itemDescriptionText;
    
    [SerializeField]
    private StateInfo[] stateInfos;
    
    private void OnEnable()
    {
        GameState.StateAdded += OnStateAdded;
        ItemAddedPopup.Confirmed += OnPopupConfirmed;
    }

    private void OnDisable()
    {
        GameState.StateAdded -= OnStateAdded;
        ItemAddedPopup.Confirmed -= OnPopupConfirmed;
    }
    
    private void OnStateAdded(string id, int amount)
    {
        foreach (StateInfo stateInfo in stateInfos)
        {
            if (stateInfo.id == id)
            {
                ItemPanel.SetActive(true);
                iconImage.sprite = stateInfo.icon;
                itemHeaderText.text = stateInfo.name;
                itemDescriptionText.text = stateInfo.description;
            }
        }
    }

    private void OnPopupConfirmed(ItemAddedPopup popup)
    {
        EventSystem.current.SetSelectedGameObject(null);
        popup.gameObject.SetActive(false);
    }
}
