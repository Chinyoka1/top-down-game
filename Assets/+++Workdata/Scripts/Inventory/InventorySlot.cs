using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private StateInfo _stateInfo;
    [SerializeField]private Image itemImage;
    [SerializeField]private TextMeshProUGUI itemAmount;
    [SerializeField]private Toggle slotToggle;
    [SerializeField]private ItemInfo itemInfo;

    private void OnEnable()
    {
        slotToggle.onValueChanged.AddListener(delegate {
            HandleSlotToggle();
        });
    }
    
    private void SetSlot()
    {
        itemImage.sprite = _stateInfo.icon;
        itemAmount.text = _stateInfo.amount.ToString();
        itemImage.gameObject.SetActive(true);
        itemAmount.gameObject.SetActive(true);
        slotToggle.interactable = true;
    }
    
    private int SetSlotAndReturnRest()
    {
        itemImage.sprite = _stateInfo.icon;
        itemImage.gameObject.SetActive(true);
        itemAmount.gameObject.SetActive(true);
        slotToggle.interactable = true;
        if (_stateInfo.amount > _stateInfo.stackSize)
        {
            itemAmount.text = _stateInfo.stackSize.ToString();
            return _stateInfo.stackSize - _stateInfo.amount;
        }
        itemAmount.text = _stateInfo.amount.ToString();
        return 0;
    }

    private void HandleSlotToggle()
    {
        if (slotToggle.isOn && _stateInfo != null)
        {
            itemInfo.SetItemInfo(_stateInfo);
        }
        else
        {
            itemInfo.gameObject.SetActive(false);
        }
    }
    
    public int SetStateInfoAndReturnRest(StateInfo stateInfo)
    {
        _stateInfo = stateInfo;
        return SetSlotAndReturnRest();
    }
    
    public void SetStateInfo(StateInfo stateInfo)
    {
        _stateInfo = stateInfo;
        SetSlot();
    }

    public void Deactivate()
    {
        itemImage.gameObject.SetActive(false);
        itemAmount.gameObject.SetActive(false);
        slotToggle.interactable = false;
        slotToggle.isOn = false;
    }

    public bool HasItem()
    {
        return _stateInfo != null;
    }
}
