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

    public void SetStateInfo(StateInfo stateInfo)
    {
        _stateInfo = stateInfo;
        SetSlot();
    }
    
    public void Deactivate()
    {
        itemImage.gameObject.SetActive(false);
        itemAmount.gameObject.SetActive(false);
        slotToggle.isOn = false;
    } 

    private void SetSlot()
    {
        itemImage.sprite = _stateInfo.icon;
        itemAmount.text = _stateInfo.amount.ToString();
        itemImage.gameObject.SetActive(true);
        itemAmount.gameObject.SetActive(true);
    }
}
