using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialLoader : MonoBehaviour
{
    public GameObject essentialContainer;

    private void Awake()
    {
        DontDestroyOnLoad(essentialContainer);
    }
}
