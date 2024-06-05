using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Image image;
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = eventData.delta;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        rectTransform.sizeDelta = new Vector2(100, 100);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.sizeDelta = new Vector2(90, 90);
    }
}
