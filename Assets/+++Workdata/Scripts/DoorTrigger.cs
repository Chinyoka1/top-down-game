using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Trigger : MonoBehaviour
{
    public SpriteRenderer sr;

    /**
    * Element 0 Door closed
    * Element 1 Door open
    **/
    public Sprite[] doorSprites;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            sr.sprite = doorSprites[1];
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            sr.sprite = doorSprites[0];
        }
    }
}