using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemText;
    [SerializeField] private Image iconImage;
    private StateInfo _stateInfo;
    
    public void SetStateInfo(StateInfo stateInfo)
    {
        _stateInfo = stateInfo;
        SetItemPopup();
    }

    private void SetItemPopup()
    {
        itemText.text = (_stateInfo.amount > 0 ? "+" : "") + _stateInfo.amount + " " + _stateInfo.name;
        iconImage.sprite = _stateInfo.icon;
    }
}
