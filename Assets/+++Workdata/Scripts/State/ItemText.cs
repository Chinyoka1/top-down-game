using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemText : MonoBehaviour
{
    private RectTransform textTransform;
    private TextMeshProUGUI textMeshPro;
    private float timeToFade = 1f;
    private float timeElapsed = 0;

    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= timeToFade)
        {
            textMeshPro.color = Color.clear;
            Destroy(gameObject);
        }
    }
}
