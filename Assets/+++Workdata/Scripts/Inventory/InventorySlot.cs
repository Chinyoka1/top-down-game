using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private StateInfo stateInfo;
    [SerializeField]private Image itemImage;
    [SerializeField]private TextMeshProUGUI itemAmount;
    [SerializeField]private Toggle slotToggle;

    public void SetStateInfo(StateInfo stateInfo)
    {
        this.stateInfo = stateInfo;
        SetSlot();
    }
    
    public void Deactivate()
    {
        itemImage.sprite = null;
        itemAmount.text = "";
        slotToggle.isOn = false;
    } 

    private void SetSlot()
    {
        itemImage.sprite = stateInfo.icon;
        itemAmount.text = stateInfo.amount.ToString();
    }
}
