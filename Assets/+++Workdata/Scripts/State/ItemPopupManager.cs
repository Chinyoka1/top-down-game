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
        GameObject itemPopup = Instantiate(popupPrefab, itemPopupsContainer);
        TextMeshProUGUI itemText = itemPopup.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        Image iconImage = itemPopup.transform.Find("Icon").GetComponent<Image>();

        itemText.text = (amount > 0 ? "+" : "") + amount + " " + stateInfo.name;
        iconImage.sprite = stateInfo.icon;
        yield return new WaitForSeconds(2);
        Destroy(itemPopup);
    }
}
