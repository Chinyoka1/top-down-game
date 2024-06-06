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

    #region private methods

    private void OnEnable()
    {
        slotToggle.onValueChanged.AddListener(delegate {
            HandleSlotToggle();
        });
    }
    
    private void SetSlot()
    {
        itemImage.sprite = _stateInfo.icon;
        itemImage.gameObject.SetActive(true);
        itemAmount.gameObject.SetActive(true);
        slotToggle.interactable = true;
        itemAmount.text = _stateInfo.amount.ToString();
        if (_stateInfo.amount > _stateInfo.stackSize)
        {
            itemAmount.text = _stateInfo.stackSize.ToString();
        }
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

    #endregion

    #region public methods

    public void SetStateInfo(StateInfo stateInfo)
    {
        _stateInfo = stateInfo;
        SetSlot();
    }

    public void Deactivate()
    {
        _stateInfo = null;
        itemImage.gameObject.SetActive(false);
        itemAmount.gameObject.SetActive(false);
        slotToggle.interactable = false;
        slotToggle.isOn = false;
    }

    public bool HasItem()
    {
        return _stateInfo != null;
    }

    #endregion
}
