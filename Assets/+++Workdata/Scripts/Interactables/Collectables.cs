using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectables : MonoBehaviour
{
    #region Inspektor
    
    [SerializeField] private List<State> states;

    [SerializeField] private UnityEvent onCollected;

    #endregion

    public void Collect()
    {
        onCollected.Invoke();
        FindObjectOfType<GameState>().Add(states);
        Destroy(gameObject);
    }
}
