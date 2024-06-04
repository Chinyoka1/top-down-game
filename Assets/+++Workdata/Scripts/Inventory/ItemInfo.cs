using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    [SerializeField]private Image image;
    [SerializeField]private TextMeshProUGUI nameText;
    [SerializeField]private TextMeshProUGUI descriptionText;

    public void SetItemInfo(StateInfo stateInfo)
    {
        image.sprite = stateInfo.icon;
        nameText.text = stateInfo.name;
        descriptionText.text = stateInfo.description;
        gameObject.SetActive(true);
    }
}
